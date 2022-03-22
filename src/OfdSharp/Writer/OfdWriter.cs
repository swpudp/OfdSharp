using OfdSharp.Primitives;
using OfdSharp.Primitives.Fonts;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Linq;
using OfdSharp.Primitives.Pages.Object;
using OfdSharp.Primitives.Pages.Tree;
using OfdSharp.Primitives.Text;
using OfdSharp.Primitives.Attachments;
using OfdSharp.Primitives.Resources;
using OfdSharp.Extensions;
using OfdSharp.Primitives.CustomTags;
using OfdSharp.Primitives.Annotations;
using OfdSharp.Primitives.Entry;
using OfdSharp.Crypto;
using OfdSharp.Ses;
using OfdSharp.Primitives.Signature;
using System.Text;
using System;
using OfdSharp.Sign;
using OfdSharp.Primitives.Doc;

namespace OfdSharp.Writer
{
    public class OfdWriter
    {
        private readonly bool _isFormat;
        private readonly OfdDocument _ofdDocument;
        private XElement _rootElement;
        private XElement _documentElement;
        private XElement _documentResElement;
        private XElement _publicResElement;
        private XElement _pageElement;
        private XElement _attachmentElement;
        private XElement _customerTagsElement;
        private XElement _annotationsElement;
        private XElement _signaturesElement;
        private XElement _signatureElement;

        //页面
        //private readonly List<PageNode> _pageNodes = new List<PageNode>();
        //private readonly List<PageObject> _pageObjects = new List<PageObject>();

        //附件
        private List<Attachment> _attachments = new List<Attachment>();
        private List<XElement> _attachmentElements = new List<XElement>();

        //多媒体
        //private List<CtMultiMedia> _ctMultiMedias = new List<CtMultiMedia>();

        //模板
        //private List<PageObject> _templatePages = new List<PageObject>();
        private List<XElement> _templateElements = new List<XElement>();

        //customerTag
        //private List<CustomTag> _customTags = new List<CustomTag>();
        private List<XElement> _customTagElements = new List<XElement>();

        private XElement _annotationElement;
        private Org.BouncyCastle.X509.X509Certificate _sealCert, _signerCert;
        private CipherKeyPair _sealKey, _signerKey;
        public OfdWriter(OfdDocument ofdDocument, bool isFormating)
        {
            _ofdDocument = ofdDocument;
            _isFormat = isFormating;
        }

        /// <summary>
        /// 入口文件写入
        /// </summary>
        /// <param name="docInfo">文档描述信息</param>
        /// <param name="docIndex">文档</param>
        public void WriteOfdRoot(CtDocInfo docInfo, int docIndex)
        {
            XElement docInfoElement = XmlExtension.CreateElement("DocInfo");
            docInfoElement.CreateOptionalElement("DocID", docInfo.DocId);
            docInfoElement.CreateOptionalElement("Title", docInfo.Title);
            docInfoElement.CreateOptionalElement("Author", docInfo.Author);
            docInfoElement.CreateOptionalElement("Subject", docInfo.Subject);
            docInfoElement.CreateOptionalElement("Abstract", docInfo.Abstract);
            docInfoElement.CreateOptionalElement("CreationDate", docInfo.CreationDate);
            docInfoElement.CreateOptionalElement("ModDate", docInfo.ModDate);
            docInfoElement.CreateOptionalElement("DocUsage", docInfo.DocUsage);
            docInfoElement.CreateOptionalElement("Cover", docInfo.Cover);
            if (docInfo.Keywords.IsAny())
            {
                XElement keywordsElement = XmlExtension.CreateElement("Keywords");
                docInfo.Keywords.ForEach(item => keywordsElement.CreateRequiredElement("Keyword", item));
                docInfoElement.Add(keywordsElement);
            }
            docInfoElement.CreateOptionalElement("Creator", docInfo.Creator);
            docInfoElement.CreateOptionalElement("CreatorVersion", docInfo.CreatorVersion);
            if (docInfo.CustomDataList.IsAny())
            {
                XElement customDataElement = XmlExtension.CreateElement("CustomDatas");
                docInfo.CustomDataList.ForEach(item => customDataElement.CreateRequiredElement("CustomData", item.Value, new XAttribute("Name", item.Name)));
                docInfoElement.Add(customDataElement);
            }
            XElement docBody = XmlExtension.CreateElement("DocBody");
            docBody.Add(docInfoElement);
            docBody.CreateRequiredElement("DocRoot", $"Doc_{docIndex}/Document.xml");
            docBody.CreateRequiredElement("Signatures", $"Doc_{docIndex}/Signs/Signatures.xml");
            _rootElement = XmlExtension.CreateElement("OFD", docBody, new XAttribute(XNamespace.Xmlns + ConstDefined.OfdPrefix, ConstDefined.OfdXmlns), new XAttribute("DocType", ConstDefined.OfdDocType), new XAttribute("Version", ConstDefined.OfdVersion));
        }

