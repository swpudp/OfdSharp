using OfdSharp.Extensions;
using OfdSharp.Primitives;
using OfdSharp.Primitives.Attachments;
using OfdSharp.Primitives.CustomTags;
using OfdSharp.Primitives.Doc;
using OfdSharp.Primitives.Fonts;
using OfdSharp.Primitives.Invoice;
using OfdSharp.Primitives.Ofd;
using OfdSharp.Primitives.Resources;
using OfdSharp.Primitives.Signature;
using OfdSharp.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using OfdSharp.Primitives.Pages.Description.Color;
using OfdSharp.Primitives.Pages.Description.ColorSpace;
using OfdSharp.Primitives.Pages.Description.DrawParam;
using OfdSharp.Primitives.Pages.Object;

namespace OfdSharp.Reader
{
    /// <summary>
    /// ofd文件读取器
    /// </summary>
    public class OfdReader
    {
        /// <summary>
        /// 文档概述
        /// </summary>
        public DocSummary Summary { get; set; }

        /// <summary>
        /// ofd入口文件名称
        /// </summary>
        private const string OfdFileName = "OFD.xml";

        /// <summary>
        /// 压缩文件
        /// </summary>
        private ZipArchive _archive;

        /// <summary>
        /// 文档路径
        /// </summary>
        private string _docRoot = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filePath"></param>
        public OfdReader(string filePath)
        {
            UnZip(new FileInfo(filePath));
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileInfo"></param>
        public OfdReader(FileInfo fileInfo)
        {
            UnZip(fileInfo);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="stream"></param>
        public OfdReader(Stream stream)
        {
            UnZip(stream);
        }

        /// <summary>
        /// 确保文件存在
        /// </summary>
        private static void EnsureFileExist(FileSystemInfo fileInfo)
        {
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException("文件未找到", fileInfo.FullName);
            }
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        private void UnZip(FileInfo fileInfo)
        {
            EnsureFileExist(fileInfo);
            using (FileStream originalFileStream = fileInfo.OpenRead())
            {
                MemoryStream decompressedStream = new MemoryStream();
                originalFileStream.CopyTo(decompressedStream);
                UnZip(decompressedStream);
            }
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        private void UnZip(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            _archive = new ZipArchive(stream, ZipArchiveMode.Read, true);
        }

        /// <summary>
        /// 读取zip项内容
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        private static MemoryStream ReadEntry(ZipArchiveEntry entry)
        {
            MemoryStream memory = new MemoryStream();
            using (Stream entryStream = entry.Open())
            {
                entryStream.CopyTo(memory);
                memory.Position = 0;
            }
            return memory;
        }

        /// <summary>
        /// 读取zip项内容
        /// </summary>
        /// <param name="entryFile"></param>
        /// <returns></returns>
        public byte[] ReadContent(string entryFile)
        {
            ZipArchiveEntry entry = _archive.Entries.First(f => f.FullName == entryFile.TrimStart('/'));
            using (Stream entryStream = entry.Open())
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    entryStream.CopyTo(memory);
                    memory.Seek(0, SeekOrigin.Begin);
                    return memory.ToArray();
                }
            }
        }

        /// <summary>
        /// 获取压缩项
        /// </summary>
        /// <param name="entryName"></param>
        /// <returns></returns>
        private ZipArchiveEntry GetEntry(string entryName)
        {
            if (string.IsNullOrWhiteSpace(entryName))
            {
                return null;
            }
            string fullName = entryName;
            if (!string.IsNullOrWhiteSpace(_docRoot) && !entryName.StartsWith(_docRoot))
            {
                fullName = string.Concat(_docRoot, "/", entryName);
            }
            return _archive.Entries.FirstOrDefault(f => f.FullName == fullName);
        }

        /// <summary>
        /// 入口文件
        /// </summary>
        private OfdRoot _ofdRoot;

        /// <summary>
        /// 入口文件OFD.xml信息
        /// </summary>
        /// <returns></returns>
        public OfdRoot GetOfdRoot()
        {
            if (_ofdRoot != null)
            {
                return _ofdRoot;
            }
            ZipArchiveEntry entry = GetEntry(OfdFileName);
            using (MemoryStream memory = ReadEntry(entry))
            {
                var document = XDocument.Load(memory);
                //XNamespace ns = document.Root.Name.Namespace;
                _ofdRoot = new OfdRoot
                {
                    DocBody = new DocBody
                    {
                        DocInfo = new DocInfo
                        {
                            DocId = document.FirstValueOrDefault("DocID"),
                            CustomDatas = document.GetDescendants("CustomData").Select(f => new CustomData { Name = f.AttributeValueOrDefault("Name"), Value = f.Value }).ToList()
                        },
                        DocRoot = new Location { Value = document.FirstValueOrDefault("DocRoot") },
                        Signatures = new Location { Value = document.FirstValueOrDefault("Signatures") }
                    }
                };
                _docRoot = _ofdRoot.DocBody.DocRoot.Value.Split('/')[0];
                return _ofdRoot;
            }
        }

        /// <summary>
        /// 主文档文件
        /// </summary>
        private Document _document;

        /// <summary>
        /// 文档主入口Document.xml读取
        /// </summary>
        /// <returns></returns>
        public Document GetDocument()
        {
            if (_document != null)
            {
                return _document;
            }
            OfdRoot ofdRoot = GetOfdRoot();
            ZipArchiveEntry entry = GetEntry(ofdRoot.DocBody.DocRoot.Value);
            using (MemoryStream memory = ReadEntry(entry))
            {
                var document = XDocument.Load(memory);
                _document = new Document
                {
                    CommonData = new CommonData
                    {
                        MaxUnitId = new Id(document.FirstValueOrDefault("MaxUnitID")),
                        PageArea = new PageArea { Physical = Box.Parse(document.FirstValueOrDefault("PhysicalBox")) },
                        PublicRes = new Location { Value = document.FirstValueOrDefault("PublicRes") },
                        DocumentRes = new Location { Value = document.FirstValueOrDefault("DocumentRes") },
                        TemplatePages = document.GetDescendants("TemplatePage").Select(f => new TemplatePage
                        {
                            Id = new Id(f.AttributeValueOrDefault("ID")),
                            BaseLoc = new Location { Value = f.AttributeValueOrDefault("BaseLoc") },
                            ZOrder = f.AttributeValueOrDefault("ZOrder").ParseEnum<LayerType>()
                        }).ToList()
                    },
                    Pages = document.GetDescendants("Page").Select(f => new Primitives.Pages.Tree.PageNode{ Id = new Id(f.AttributeValueOrDefault("ID")), BaseLoc = new Location { Value = f.AttributeValueOrDefault("BaseLoc") } }).ToList(),
                    Annotations = new List<Location> { new Location { Value = document.FirstValueOrDefault("Annotations") } },
                    Attachments = new List<Location> { new Location { Value = document.FirstValueOrDefault("Attachments") } },
                    CustomTags = new List<Location> { new Location { Value = document.FirstValueOrDefault("CustomTags") } }
                };
                return _document;
            }
        }

        /// <summary>
        /// 资源文件
        /// </summary>
        private DocumentResource _documentResource;

        /// <summary>
        /// 资源文件DocumentRes.xml读取
        /// </summary>
        /// <returns></returns>
        public DocumentResource GetDocumentResource()
        {
            if (_documentResource != null)
            {
                return _documentResource;
            }
            Document document = GetDocument();
            ZipArchiveEntry entry = GetEntry(document.CommonData.DocumentRes.Value);
            if (entry == null)
            {
                return null;
            }
            using (MemoryStream memory = ReadEntry(entry))
            {
                var xDocument = XDocument.Load(memory);
                _documentResource = GetDocumentResource(xDocument);
                return _documentResource;
            }
        }

        /// <summary>
        /// 资源文件PublicRes.xml读取
        /// </summary>
        /// <returns></returns>
        public DocumentResource GetPublicResource()
        {
            if (_documentResource != null)
            {
                return _documentResource;
            }
            Document document = GetDocument();
            ZipArchiveEntry entry = GetEntry(document.CommonData.PublicRes.Value);
            if (entry == null)
            {
                return null;
            }
            using (MemoryStream memory = ReadEntry(entry))
            {
                XDocument xDocument = XDocument.Load(memory);
                _documentResource = GetDocumentResource(xDocument);
                return _documentResource;
            }
        }

        /// <summary>
        /// 从文档读取资源文件
        /// </summary>
        /// <param name="xDocument"></param>
        /// <returns></returns>
        private DocumentResource GetDocumentResource(XDocument xDocument)
        {
            return new DocumentResource
            {
                BaseLoc = new Location { Value = xDocument.Root.AttributeValueOrDefault("BaseLoc") },
                ColorSpaces = xDocument.GetDescendants("ColorSpace").Select(f => new CtColorSpace
                {
                    Type = f.AttributeValueOrDefault("Type").ParseEnum<ColorSpaceType>(),
                    BitsPerComponent = f.AttributeValueOrDefault("BitsPerComponent").ParseEnum<BitsPerComponent>(),
                    Id = new Id(f.AttributeValueOrDefault("ID"))
                }).ToList(),
                Fonts = xDocument.GetDescendants("Font").Select(f => new CtFont
                {
                    Id = new Id(f.AttributeValueOrDefault("ID")),
                    FontName = f.AttributeValueOrDefault("FontName"),
                    FamilyName = f.AttributeValueOrDefault("FamilyName")
                }).ToList(),
                DrawParams = xDocument.GetDescendants("DrawParam").Select(f => new CtDrawParam
                {
                    Id = new Id(f.AttributeValueOrDefault("ID")),
                    LineWidth = Convert.ToDouble(f.AttributeValueOrDefault("LineWidth")),
                    FillColor = new CtColor
                    {
                        Value = Primitives.Array.Parse(f.AttributeValueForElementOrDefault("FillColor", "Value")),
                        ColorSpace = new RefId { Id = new Id(f.AttributeValueForElementOrDefault("FillColor", "ColorSpace")) }
                    },
                    StrokeColor = new CtColor
                    {
                        Value = Primitives.Array.Parse(f.AttributeValueForElementOrDefault("StrokeColor", "Value")),
                        ColorSpace = new RefId { Id = new Id(f.AttributeValueForElementOrDefault("StrokeColor", "ColorSpace")) }
                    }
                }).ToList(),
                MultiMedias = xDocument.GetDescendants("MultiMedia").Select(f => new CtMultiMedia
                {
                    Type = f.AttributeValueOrDefault("Type").ParseEnum<MediaType>(),
                    Id = new Id(f.AttributeValueOrDefault("ID")),
                    MediaFile = new Location { Value = f.ElementValueOrDefault("MediaFile") }
                }).ToList()
            };
        }

        /// <summary>
        /// 附件信息
        /// </summary>
        private List<Attachment> _attachments;

        /// <summary>
        /// 获取附件信息
        /// </summary>
        /// <returns></returns>
        public List<Attachment> GetAttachments()
        {
            if (_attachments != null)
            {
                return _attachments;
            }
            Document document = GetDocument();
            _attachments = new List<Attachment>();
            foreach (var attachLocation in document.Attachments)
            {
                ZipArchiveEntry entry = GetEntry(attachLocation.Value);
                if (entry == null)
                {
                    return null;
                }
                using (MemoryStream memory = ReadEntry(entry))
                {
                    XDocument xDocument = XDocument.Load(memory);
                    var currentAttachments = xDocument.GetDescendants("Attachment").Select(f => new Attachment
                    {
                        Id = new Id(f.AttributeValueOrDefault("ID")),
                        Name = f.AttributeValueOrDefault("Name"),
                        Format = f.AttributeValueOrDefault("Format"),
                        Visible = bool.TryParse(f.AttributeValueOrDefault("Visible"), out bool visible) && visible,
                        FileLoc = new Location { Value = string.Concat(Path.GetDirectoryName(attachLocation.Value), "/", Path.GetFileName(f.ElementValueOrDefault("FileLoc"))) }
                    }).ToList();
                    _attachments.AddRange(currentAttachments);
                }
            }
            return _attachments;
        }

        /// <summary>
        /// 自定义标签
        /// </summary>
        private List<CustomTag> _customTags;

        /// <summary>
        /// 获取自定义标签
        /// </summary>
        /// <returns></returns>
        public List<CustomTag> GetCustomTags()
        {
            if (_customTags != null)
            {
                return _customTags;
            }
            Document document = GetDocument();
            _customTags = new List<CustomTag>();
            foreach (var customTag in document.CustomTags)
            {
                ZipArchiveEntry entry = GetEntry(customTag.Value);
                if (entry == null)
                {
                    return null;
                }
                using (MemoryStream memory = ReadEntry(entry))
                {
                    XDocument xDocument = XDocument.Load(memory);
                    var currentCustomTag = xDocument.GetDescendants("CustomTag").Select(f => new CustomTag
                    {
                        TypeId = f.AttributeValueOrDefault("TypeID"),
                        FileLoc = new Location { Value = f.ElementValueOrDefault("FileLoc") }
                    });
                    _customTags.AddRange(currentCustomTag);
                }
            }
            return _customTags;
        }



        private static XmlDocument LoadXml(ZipArchiveEntry entry)
        {
            using (Stream entryStream = entry.Open())
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    entryStream.CopyTo(memory);
                    memory.Seek(0, SeekOrigin.Begin);
                    XmlDocument body = new XmlDocument();
                    body.Load(memory);
                    return body;
                }
            }
        }

