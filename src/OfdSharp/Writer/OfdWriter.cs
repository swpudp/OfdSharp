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
using OfdSharp.Primitives.Signatures;

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
        private List<XElement> _pageElements = new List<XElement>();
        private XElement _attachmentsElement;
        private XElement _customerTagsElement;
        private XElement _annotationsElement;
        private XElement _signaturesElement;
        private XElement _signatureElement;
        //模板
        private List<XElement> _templateElements = new List<XElement>();
        private List<XElement> _customTagElements = new List<XElement>();
        private XElement _annotationElement;
        private byte[] _signedValue;

        public OfdWriter(OfdDocument ofdDocument, bool isFormating)
        {
            _ofdDocument = ofdDocument;
            _isFormat = isFormating;
        }

        /// <summary>
        /// 入口文件写入
        /// </summary>
        /// <param name="ofdRoot">文档描述信息</param>
        public void WriteOfdRoot(OfdRoot ofdRoot)
        {
            XElement docInfoElement = XmlExtension.CreateElement("DocInfo");
            var docInfo = ofdRoot.DocBody.DocInfo;
            docInfoElement.AddOptionalElement("DocID", docInfo.DocId);
            docInfoElement.AddOptionalElement("Title", docInfo.Title);
            docInfoElement.AddOptionalElement("Author", docInfo.Author);
            docInfoElement.AddOptionalElement("Subject", docInfo.Subject);
            docInfoElement.AddOptionalElement("Abstract", docInfo.Abstract);
            docInfoElement.AddOptionalElement("CreationDate", docInfo.CreationDate);
            docInfoElement.AddOptionalElement("ModDate", docInfo.ModDate);
            docInfoElement.AddOptionalElement("DocUsage", docInfo.DocUsage);
            docInfoElement.AddOptionalElement("Cover", docInfo.Cover);
            if (docInfo.Keywords.IsAny())
            {
                XElement keywordsElement = XmlExtension.CreateElement("Keywords");
                docInfo.Keywords.ForEach(item => keywordsElement.AddRequiredElement("Keyword", item));
                docInfoElement.Add(keywordsElement);
            }
            docInfoElement.AddOptionalElement("Creator", docInfo.Creator);
            docInfoElement.AddOptionalElement("CreatorVersion", docInfo.CreatorVersion);
            if (docInfo.CustomDataList.IsAny())
            {
                XElement customDataElement = XmlExtension.CreateElement("CustomDatas");
                docInfo.CustomDataList.ForEach(item => customDataElement.AddRequiredElement("CustomData", item.Value, new XAttribute("Name", item.Name)));
                docInfoElement.Add(customDataElement);
            }
            XElement docBody = XmlExtension.CreateElement("DocBody");
            docBody.Add(docInfoElement);

            docBody.AddOptionalElement("DocRoot", ofdRoot.DocBody.DocRoot?.Value);
            docBody.AddOptionalElement("Signatures", ofdRoot.DocBody.Signatures?.Value);
            _rootElement = XmlExtension.CreateElement("OFD", docBody, new XAttribute(XNamespace.Xmlns + ConstDefined.OfdPrefix, ConstDefined.OfdXmlns), new XAttribute("DocType", ConstDefined.OfdDocType), new XAttribute("Version", ConstDefined.OfdVersion));
        }

        public void WriteDocument(CtDocument ctDocument)
        {
            //CommonData
            XElement commonDataElement = XmlExtension.CreateElement("CommonData");
            commonDataElement.AddRequiredElement("MaxUnitID", ctDocument.CommonData.MaxUnitId);
            if (ctDocument.CommonData.TemplatePages.IsAny())
            {
                ctDocument.CommonData.TemplatePages.ForEach((item) => commonDataElement.AddRequiredElement("TemplatePage", new XAttribute("ID", item.Id), new XAttribute("BaseLoc", item.BaseLoc.Value)));
            }
            commonDataElement.AddRequiredElement("PageArea", XmlExtension.CreateElement("PhysicalBox", ctDocument.CommonData.PageArea.Physical.ToString()));
            commonDataElement.AddOptionalElement("DocumentRes", ctDocument.CommonData.DocumentRes?.Value);
            commonDataElement.AddOptionalElement("PublicRes", ctDocument.CommonData.PublicRes?.Value);
            _documentElement = XmlExtension.CreateElement("Document", new XAttribute(XNamespace.Xmlns + ConstDefined.OfdPrefix, ConstDefined.OfdXmlns));
            _documentElement.Add(commonDataElement);

            //Pages
            XElement pagesElement = XmlExtension.CreateElement("Pages");
            foreach (var pg in ctDocument.Pages)
            {
                pagesElement.AddRequiredElement("Page", new XAttribute("ID", pg.Id), new XAttribute("BaseLoc", pg.BaseLoc.Value));
            }
            _documentElement.Add(pagesElement);

            //Annotations
            if (ctDocument.Annotations.IsAny())
            {
                _documentElement.AddRequiredElement("Annotations", "Annots/Annotations.xml");
            }
            //Attachments
            if (ctDocument.Attachments.IsAny())
            {
                _documentElement.AddRequiredElement("Attachments", "Attachs/Attachments.xml");
            }
            //CustomTags
            if (ctDocument.CustomTags.IsAny())
            {
                _documentElement.AddRequiredElement("CustomTags", "Tags/CustomTags.xml");
            }
        }

        public void WriteDocumentRes(DocumentResource res)
        {
            _documentResElement = XmlExtension.CreateElement("Res", new XAttribute(XNamespace.Xmlns + ConstDefined.OfdPrefix, ConstDefined.OfdXmlns), new XAttribute("BaseLoc", res.BaseLoc.Value));
            if (res.DrawParams.IsAny())
            {
                //DrawParams
                XElement drawParams = XmlExtension.CreateElement("DrawParams");

                foreach (var item in res.DrawParams)
                {
                    XElement drawParam = XmlExtension.CreateElement("DrawParam", new XAttribute("ID", item.Id), new XAttribute("LineWidth", item.LineWidth));

                    if (item.FillColor != null)
                    {
                        drawParam.AddRequiredElement("FillColor", new XAttribute("Value", item.FillColor.Value), new XAttribute("ColorSpace", item.FillColor.ColorSpace));
                    }
                    if (item.StrokeColor != null)
                    {
                        drawParam.AddRequiredElement("StrokeColor", new XAttribute("Value", item.StrokeColor.Value), new XAttribute("ColorSpace", item.StrokeColor.ColorSpace));
                    }
                    drawParams.Add(drawParam);
                }
                _documentResElement.Add(drawParams);
            }
            //MultiMedias
            if (res.MultiMedias.IsAny())
            {
                XElement multiMedias = XmlExtension.CreateElement("MultiMedias");
                foreach (var ctMultiMedia in res.MultiMedias)
                {
                    XElement multiMedia = XmlExtension.CreateElement("MultiMedia", new XAttribute("ID", ctMultiMedia.Id), new XAttribute("Type", ctMultiMedia.Type), new XAttribute("Format", ctMultiMedia.Format));
                    multiMedia.AddRequiredElement("MediaFile", ctMultiMedia.MediaFile.Value);
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
                    colorSpacesElement.AddRequiredElement("ColorSpace", new XAttribute("ID", colorSpace.Id), new XAttribute("Type", colorSpace.Type), new XAttribute("BitsPerComponent", colorSpace.BitsPerComponent));
                }
                _publicResElement.Add(colorSpacesElement);
            }
            if (res.Fonts.IsAny())
            {
                XElement fontsElement = XmlExtension.CreateElement("Fonts");
                foreach (var ctFont in res.Fonts)
                {
                    fontsElement.AddRequiredElement("Font", new XAttribute("ID", ctFont.Id.ToString()), new XAttribute("FontName", ctFont.FontName), new XAttribute("FamilyName", ctFont.FamilyName));
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
            var pageElement = XmlExtension.CreateElement("Page", new XAttribute(XNamespace.Xmlns + ConstDefined.OfdPrefix, ConstDefined.OfdXmlns));
            foreach (var po in pageObjects)
            {
                pageElement.AddRequiredElement("Area", XmlExtension.CreateElement("PhysicalBox", po.Area.Physical.ToString()));
                if (po.Template != null)
                {
                    pageElement.AddRequiredElement("Template", new XAttribute("TemplateID", po.Template.TemplateId), new XAttribute("ZOrder", po.Template.ZOrder));
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
                            textObjectElement.AddRequiredElement("FillColor", new XAttribute("Value", pb.TextObject.FillColor.Value.ToString()));

                            XElement textElement = XmlExtension.CreateElement("TextCode", pb.TextObject.TextCode.Value);
                            textElement.Add(new XAttribute("X", pb.TextObject.TextCode.X), new XAttribute("Y", pb.TextObject.TextCode.Y));
                            if (pb.TextObject.TextCode.DeltaX != null)
                            {
                                textElement.Add(new XAttribute("DeltaX", pb.TextObject.TextCode.DeltaX));
                            }
                            if (pb.TextObject.TextCode.DeltaY != null)
                            {
                                textElement.Add(new XAttribute("DeltaY", pb.TextObject.TextCode.DeltaY));
                            }
                            textObjectElement.Add(textElement);
                            layerElement.Add(textObjectElement);
                        }
                        if (pb.PathObject != null)
                        {
                            XElement pathObjectElement = XmlExtension.CreateElement("PathObject", new XAttribute("ID", pb.PathObject.Id), new XAttribute("Boundary", pb.PathObject.Boundary.ToString()), new XAttribute("LineWidth", pb.PathObject.LineWidth));
                            pathObjectElement.AddRequiredElement("StrokeColor", new XAttribute("Value", pb.PathObject.StrokeColor.Value.ToString()));
                            pathObjectElement.AddRequiredElement("AbbreviatedData", pb.PathObject.AbbreviatedData);
                            layerElement.Add(pathObjectElement);
                        }
                        if (pb.ImageObject != null)
                        {
                            layerElement.AddRequiredElement("ImageObject", new XAttribute("ID", pb.ImageObject.Id), new XAttribute("Boundary", pb.ImageObject.Boundary), new XAttribute("CTM", pb.ImageObject.Ctm), new XAttribute("ResourceID", pb.ImageObject.ResourceId));
                        }
                    }
                    contentElement.Add(layerElement);
                }
                pageElement.Add(contentElement);
            }
            _pageElements.Add(pageElement);
        }

        /// <summary>
        /// 写入附件信息
        /// </summary>
        private void ParseAttachment()
        {
            if (!_ofdDocument.Attachments.Any())
            {
                return;
            }
            _attachmentsElement = XmlExtension.CreateElement("Attachments", new XAttribute(XNamespace.Xmlns + ConstDefined.OfdPrefix, ConstDefined.OfdXmlns));
            foreach (var attachment in _ofdDocument.Attachments)
            {
                XElement newAttachment = XmlExtension.CreateElement("Attachment", new XAttribute("ID", attachment.Id), new XAttribute("Name", attachment.Name), new XAttribute("Format", attachment.Format), new XAttribute("Visible", attachment.Visible));
                newAttachment.AddRequiredElement("FileLoc", attachment.FileLoc.Value);
                _attachmentsElement.Add(newAttachment);
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
                            pathElement.AddRequiredElement("FillColor", new XAttribute("Value", pageBlock.PathObject.FillColor.Value), new XAttribute("ColorSpace", pageBlock.PathObject.FillColor.ColorSpace));
                            pathElement.AddRequiredElement("StrokeColor", new XAttribute("Value", pageBlock.PathObject.StrokeColor.Value), new XAttribute("ColorSpace", pageBlock.PathObject.StrokeColor.ColorSpace));
                            pathElement.AddRequiredElement("AbbreviatedData", new XAttribute("Value", pageBlock.PathObject.AbbreviatedData));
                            layerElement.Add(pathElement);
                        }

                        if (pageBlock.TextObject != null)
                        {
                            XElement textObjectElement = XmlExtension.CreateElement("TextObject", new XAttribute("ID", pageBlock.TextObject.Id.ToString()), new XAttribute("Boundary", pageBlock.TextObject.Boundary.ToString()), new XAttribute("Font", pageBlock.TextObject.Font.Id.ToString()), new XAttribute("Size", pageBlock.TextObject.Size));
                            textObjectElement.AddRequiredElement("FillColor", new XAttribute("Value", pageBlock.TextObject.FillColor.Value.ToString()));
                            textObjectElement.AddRequiredElement("TextCode", pageBlock.TextObject.TextCode.Value, new XAttribute("X", pageBlock.TextObject.TextCode.X), new XAttribute("Y", pageBlock.TextObject.TextCode.Y), new XAttribute("DeltaX", pageBlock.TextObject.TextCode.DeltaX));
                            layerElement.Add(textObjectElement);
                        }
                    }
                    contentElement.Add(layerElement);
                }
                pageElement.Add(contentElement);
                _templateElements.Add(pageElement);
            }
        }

        public void WriteCustomerTag(CustomTag customTag, XElement content)
        {
            _customerTagsElement = XmlExtension.CreateElement("CustomTags", new XAttribute(XNamespace.Xmlns + ConstDefined.OfdPrefix, ConstDefined.OfdXmlns));

            XElement customerTagElement = XmlExtension.CreateElement("CustomTag", new XAttribute("TypeID", customTag.TypeId));
            customerTagElement.AddRequiredElement("FileLoc", customTag.FileLoc.Value);
            _customerTagsElement.Add(customerTagElement);

            //tags内容
            _customTagElements.Add(content);
        }

        public void WriteAnnotation(AnnotationInfo annotationInfo, RefPage refPage)
        {
            _annotationsElement = XmlExtension.CreateElement("Annotations", new XAttribute(XNamespace.Xmlns + ConstDefined.OfdPrefix, ConstDefined.OfdXmlns));

            XElement pageElement = XmlExtension.CreateElement("Page", new XAttribute("PageID", refPage.PageId));
            pageElement.AddRequiredElement("FileLoc", refPage.FileLoc.Value);
            _annotationsElement.Add(pageElement);

            _annotationElement = XmlExtension.CreateElement("PageAnnot", new XAttribute(XNamespace.Xmlns + ConstDefined.OfdPrefix, ConstDefined.OfdXmlns));
            XElement annotElement = XmlExtension.CreateElement("Annot");
            annotElement.Add(new XAttribute("Type", annotationInfo.Type));
            annotElement.Add(new XAttribute("ID", annotationInfo.Id));
            annotElement.Add(new XAttribute("Subtype", annotationInfo.Subtype ?? "None"));

            XElement parametersElement = XmlExtension.CreateElement("Parameters");
            foreach (var p in annotationInfo.Parameters)
            {
                parametersElement.AddRequiredElement("Parameter", p.Value, new XAttribute("Name", p.Name));
            }
            annotElement.Add(parametersElement);
            //todo PathObject，TextObject等各种对象写入没有完成
            if (annotationInfo.Appearance?.PathObject != null)
            {
                annotElement.AddRequiredElement("Appearance", new XAttribute("Boundary", annotationInfo.Appearance.PathObject.Boundary));
                _annotationElement.Add(annotElement);
            }
        }

        public void WriteSignature(SignedInfo signedInfo)
        {
            //todo SignatureCollect在哪里定义比较合适？
            SignatureCollect signatureCollect = new SignatureCollect
            {
                MaxSignId = new Id(100),
                Signatures = new List<SignatureInfo>
                {
                    new SignatureInfo
                    {
                        BaseLoc = "/Doc_0/Signs/Sign_0/Signature.xml",
                        Id = "100"
                    }
                }
            };
            _signaturesElement = XmlExtension.CreateElement("Signatures", new XAttribute(XNamespace.Xmlns + ConstDefined.OfdPrefix, ConstDefined.OfdXmlns));
            _signaturesElement.AddRequiredElement("MaxSignId", signatureCollect.MaxSignId);
            foreach (var signature in signatureCollect.Signatures)
            {
                _signaturesElement.AddRequiredElement("Signature", new XAttribute("ID", signature.Id), new XAttribute("BaseLoc", signature.BaseLoc));
            }

            signedInfo.ReferenceCollect.Items.Add(new Reference { CheckValue = new CheckValue { Value = _rootElement.GetDigest() }, FileRef = "/" + ConstDefined.OfdRootFileName });
            signedInfo.ReferenceCollect.Items.Add(new Reference { CheckValue = new CheckValue { Value = _documentElement.GetDigest() }, FileRef = "/Doc_0/Document.xml" });
            signedInfo.ReferenceCollect.Items.Add(new Reference { CheckValue = new CheckValue { Value = _documentResElement.GetDigest() }, FileRef = "/Doc_0/DocumentRes.xml" });
            signedInfo.ReferenceCollect.Items.Add(new Reference { CheckValue = new CheckValue { Value = _publicResElement.GetDigest() }, FileRef = "/Doc_0/PublicRes.xml" });
            if (_ofdDocument.Attachments.IsAny())
            {
                signedInfo.ReferenceCollect.Items.Add(new Reference { CheckValue = new CheckValue { Value = _attachmentsElement.GetDigest() }, FileRef = "/Doc_0/Attachs/Attachments.xml" });
            }
            signedInfo.ReferenceCollect.Items.Add(new Reference { CheckValue = new CheckValue { Value = _customerTagsElement.GetDigest() }, FileRef = "/Doc_0/Tags/CustomTags.xml" });
            signedInfo.ReferenceCollect.Items.Add(new Reference { CheckValue = new CheckValue { Value = _annotationsElement.GetDigest() }, FileRef = "/Doc_0/Annots/Annotations.xml" });
            signedInfo.ReferenceCollect.Items.Add(new Reference { CheckValue = new CheckValue { Value = _signaturesElement.GetDigest() }, FileRef = "/Doc_0/Signs/Signatures.xml" });
            signedInfo.ReferenceCollect.Items.Add(new Reference { CheckValue = new CheckValue { Value = _annotationElement.GetDigest() }, FileRef = "/Doc_0/Annots/Page_0/Annotation.xml" });

            foreach (var p in _pageElements)
            {
                signedInfo.ReferenceCollect.Items.Add(new Reference { CheckValue = new CheckValue { Value = p.GetDigest() }, FileRef = $"/Doc_0/Pages/Page_{_pageElements.IndexOf(p)}/Content.xml" });
            }
            foreach (var tpl in _templateElements)
            {
                signedInfo.ReferenceCollect.Items.Add(new Reference { CheckValue = new CheckValue { Value = tpl.GetDigest() }, FileRef = $"/Doc_0/Tpls/Tpl_{_templateElements.IndexOf(tpl)}/Content.xml" });
            }
            foreach (var tg in _customTagElements)
            {
                signedInfo.ReferenceCollect.Items.Add(new Reference { CheckValue = new CheckValue { Value = tg.GetDigest() }, FileRef = $"/Doc_0/Tags/CustomTag.xml" });
            }
            //List<XElement> xElements = new List<XElement>
            //{
            //    _rootElement,
            //    _documentElement,
            //    _documentResElement,
            //    _publicResElement,
            //    _attachmentElement,
            //    _customerTagsElement,
            //    _annotationsElement,
            //    _signaturesElement,
            //    _annotationElement
            //};
            //xElements.AddRange(_pageElements);
            //xElements.AddRange(_templateElements);
            //xElements.AddRange(_customTagElements);
            //todo 统一处理摘要，路径上哪里去找？
            //foreach (var e in xElements)
            //{
            //    Reference reference = new Reference { FileRef = , CheckValue = new CheckValue { Value = e.GetDigest() } };
            //    signedInfo.ReferenceCollect.Items.Add(reference);
            //}

            //todo 未完成附件的摘要计算
            foreach (Attachment attach in _ofdDocument.Attachments)
            {

            }
            foreach (var attachElement in _ofdDocument.AttachmentElements)
            {
                Reference reference = new Reference
                {
                    FileRef = _ofdDocument.Attachments[_ofdDocument.AttachmentElements.IndexOf(attachElement)].FileLoc.Value,
                    CheckValue = new CheckValue { Value = attachElement.GetDigest() }
                };
                signedInfo.ReferenceCollect.Items.Add(reference);
            }
            //todo 需要new XAttribute("ID", signedInfo.), new XAttribute("Type", "Seal")？
            _signatureElement = XmlExtension.CreateElement("Signature", new XAttribute(XNamespace.Xmlns + ConstDefined.OfdPrefix, ConstDefined.OfdXmlns));
            var signedInfoElement = XmlExtension.CreateElement("SignedInfo");
            signedInfoElement.AddRequiredElement("Provider", new XAttribute("ProviderName", signedInfo.Provider.ProviderName), new XAttribute("Version", signedInfo.Provider.Version), new XAttribute("Company", signedInfo.Provider.Company));
            signedInfoElement.AddRequiredElement("SignatureMethod", signedInfo.SignatureMethod);
            signedInfoElement.AddRequiredElement("SignatureDateTime", signedInfo.SignatureDateTime);

            XElement referencesElement = XmlExtension.CreateElement("References", new XAttribute("CheckMethod", signedInfo.ReferenceCollect.CheckMethod));
            foreach (var reference in signedInfo.ReferenceCollect.Items)
            {
                XElement referenceElement = XmlExtension.CreateElement("Reference", new XAttribute("FileRef", reference.FileRef));
                referenceElement.AddRequiredElement("CheckValue", reference.CheckValue.Value);
                referencesElement.Add(referenceElement);
            }
            signedInfoElement.Add(referencesElement);
            signedInfoElement.AddRequiredElement("StampAnnot", new XAttribute("ID", signedInfo.StampAnnot.Id), new XAttribute("PageRef", signedInfo.StampAnnot.PageRef), new XAttribute("Boundary", signedInfo.StampAnnot.Boundary));

            _signatureElement.Add(signedInfoElement);
            //todo DigestInfo在哪里实例化比较合适？
            DigestInfo digestInfo = new DigestInfo
            {
                SignedInfo = signedInfo,
                SignedValue = "/Doc_0/Signs/Sign_0/SignedValue.dat"
            };
            _signatureElement.AddRequiredElement("SignedValue", digestInfo.SignedValue);
        }

        /// <summary>
        /// 创建签章签名值文件测试
        /// </summary>
        public void ExecuteSign(SesSealConfig sesSealConfig)
        {
            string content = _signatureElement.GetContent();
            _signedValue = new SesSigner(sesSealConfig).Sign(content, "/Doc_0/Signs/Sign_0/Signature.xml");
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
                foreach (var p in _pageElements)
                {
                    zipArchive.CreateEntry($"Doc_0/Pages/Page_{_pageElements.IndexOf(p)}/Content.xml", p, _isFormat);
                }
                ParseAttachment();
                zipArchive.CreateEntry("Doc_0/Attachs/Attachments.xml", _attachmentsElement, _isFormat);

                foreach (var attachment in _ofdDocument.Attachments)
                {
                    XElement attachmentElement = _ofdDocument.AttachmentElements.ElementAt(_ofdDocument.Attachments.IndexOf(attachment));
                    zipArchive.CreateEntry("Doc_0/Attachs/" + attachment.FileLoc.Value, attachmentElement, _isFormat);
                }
                DocumentResource res = _ofdDocument.DocumentResource;
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
                    zipArchive.CreateEntry("Doc_0/Tpls/Tpl_" + _templateElements.IndexOf(tplElement) + "/Content.xml", tplElement, _isFormat);
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

                zipArchive.CreateEntry("Doc_0/Signs/Sign_0/SignedValue.dat", _signedValue);
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
                sw.WriteLine(ConstDefined.DefaultDeclaration.ToString());
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

        public static string GetDigest(this XElement e)
        {
            return Convert.ToBase64String(Sm2Utils.Digest(e.GetContent(), Encoding.UTF8));
        }

        public static string GetContent(this XElement e)
        {
            StringBuilder b = new StringBuilder();
            b.AppendLine(ConstDefined.DefaultDeclaration.ToString());
            b.AppendLine(e.ToString(SaveOptions.DisableFormatting));
            return b.ToString();
        }
    }
}