        public void WriteDocument(CommonData commonData, List<PageNode> pages, bool hasAttachment)
        {
            XElement commonDataElement = XmlExtension.CreateElement("CommonData");
            commonDataElement.CreateRequiredElement("MaxUnitID", commonData.MaxUnitId);
            if (commonData.TemplatePages.IsAny())
            {
                commonData.TemplatePages.ForEach((item) => commonDataElement.CreateRequiredElement("TemplatePage", new XAttribute("ID", item.Id), new XAttribute("BaseLoc", item.BaseLoc.Value)));
            }
            commonDataElement.Add(XmlExtension.CreateElement("PageArea", XmlExtension.CreateElement("PhysicalBox", commonData.PageArea.Physical.ToString())));
            commonDataElement.Add(XmlExtension.CreateElement("DocumentRes", commonData.DocumentRes.Value));
            commonDataElement.Add(XmlExtension.CreateElement("PublicRes", commonData.PublicRes.Value));

            _documentElement = XmlExtension.CreateElement("Document", new XAttribute(XNamespace.Xmlns + ConstDefined.OfdPrefix, ConstDefined.OfdXmlns));
            _documentElement.Add(commonDataElement);

            //Pages
            XElement pagesElement = XmlExtension.CreateElement("Pages");
            foreach (var pg in pages)
            {
                pagesElement.CreateRequiredElement("Page", new XAttribute("ID", pg.Id), new XAttribute("BaseLoc", pg.BaseLoc.Value));
            }
            _documentElement.Add(pagesElement);

            //Annotations
            XElement annotationsElement = XmlExtension.CreateElement("Annotations", "Annots/Annotations.xml");
            _documentElement.Add(annotationsElement);

            if (hasAttachment)
            {
                //Attachments
                XElement attachmentsElement = XmlExtension.CreateElement("Attachments", "Attachs/Attachments.xml");
                _documentElement.Add(attachmentsElement);
            }

            //CustomTags
            XElement customTagsElement = new XElement(ConstDefined.OfdNamespace + "CustomTags", "Tags/CustomTags.xml");
            _documentElement.Add(customTagsElement);
        }

        public void WriteDocumentRes(DocumentResource res)
        {
            _documentResElement = XmlExtension.CreateElement("Res", new XAttribute(XNamespace.Xmlns + ConstDefined.OfdPrefix, ConstDefined.OfdXmlns), new XAttribute("BaseLoc", res.BaseLoc.Value));
            //DrawParams
            XElement drawParams = XmlExtension.CreateElement("DrawParams");

            foreach (var item in res.DrawParams)
            {
                XElement drawParam = XmlExtension.CreateElement("DrawParam", new XAttribute("ID", item.Id), new XAttribute("LineWidth", item.LineWidth));

                if (item.FillColor != null)
                {
                    drawParam.CreateRequiredElement("FillColor", new XAttribute("Value", item.FillColor.Value), new XAttribute("ColorSpace", item.FillColor.ColorSpace));
                }
                if (item.StrokeColor != null)
                {
                    drawParam.CreateRequiredElement("StrokeColor", new XAttribute("Value", item.StrokeColor.Value), new XAttribute("ColorSpace", item.StrokeColor.ColorSpace));
                }
                drawParams.Add(drawParam);
            }
            _documentResElement.Add(drawParams);

            //MultiMedias
            if (res.MultiMedias.IsAny())
            {
                XElement multiMedias = XmlExtension.CreateElement("MultiMedias");
                foreach (var ctMultiMedia in res.MultiMedias)
                {
                    XElement multiMedia = XmlExtension.CreateElement("MultiMedia", new XAttribute("ID", ctMultiMedia.Id), new XAttribute("Type", ctMultiMedia.Type), new XAttribute("Format", ctMultiMedia.Format));
                    multiMedia.CreateRequiredElement("MediaFile", ctMultiMedia.MediaFile.Value);
                    multiMedias.Add(multiMedia);
                }
                _documentResElement.Add(multiMedias);
            }
        }