        private static T Deserialize<T>(ZipArchiveEntry entry)
        {
            using (Stream entryStream = entry.Open())
            {
                return XmlUtils.Deserialize<T>(entryStream);
            }
        }

        /// <summary>
        /// 获取签名文件
        /// </summary>
        /// <returns></returns>
        public string GetSignatures()
        {
            ZipArchiveEntry entry = _archive.Entries.First(f => f.FullName == "OFD.xml");
            XmlDocument body = LoadXml(entry);

            XmlNode node = body.LastChild.LastChild.LastChild.LastChild;
            string signaturesFile = node.Value;

            ZipArchiveEntry signaturesBaseLoc = _archive.Entries.First(f => f.FullName == signaturesFile);
            XmlDocument signaturesBaseLocContent = LoadXml(signaturesBaseLoc);
            string signaturesBaseLocFile = signaturesBaseLocContent.LastChild.LastChild.Attributes.GetNamedItem("BaseLoc").Value;


            return signaturesBaseLocFile;


        }

        public DigestInfo GetSignature()
        {
            string signaturesBaseLocFile = GetSignatures();
            ZipArchiveEntry signatureFile = _archive.Entries.First(f => f.FullName == signaturesBaseLocFile.TrimStart('/'));
            return Deserialize<DigestInfo>(signatureFile);
        }

