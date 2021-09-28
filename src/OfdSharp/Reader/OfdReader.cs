using OfdSharp.Extensions;
using OfdSharp.Primitives;
using OfdSharp.Primitives.Doc;
using OfdSharp.Primitives.Fonts;
using OfdSharp.Primitives.Invoice;
using OfdSharp.Primitives.Ofd;
using OfdSharp.Primitives.PageDescription.Color;
using OfdSharp.Primitives.PageDescription.ColorSpace;
using OfdSharp.Primitives.PageDescription.DrawParam;
using OfdSharp.Primitives.PageObject;
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
                throw new FileNotFoundException($"文件{fileInfo.Name}未找到");
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
                System.Console.WriteLine(entryStream.GetType().FullName);
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
                XNamespace ns = document.Root.Name.Namespace;
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
                XNamespace ns = document.Root.Name.Namespace;
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
                    Pages = document.GetDescendants("Page").Select(f => new Primitives.PageTree.Page { Id = new Id(f.AttributeValueOrDefault("ID")), BaseLoc = new Location { Value = f.AttributeValueOrDefault("BaseLoc") } }).ToList(),
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
        /// 获取发票信息
        /// </summary>
        /// <returns></returns>
        public InvoiceInfo GetInvoiceInfo()
        {
            const string invoiceEntryName = "original_invoice.xml";
            ZipArchiveEntry invoiceEntry = _archive.Entries.First(f => f.Name == invoiceEntryName);
            return Deserialize<InvoiceInfo>(invoiceEntry);
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