        public void WritePublicRes(DocumentResource res)
        {
            _publicResElement = XmlExtension.CreateElement("Res", new XAttribute(XNamespace.Xmlns + ConstDefined.OfdPrefix, ConstDefined.OfdXmlns), new XAttribute("BaseLoc", res.BaseLoc.Value));
            if (res.ColorSpaces.IsAny())
            {
                XElement colorSpacesElement = new XElement(ConstDefined.OfdNamespace + "ColorSpaces");
                foreach (var colorSpace in res.ColorSpaces)
                {
                    colorSpacesElement.CreateRequiredElement("ColorSpace", new XAttribute("ID", colorSpace.Id), new XAttribute("Type", colorSpace.Type), new XAttribute("BitsPerComponent", colorSpace.BitsPerComponent));
                }
                _publicResElement.Add(colorSpacesElement);
            }
            if (res.Fonts.IsAny())
            {
                XElement fontsElement = XmlExtension.CreateElement("Fonts");
                foreach (var ctFont in res.Fonts)
                {
                    fontsElement.CreateRequiredElement("Font", new XAttribute("ID", ctFont.Id.ToString()), new XAttribute("FontName", ctFont.FontName), new XAttribute("FamilyName", ctFont.FamilyName));
                }
                _publicResElement.Add(fontsElement);
            }
        }

        /// <summary>
        /// todo pageobjects如何组织？
        /// </summary>
        /// <param name="pageObjects"></param>
        public void WritePages(List<PageObject> pageObjects)
        {
            _pageElement = XmlExtension.CreateElement("Page", new XAttribute(XNamespace.Xmlns + ConstDefined.OfdPrefix, ConstDefined.OfdXmlns));
            foreach (var po in pageObjects)
            {
                _pageElement.CreateRequiredElement("Area", XmlExtension.CreateElement("PhysicalBox", po.Area.Physical.ToString()));
                if (po.Template != null)
                {
                    _pageElement.CreateRequiredElement("Template", new XAttribute("TemplateID", po.Template.TemplateId), new XAttribute("ZOrder", po.Template.ZOrder));
                }
                XElement contentElement = XmlExtension.CreateElement("Content");
                foreach (var layer in po.Content.Layers)
                {
                    XElement layerElement = XmlExtension.CreateElement("Layer", new XAttribute("ID", layer.Id));

                    foreach (var pb in layer.PageBlocks)
                    {
                        if (pb.TextObject != null)
                        {
                            XElement textObjectElement = XmlExtension.CreateElement("TextObject", new XAttribute("ID", pb.TextObject.Id), new XAttribute("Boundary", pb.TextObject.Boundary.ToString()), new XAttribute("Font", pb.TextObject.Font.Id), new XAttribute("Size", pb.TextObject.Size));
                            textObjectElement.CreateRequiredElement("FillColor", new XAttribute("Value", pb.TextObject.FillColor.Value.ToString()));
                            textObjectElement.CreateRequiredElement("TextCode", pb.TextObject.TextCode.Value, new XAttribute("X", pb.TextObject.TextCode.X), new XAttribute("Y", pb.TextObject.TextCode.Y), new XAttribute("DeltaX", "g " + pb.TextObject.TextCode.DeltaX));
                            layerElement.Add(textObjectElement);
                        }
                        if (pb.PathObject != null)
                        {
                            XElement pathObjectElement = XmlExtension.CreateElement("PathObject", new XAttribute("ID", pb.PathObject.Id), new XAttribute("Boundary", pb.PathObject.Boundary.ToString()), new XAttribute("LineWidth", pb.PathObject.LineWidth));
                            pathObjectElement.CreateRequiredElement("StrokeColor", new XAttribute("Value", pb.PathObject.StrokeColor.Value.ToString()));
                            pathObjectElement.CreateRequiredElement("AbbreviatedData", pb.PathObject.AbbreviatedData);
                            layerElement.Add(pathObjectElement);
                        }
                        if (pb.ImageObject != null)
                        {
                            layerElement.CreateRequiredElement("ImageObject", new XAttribute("ID", pb.ImageObject.Id), new XAttribute("Boundary", pb.ImageObject.Boundary), new XAttribute("CTM", pb.ImageObject.Ctm), new XAttribute("ResourceID", pb.ImageObject.ResourceId));
                        }
                    }
                    contentElement.Add(layerElement);
                }
                _pageElement.Add(contentElement);
            }
        }