        public string GetSignedList()
        {
            string signatures = GetSignatures();
            ZipArchiveEntry entry = _archive.Entries.First(f => f.FullName == signatures);
            XmlDocument document = LoadXml(entry);
            XmlNode node = document.LastChild.LastChild;
            if (node == null)
            {
                return string.Empty;
            }
            ZipArchiveEntry signedEntry = _archive.Entries.First(f => f.FullName == node.Value);
            XmlDocument signedXml = LoadXml(signedEntry);
            XmlNode signedNode = signedXml.LastChild.LastChild;
            if (signedNode == null)
            {
                return string.Empty;
            }
            foreach (XmlElement childNode in signedNode.ChildNodes)
            {
                return childNode.Value;
            }
            return string.Empty;
        }

        /// <summary>
        /// 发票信息
        /// </summary>
        private InvoiceInfo _invoiceInfo;

        /// <summary>
        /// 获取发票信息
        /// </summary>
        /// <returns></returns>
        public InvoiceInfo GetInvoiceInfo()
        {
            if (_invoiceInfo != null)
            {
                return _invoiceInfo;
            }
            List<Attachment> attachments = GetAttachments();
            Attachment invoiceAttachment = attachments.FirstOrDefault(f => f.Name == "original_invoice");
            if (invoiceAttachment == null)
            {
                throw new FileNotFoundException("original_invoice.xml");
            }
            ZipArchiveEntry entry = GetEntry(invoiceAttachment.FileLoc.Value);
            if (entry == null)
            {
                return null;
            }
            using (MemoryStream memory = ReadEntry(entry))
            {
                XDocument xDocument = XDocument.Load(memory);

                //todo 无法读取namespace
                const string invoiceNamespace = "http://www.edrm.org.cn/schema/e-invoice/2019";
                XElement root = xDocument.Root;
                _invoiceInfo = new InvoiceInfo
                {
                    DocId = root.ElementValueOrDefault(XName.Get("DocID", invoiceNamespace)),
                    AreaCode = root.ElementValueOrDefault(XName.Get("AreaCode", invoiceNamespace)),
                    TypeCode = root.ElementValueOrDefault(XName.Get("TypeCode", invoiceNamespace)),
                    InvoiceSIA2 = root.ElementValueOrDefault(XName.Get("InvoiceSIA2", invoiceNamespace)),
                    InvoiceSIA1 = root.ElementValueOrDefault(XName.Get("InvoiceSIA1", invoiceNamespace)),
                    InvoiceCode = root.ElementValueOrDefault(XName.Get("InvoiceCode", invoiceNamespace)),
                    InvoiceNo = root.ElementValueOrDefault(XName.Get("InvoiceNo", invoiceNamespace)),
                    IssueDate = root.ElementValueOrDefault(XName.Get("IssueDate", invoiceNamespace)),
                    InvoiceCheckCode = root.ElementValueOrDefault(XName.Get("InvoiceCheckCode", invoiceNamespace)),
                    MachineNo = root.ElementValueOrDefault(XName.Get("MachineNo", invoiceNamespace)),
                    GraphCode = root.ElementValueOrDefault(XName.Get("GraphCode", invoiceNamespace)),
                    TaxControlCode = root.ElementValueOrDefault(XName.Get("TaxControlCode", invoiceNamespace)),
                    Buyer = new BuyerInfo
                    {
                        BuyerName = root.ElementValueForElementOrDefault(XName.Get("Buyer", invoiceNamespace), XName.Get("BuyerName", invoiceNamespace)),
                        BuyerTaxNo = root.ElementValueForElementOrDefault(XName.Get("Buyer", invoiceNamespace), XName.Get("BuyerTaxID", invoiceNamespace)),
                        BuyerAddressTel = root.ElementValueForElementOrDefault(XName.Get("Buyer", invoiceNamespace), XName.Get("BuyerAddrTel", invoiceNamespace)),
                        BuyerBankAccount = root.ElementValueForElementOrDefault(XName.Get("Buyer", invoiceNamespace), XName.Get("BuyerFinancialAccount", invoiceNamespace))
                    },
                    Seller = new SellerInfo
                    {
                        SellerName = root.ElementValueForElementOrDefault(XName.Get("Seller", invoiceNamespace), XName.Get("SellerName", invoiceNamespace)),
                        SellerTaxNo = root.ElementValueForElementOrDefault(XName.Get("Seller", invoiceNamespace), XName.Get("SellerTaxID", invoiceNamespace)),
                        SellerAddressTel = root.ElementValueForElementOrDefault(XName.Get("Seller", invoiceNamespace), XName.Get("SellerAddrTel", invoiceNamespace)),
                        SellerBankAccount = root.ElementValueForElementOrDefault(XName.Get("Seller", invoiceNamespace), XName.Get("SellerFinancialAccount", invoiceNamespace))
                    },
                    TaxInclusiveTotalAmount = root.ElementValueOrDefault(XName.Get("TaxInclusiveTotalAmount", invoiceNamespace)),
                    TaxInclusiveTotalAmountWithWords = root.ElementValueOrDefault(XName.Get("TaxInclusiveTotalAmountWithWords", invoiceNamespace)),
                    TaxExclusiveTotalAmount = root.ElementValueOrDefault(XName.Get("TaxExclusiveTotalAmount", invoiceNamespace)),
                    TaxTotalAmount = root.ElementValueOrDefault(XName.Get("TaxTotalAmount", invoiceNamespace)),
                    Note = root.ElementValueOrDefault(XName.Get("Note", invoiceNamespace)),
                    InvoiceClerk = root.ElementValueOrDefault(XName.Get("InvoiceClerk", invoiceNamespace)),
                    Payee = root.ElementValueOrDefault(XName.Get("Payee", invoiceNamespace)),
                    Checker = root.ElementValueOrDefault(XName.Get("Checker", invoiceNamespace)),
                    Signature = root.ElementValueOrDefault(XName.Get("Signature", invoiceNamespace)),
                    DeductibleAmount = root.ElementValueOrDefault(XName.Get("DeductibleAmount", invoiceNamespace)),
                    OriginalInvoiceCode = root.ElementValueOrDefault(XName.Get("OriginalInvoiceCode", invoiceNamespace)),
                    OriginalInvoiceNo = root.ElementValueOrDefault(XName.Get("OriginalInvoiceNo", invoiceNamespace)),

                };
                _invoiceInfo.GoodsInfos = root.Descendants(XName.Get("GoodsInfo", invoiceNamespace)).Select(f => new GoodsInfo
                {
                    LineNo = int.Parse(f.ElementValueOrDefault("LineNo")),
                    LineNature = int.Parse(f.ElementValueOrDefault("LineNature")),
                    Item = f.ElementValueOrDefault("Item"),
                    Code = f.ElementValueOrDefault("Code"),
                    Specification = f.ElementValueOrDefault("Specification"),
                    MeasurementDimension = f.ElementValueOrDefault("MeasurementDimension"),
                    Price = f.ElementValueOrDefault("Price"),
                    Quantity = f.ElementValueOrDefault("Quantity"),
                    Amount = f.ElementValueOrDefault("Amount"),
                    TaxScheme = f.ElementValueOrDefault("TaxScheme"),
                    TaxAmount = f.ElementValueOrDefault("TaxAmount"),
                    PreferentialMark = f.ElementValueOrDefault("PreferentialMark"),
                    FreeTaxMark = f.ElementValueOrDefault("FreeTaxMark"),
                    VATSpecialManagement = f.ElementValueOrDefault("VATSpecialManagement")
                }).ToList();
            }
            return _invoiceInfo;
        }

        /// <summary>
        /// 获取文档
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetDoc(int index)
        {
            return $"Doc_{index}";
        }

        /// <summary>
        /// 关闭文档
        /// </summary>
        public void Close()
        {
            _archive.Dispose();
        }
    }
}