        /// <summary>
        /// 添加附件
        /// </summary>
        public void AddAttachment(string name, string fileName, string format, bool visible, XElement content)
        {
            Attachment attachment = new Attachment
            {
                Id = new Id(10),
                Name = name,
                FileLoc = new Location { Value = fileName },
                Visible = visible,
                Format = format
            };
            _attachments.Add(attachment);
            _attachmentElements.Add(content);
        }

        /// <summary>
        /// 写入附件信息
        /// </summary>
        private void ParseAttachment()
        {
            if (!_attachments.Any())
            {
                return;
            }

            _attachmentElement = new XElement(ConstDefined.OfdNamespace + "Attachments", new XAttribute(XNamespace.Xmlns + "ofd", ConstDefined.OfdXmlns));
            foreach (var attachment in _attachments)
            {
                XElement newAttachment = new XElement(ConstDefined.OfdNamespace + "Attachment", new XAttribute("ID", attachment.Id), new XAttribute("Name", attachment.Name), new XAttribute("Format", attachment.Format), new XAttribute("Visible", attachment.Visible));
                newAttachment.Add(new XElement(ConstDefined.OfdNamespace + "FileLoc", attachment.FileLoc.Value));
                _attachmentElement.Add(newAttachment);
            }
        }

        public void WriteTemplate(List<PageObject> templatePages)
        {
            foreach (var pageObject in templatePages)
            {
                XElement pageElement = XmlExtension.CreateElement("Page", new XAttribute(XNamespace.Xmlns + ConstDefined.OfdPrefix, ConstDefined.OfdXmlns));
                XElement contentElement = XmlExtension.CreateElement("Content");
                foreach (var layer in pageObject.Content.Layers)
                {
                    XElement layerElement = XmlExtension.CreateElement("Layer", new XAttribute("DrawParam", layer.DrawParam), new XAttribute("ID", layer.Id));
                    foreach (var pageBlock in layer.PageBlocks)
                    {
                        if (pageBlock.PathObject != null)
                        {
                            XElement pathElement = XmlExtension.CreateElement("PathObject", new XAttribute("ID", pageBlock.PathObject.Id), new XAttribute("Boundary", pageBlock.PathObject.Boundary), new XAttribute("LineWidth", pageBlock.PathObject.LineWidth));
                            pathElement.CreateRequiredElement("FillColor", new XAttribute("Value", pageBlock.PathObject.FillColor.Value), new XAttribute("ColorSpace", pageBlock.PathObject.FillColor.ColorSpace));
                            pathElement.CreateRequiredElement("StrokeColor", new XAttribute("Value", pageBlock.PathObject.StrokeColor.Value), new XAttribute("ColorSpace", pageBlock.PathObject.StrokeColor.ColorSpace));
                            pathElement.CreateRequiredElement("AbbreviatedData", new XAttribute("Value", pageBlock.PathObject.AbbreviatedData));
                            layerElement.Add(pathElement);
                        }

                        if (pageBlock.TextObject != null)
                        {
                            XElement textObjectElement = XmlExtension.CreateElement("TextObject", new XAttribute("ID", pageBlock.TextObject.Id.ToString()), new XAttribute("Boundary", pageBlock.TextObject.Boundary.ToString()), new XAttribute("Font", pageBlock.TextObject.Font.Id.ToString()), new XAttribute("Size", pageBlock.TextObject.Size));
                            textObjectElement.CreateRequiredElement("FillColor", new XAttribute("Value", pageBlock.TextObject.FillColor.Value.ToString()));
                            textObjectElement.CreateRequiredElement("TextCode", pageBlock.TextObject.TextCode.Value, new XAttribute("X", pageBlock.TextObject.TextCode.X), new XAttribute("Y", pageBlock.TextObject.TextCode.Y), new XAttribute("DeltaX", pageBlock.TextObject.TextCode.DeltaX));
                            layerElement.Add(textObjectElement);
                        }
                    }
                    contentElement.Add(layerElement);
                }
                pageElement.Add(contentElement);
                _templateElements.Add(pageElement);
            }
        }

        public void WriteCustomerTag(List<CustomTag> customTags, XElement content)
        {
            _customerTagsElement = new XElement(ConstDefined.OfdNamespace + "CustomTags", new XAttribute(XNamespace.Xmlns + "ofd", ConstDefined.OfdXmlns));
            foreach (var tg in customTags)
            {
                XElement customerTagElement = new XElement(ConstDefined.OfdNamespace + "CustomTag");
                customerTagElement.Add(new XAttribute("TypeID", tg.TypeId));
                customerTagElement.Add(new XElement(ConstDefined.OfdNamespace + "FileLoc", tg.FileLoc));
                _customerTagsElement.Add(customerTagElement);
            }
            //tags内容
            _customTagElements.Add(content);
        }

        public void WriteAnnotation()
        {
            _annotationsElement = new XElement(ConstDefined.OfdNamespace + "Annotations", new XAttribute(XNamespace.Xmlns + "ofd", ConstDefined.OfdXmlns));
            AnnotationInfo annotationInfo = new AnnotationInfo
            {
                Type = AnnotationType.Stamp,
                Id = new Id(93),
                Subtype = "SignatureInFile",
                Parameters = new List<Parameter>
                {
                    new Parameter
                    {
                        Name = "fp.NativeSign",
                        Value = "original_invoice"
                    }
                },
                Appearance = new PageBlock
                {
                    PathObject = new Primitives.Graph.CtPath
                    {
                        Boundary = new Box(4.5, 104, 115, 20)
                    }
                }
            };
            _annotationsElement.Add(new XElement(ConstDefined.OfdNamespace + "Page", new XAttribute("PageID", "1"), new XElement(ConstDefined.OfdNamespace + "FileLoc", "Page_0/Annotation.xml")));

            _annotationElement = new XElement(ConstDefined.OfdNamespace + "PageAnnot", new XAttribute(XNamespace.Xmlns + "ofd", ConstDefined.OfdXmlns));
            XElement annotElement = new XElement(ConstDefined.OfdNamespace + "Annot");
            annotElement.Add(new XAttribute("Type", annotationInfo.Type));
            annotElement.Add(new XAttribute("ID", annotationInfo.Id));
            annotElement.Add(new XAttribute("Subtype", annotationInfo.Subtype));

            XElement parametersElement = new XElement(ConstDefined.OfdNamespace + "Parameters");
            foreach (var p in annotationInfo.Parameters)
            {
                parametersElement.Add(new XElement(ConstDefined.OfdNamespace + "Parameter", p.Value, new XAttribute("Name", p.Name)));
            }

            annotElement.Add(parametersElement);
            annotElement.Add(new XElement(ConstDefined.OfdNamespace + "Appearance", new XAttribute("Boundary", annotationInfo.Appearance.PathObject.Boundary)));
            _annotationElement.Add(annotElement);
        }

        public OfdWriter WriteCert(CipherKeyPair sealKey, CipherKeyPair signerKey)
        {
            //印章
            _sealKey = sealKey;
            _sealCert = Sm2Utils.MakeCert(sealKey.PublicKey, sealKey.PrivateKey, "yzw", "tax");

            //签章者
            _signerKey = signerKey;
            _signerCert = Sm2Utils.MakeCert(signerKey.PublicKey, signerKey.PrivateKey, "yzw", "tax");
            return this;
        }

        public void WriteSignature()
        {
            _signaturesElement = new XElement(ConstDefined.OfdNamespace + "Signatures", new XAttribute(XNamespace.Xmlns + "ofd", ConstDefined.OfdXmlns));
            _signaturesElement.Add(new XElement(ConstDefined.OfdNamespace + "MaxSignId", "2"));
            _signaturesElement.Add(new XElement(ConstDefined.OfdNamespace + "Signature", new XAttribute("ID", 2), new XAttribute("BaseLoc", "/Doc_0/Signs/Sign_0/Signature.xml")));

            _signatureElement = new XElement(ConstDefined.OfdNamespace + "Signature", new XAttribute(XNamespace.Xmlns + "ofd", ConstDefined.OfdXmlns), new XAttribute("ID", 2), new XAttribute("Type", "Seal"));

            string c = _annotationElement.GetElementContentTest();

            SignedInfo signedInfo = new SignedInfo
            {
                Provider = new Provider { Company = "gomain", ProviderName = "gomain_eseal", Version = "2.0" },
                SignatureMethod = SesSigner.SignatureMethod.Id,
                SignatureDateTime = "20220124060837.228Z",
                ReferenceCollect = new ReferenceCollect
                {
                    CheckMethod = SesSigner.DigestMethod.Id,
                    Items = new List<Primitives.Signatures.Reference>
                    {
                        new Primitives.Signatures.Reference
                        {
                            CheckValue = new CheckValue
                            {
                                Value= Convert.ToBase64String(Sm2Utils.Digest (c,Encoding.UTF8)),
                            },
                            FileRef="/Doc_0/Annots/Page_0/Annotation.xml"
                        }
                    }
                },
                StampAnnot = new StampAnnot()
                {
                    Id = new Id(1),
                    PageRef = new RefId { Id = new Id(1) },
                    Boundary = new Box(90.00, 8.00, 30.00, 20.00)
                },
                Seal = new Seal
                {
                    BaseLoc = new Location
                    {
                        Value = "Doc_0/Signs/Sign_0/SignedValue.dat"
                    }
                }
            };
            DigestInfo digestInfo = new DigestInfo
            {
                SignedInfo = signedInfo,
                SignedValue = "/Doc_0/Signs/Sign_0/SignedValue.dat"
            };
            var signedInfoElement = new XElement(ConstDefined.OfdNamespace + "SignedInfo");
            signedInfoElement.Add(new XElement(ConstDefined.OfdNamespace + "Provider", new XAttribute("ProviderName", signedInfo.Provider.ProviderName), new XAttribute("Version", signedInfo.Provider.Version), new XAttribute("Company", signedInfo.Provider.Company)));
            signedInfoElement.Add(new XElement(ConstDefined.OfdNamespace + "SignatureMethod", signedInfo.SignatureMethod));
            signedInfoElement.Add(new XElement(ConstDefined.OfdNamespace + "SignatureDateTime", signedInfo.SignatureDateTime));

            XElement referencesElement = new XElement(ConstDefined.OfdNamespace + "References", new XAttribute("CheckMethod", signedInfo.ReferenceCollect.CheckMethod));
            foreach (var reference in signedInfo.ReferenceCollect.Items)
            {
                XElement referenceElement = new XElement(ConstDefined.OfdNamespace + "Reference", new XAttribute("FileRef", reference.FileRef));
                referenceElement.Add(new XElement(ConstDefined.OfdNamespace + "CheckValue", reference.CheckValue.Value));
                referencesElement.Add(referenceElement);
            }
            signedInfoElement.Add(referencesElement);
            signedInfoElement.Add(new XElement(ConstDefined.OfdNamespace + "StampAnnot", new XAttribute("ID", signedInfo.StampAnnot.Id), new XAttribute("PageRef", signedInfo.StampAnnot.PageRef), new XAttribute("Boundary", signedInfo.StampAnnot.Boundary)));

            _signatureElement.Add(signedInfoElement);
            _signatureElement.Add(new XElement(ConstDefined.OfdNamespace + "SignedValue", digestInfo.SignedValue));
        }

        /// <summary>
        /// 创建签章签名值文件测试
        /// </summary>
        private byte[] CreateSignedValueDataFileContent()
        {
            string s = _signatureElement.GetElementContentTest();
            //SesSignatureInfo t = new SesSignatureInfo
            //{
            //    dataHash = Sm2Utils.Digest(s, Encoding.UTF8),
            //    PropertyInfo = "/Doc_0/Signs/Sign_0/Signature.xml",
            //    manufacturer = "GOMAIN",
            //    sealName = "测试全国统一发票监制章国家税务总局重庆市税务局",
            //    esId = "50011200000001",
            //    sealCert = _sealCert.GetEncoded(),
            //    sealPrivateKey = _sealKey.PrivateKey,
            //    sealPicture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Files", "image_78.jb2")),
            //    sealType = "ofd",
            //    sealWidth = 30,
            //    sealHeigth = 20,
            //    signature = Sm2Utils.Sign(_signerKey.PrivateKey, s),
            //    signerCert = _signerCert.GetEncoded()
            //};
            SesSealConfig config = new SesSealConfig
            {
                manufacturer = "GOMAIN",
                sealName = "测试全国统一发票监制章国家税务总局重庆市税务局",
                esId = "50011200000001",
                sealCert = _sealCert.GetEncoded(),
                sealPrivateKey = _sealKey.PrivateKey,
                sealPicture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Files", "image_78.jb2")),
                sealType = "ofd",
                sealWidth = 30,
                sealHeigth = 20,
                signerPrivateKey = _signerKey.PrivateKey,
                signerCert = _signerCert.GetEncoded()
            };

            return new SesSigner(config).Sign(Encoding.UTF8.GetBytes(s), "/Doc_0/Signs/Sign_0/Signature.xml");


            //return Sm2Utils.CreateSignedValueData(t);
        }

        public byte[] Flush()
        {
            var stream = new MemoryStream();
            ZipArchive zipArchive = new ZipArchive(stream, ZipArchiveMode.Create, true);
            try
            {
                zipArchive.CreateEntry("OFD.xml", _rootElement, _isFormat);
                zipArchive.CreateEntry("Doc_0/Document.xml", _documentElement, _isFormat);
                zipArchive.CreateEntry("Doc_0/DocumentRes.xml", _documentResElement, _isFormat);
                zipArchive.CreateEntry("Doc_0/PublicRes.xml", _publicResElement, _isFormat);
                zipArchive.CreateEntry("Doc_0/Pages/Page_0/Content.xml", _pageElement, _isFormat);
                ParseAttachment();
                zipArchive.CreateEntry("Doc_0/Attachs/Attachments.xml", _attachmentElement, _isFormat);
                foreach (var attachment in _attachments)
                {
                    XElement attachmentElement = _attachmentElements.ElementAt(_attachments.IndexOf(attachment));
                    zipArchive.CreateEntry("Doc_0/Attachs/" + attachment.FileLoc.Value, attachmentElement, _isFormat);
                }
                DocumentResource res = _ofdDocument.GetDocumentResource();
                foreach (var multiMedia in res.MultiMedias)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "Files", multiMedia.MediaFile.Value);
                    if (res.MultiMedias.IndexOf(multiMedia) == 0)
                    {
                        byte[] fileBytes = File.ReadAllBytes(path);
                        zipArchive.CreateEntry("Doc_0/Res/" + multiMedia.MediaFile.Value, fileBytes);
                    }
                    else
                    {
                        using (var fileStream = new FileStream(path, FileMode.Open))
                        {
                            zipArchive.CreateEntry("Doc_0/Res/" + multiMedia.MediaFile.Value, fileStream);
                        }
                    }
                }
                foreach (var tplElement in _templateElements)
                {
                    zipArchive.CreateEntry("Doc_0/Tpls/Tpls_" + _templateElements.IndexOf(tplElement) + "/Content.xml", tplElement, _isFormat);
                }

                zipArchive.CreateEntry("Doc_0/Tags/CustomTags.xml", _customerTagsElement, _isFormat);
                foreach (var tg in _customTagElements)
                {
                    zipArchive.CreateEntry("Doc_0/Tags/CustomTag.xml", tg, _isFormat);
                }

                zipArchive.CreateEntry("Doc_0/Annots/Annotations.xml", _annotationsElement, _isFormat);
                zipArchive.CreateEntry("Doc_0/Annots/Page_0/Annotation.xml", _annotationElement, _isFormat);

                zipArchive.CreateEntry("Doc_0/Signs/Signatures.xml", _signaturesElement, _isFormat);
                zipArchive.CreateEntry("Doc_0/Signs/Sign_0/Signature.xml", _signatureElement, _isFormat);

                byte[] signedFile = CreateSignedValueDataFileContent();
                zipArchive.CreateEntry("Doc_0/Signs/Sign_0/SignedValue.dat", signedFile);
            }
            finally
            {
                zipArchive.Dispose(); //LeaveOpen为true是，释放该资源，stream才不是空
                stream.Seek(0, SeekOrigin.Begin);
                stream.Flush(); //强制刷新缓冲区 这句话很关键
            }
            //using (MemoryStream compressStream = new MemoryStream())
            //{
            //    using (DeflateStream deflateStream = new DeflateStream(compressStream, CompressionLevel.Optimal))
            //    {
            //        stream.CopyTo(deflateStream);
            //        deflateStream.Close();
            //        return compressStream.ToArray();
            //    }
            //}
            return stream.ToArray();
        }
    }

    public static class OfdWriterExtensions
    {
        /// <summary>
        /// 默认xml声明
        /// </summary>
        private static readonly XDeclaration DefaultDeclaration = new XDeclaration("1.0", "UTF-8", null);

        /// <summary>
        /// 创建压缩项
        /// </summary>
        /// <param name="zipArchive"></param>
        /// <param name="entryName"></param>
        /// <param name="content"></param>
        public static void CreateEntry(this ZipArchive zipArchive, string entryName, XElement content, bool isFormatting = false)
        {
            if (content == null)
            {
                return;
            }

            ZipArchiveEntry ofd = zipArchive.CreateEntry(entryName);
            using (StreamWriter sw = new StreamWriter(ofd.Open()))
            {
                sw.WriteLine(DefaultDeclaration.ToString());
                if (isFormatting)
                {
                    sw.WriteLine(content.ToString());
                }
                else
                {
                    sw.WriteLine(content.ToString(SaveOptions.DisableFormatting));
                }
            }
        }

        /// <summary>
        /// 创建压缩项
        /// </summary>
        /// <param name="zipArchive"></param>
        /// <param name="entryName"></param>
        /// <param name="content"></param>
        public static void CreateEntry(this ZipArchive zipArchive, string entryName, Stream content)
        {
            if (content == null)
            {
                return;
            }

            ZipArchiveEntry ofd = zipArchive.CreateEntry(entryName);
            byte[] input = content.ReadToEnd();
            using (BinaryWriter writer = new BinaryWriter(ofd.Open()))
            {
                writer.Write(input);
                writer.Flush();
            }
        }

        /// <summary>
        /// 创建压缩项
        /// </summary>
        /// <param name="zipArchive"></param>
        /// <param name="entryName"></param>
        /// <param name="content"></param>
        public static void CreateEntry(this ZipArchive zipArchive, string entryName, byte[] content)
        {
            if (content == null || content.Length == 0)
            {
                return;
            }

            ZipArchiveEntry ofd = zipArchive.CreateEntry(entryName);
            using (BinaryWriter writer = new BinaryWriter(ofd.Open()))
            {
                writer.Write(content);
                writer.Flush();
            }
        }

        public static string GetElementContentTest(this XElement e)
        {
            StringBuilder b = new StringBuilder();
            b.AppendLine(DefaultDeclaration.ToString());
            b.AppendLine(e.ToString(SaveOptions.DisableFormatting));
            return b.ToString();
        }
    }
}