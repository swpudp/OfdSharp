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
using Org.BouncyCastle.Asn1.GM;
using System.Text;
using System;

namespace OfdSharp.Writer
{
    public class OfdWriter
    {
        private readonly bool _isFormat;
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
        private readonly List<PageNode> _pageNodes = new List<PageNode>();
        private readonly List<PageObject> _pageObjects = new List<PageObject>();

        //附件
        private List<Attachment> _attachments = new List<Attachment>();
        private List<XElement> _attachmentElements = new List<XElement>();

        //多媒体
        private List<CtMultiMedia> _ctMultiMedias = new List<CtMultiMedia>();

        //模板
        private List<PageObject> _templatePages = new List<PageObject>();
        private List<XElement> _templateElements = new List<XElement>();

        //customerTag
        private List<CustomTag> _customTags = new List<CustomTag>();
        private List<XElement> _customTagElements = new List<XElement>();

        private XElement _annotationElement;
        private byte[] _sealCert, _signerCert;

        public OfdWriter()
        {
            _isFormat = false;
        }

        public OfdWriter(bool isFormating)
        {
            _isFormat = isFormating;
        }

        public void WriteOfdRoot()
        {
            XElement docInfo = new XElement(Const.OfdNamespace + "DocInfo");
            docInfo.Add(new XElement(Const.OfdNamespace + "DocID", "c9a4ced42f284320964bf1e630d1fa2a"));
            docInfo.Add(new XElement(Const.OfdNamespace + "Author", "China Tax"));
            docInfo.Add(new XElement(Const.OfdNamespace + "CreationDate", "2020-11-16"));
            List<CustomData> customDatas = new List<CustomData>
            {
                new CustomData {Name = "template-version", Value = "1.0.20.0422"},
                new CustomData {Name = "native-producer", Value = "SuwellFormSDK"},
                new CustomData {Name = "producer-version", Value = "1.0.20.0603"},
                new CustomData {Name = "发票代码", Value = "032001900311"},
                new CustomData {Name = "发票号码", Value = "25577301"},
                new CustomData {Name = "合计税额", Value = "***"},
                new CustomData {Name = "合计金额", Value = "181.14"},
                new CustomData {Name = "开票日期", Value = "2020年11月12日"},
                new CustomData {Name = "校验码", Value = "58569 30272 33709 75117"},
                new CustomData {Name = "购买方纳税人识别号", Value = "91510700205412308D"},
                new CustomData {Name = "销售方纳税人识别号", Value = "91320111339366503A"}
            };
            XElement customDataElement = new XElement(Const.OfdNamespace + "CustomDatas");
            foreach (var item in customDatas)
            {
                customDataElement.Add(new XElement(Const.OfdNamespace + "CustomData", new XAttribute("Name", item.Name), item.Value));
            }

            docInfo.Add(customDataElement);
            XElement docBody = new XElement(Const.OfdNamespace + "DocBody", docInfo);
            docBody.Add(new XElement(Const.OfdNamespace + "DocRoot", "Doc_0/Document.xml"));
            docBody.Add(new XElement(Const.OfdNamespace + "Signatures", "Doc_0/Signs/Signatures.xml"));
            _rootElement = new XElement(Const.OfdNamespace + "OFD", new XAttribute(XNamespace.Xmlns + "ofd", Const.OfdXmlns), new XAttribute("DocType", "OFD"), new XAttribute("Version", "1.1"), docBody);
        }

        public void WriteDocument()
        {
            XElement commonDataElement = new XElement(Const.OfdNamespace + "CommonData");
            commonDataElement.Add(new XElement(Const.OfdNamespace + "MaxUnitID", "97"));
            commonDataElement.Add(new XElement(Const.OfdNamespace + "TemplatePage", new XAttribute("ID", "2"), new XAttribute("BaseLoc", "Tpls/Tpl_0/Content.xml")));
            commonDataElement.Add(new XElement(Const.OfdNamespace + "PageArea", new XElement(Const.OfdNamespace + "PhysicalBox", "0 0 210 140")));
            commonDataElement.Add(new XElement(Const.OfdNamespace + "DocumentRes", "DocumentRes.xml"));
            commonDataElement.Add(new XElement(Const.OfdNamespace + "PublicRes", "PublicRes.xml"));

            _documentElement = new XElement(Const.OfdNamespace + "Document", new XAttribute(XNamespace.Xmlns + "ofd", Const.OfdXmlns));
            _documentElement.Add(commonDataElement);

            //Pages
            XElement pagesElement = new XElement(Const.OfdNamespace + "Pages");
            _pageObjects.Add(new PageObject
            {
                PageRes = new Location { Value = "Pages/Page_0/Content.xml" },
                Area = new Primitives.Doc.PageArea { Physical = new Box(0, 0, 210, 297) },
                Template = new Template { TemplateId = "2", ZOrder = LayerType.Background },
                Content = new Content
                {
                    Layers = new List<Layer>
                    {
                        new Layer
                        {
                            Id = new Id(303),
                            PageBlocks = new List<PageBlock>
                            {
                                new PageBlock
                                {
                                    TextObject = new CtText
                                    {
                                        Id = new Id(302),
                                        Boundary = new Box(69, 7, 72, 7.6749),
                                        Font = new RefId {Id = new Id(60)},
                                        Size = 6.61,
                                        FillColor = new Primitives.Pages.Description.Color.CtColor {Value = new Primitives.Array("156 82 35")},
                                        TextCode = new TextCode {X = 0, Y = 5.683674, DeltaX = new Primitives.Array("10 6.61"), Value = "北京增值税电子普通发票"}
                                    }
                                },
                                new PageBlock
                                {
                                    PathObject = new Primitives.Graph.Path
                                    {
                                        Id = new Id(304),
                                        Boundary = new Box(68.5, 18, 73, 0.25),
                                        LineWidth = 0.25,
                                        StrokeColor = new Primitives.Pages.Description.Color.CtColor {Value = new Primitives.Array("156 82 35")},
                                        AbbreviatedData = "M 0 0 L 73 0",
                                    }
                                },
                                new PageBlock
                                {
                                    PathObject = new Primitives.Graph.Path
                                    {
                                        Id = new Id(305),
                                        Boundary = new Box(68.5, 19, 73, 0.25),
                                        LineWidth = 0.25,
                                        StrokeColor = new Primitives.Pages.Description.Color.CtColor {Value = new Primitives.Array("156 82 35")},
                                        AbbreviatedData = "M 0 0 L 73 0",
                                    }
                                },
                                new PageBlock
                                {
                                    ImageObject = new Primitives.Image.CtImage
                                    {
                                        Id = new Id(310),
                                        Ctm = new Primitives.Array("20 0 0 20 0 0"),
                                        Boundary = new Box(8.5, 4, 20, 20),
                                        ResourceId = new RefId {Id = new Id(311)}
                                    }
                                }
                            }
                        }
                    }
                }
            });

            foreach (var pageObject in _pageObjects)
            {
                _pageNodes.Add(new PageNode { Id = new Id(1), BaseLoc = pageObject.PageRes });
            }

            foreach (var pg in _pageNodes)
            {
                pagesElement.Add(new XElement(Const.OfdNamespace + "Page", new XAttribute("ID", pg.Id.ToString()), new XAttribute("BaseLoc", pg.BaseLoc.Value)));
            }

            _documentElement.Add(pagesElement);

            //Annotations
            XElement annotationsElement = new XElement(Const.OfdNamespace + "Annotations", "Annots/Annotations.xml");
            _documentElement.Add(annotationsElement);

            //Attachments
            XElement attachmentsElement = new XElement(Const.OfdNamespace + "Attachments", "Attachs/Attachments.xml");
            _documentElement.Add(attachmentsElement);

            //CustomTags
            XElement customTagsElement = new XElement(Const.OfdNamespace + "CustomTags", "Tags/CustomTags.xml");
            _documentElement.Add(customTagsElement);
        }

        public void WriteDocumentRes()
        {
            _documentResElement = new XElement(Const.OfdNamespace + "Res", new XAttribute(XNamespace.Xmlns + "ofd", Const.OfdXmlns), new XAttribute("BaseLoc", "Res"));
            //DrawParams
            XElement drawParams = new XElement(Const.OfdNamespace + "DrawParams");
            XElement drawParam = new XElement(Const.OfdNamespace + "DrawParam", new XAttribute("ID", "4"), new XAttribute("LineWidth", "0.25"));
            drawParam.Add(new XElement(Const.OfdNamespace + "FillColor", new XAttribute("Value", "156 82 35"), new XAttribute("ColorSpace", "5")));
            drawParam.Add(new XElement(Const.OfdNamespace + "StrokeColor", new XAttribute("Value", "156 82 35"), new XAttribute("ColorSpace", "5")));
            drawParams.Add(drawParam);
            _documentResElement.Add(drawParams);

            _ctMultiMedias.Add(new CtMultiMedia { Format = "GBIG2", Type = MediaType.Image, Id = new Id(78), MediaFile = new Location { Value = "image_78.jb2" } });
            _ctMultiMedias.Add(new CtMultiMedia { Format = "GBIG2", Type = MediaType.Image, Id = new Id(79), MediaFile = new Location { Value = "image_80.jb2" } });
            //MultiMedias
            XElement multiMedias = new XElement(Const.OfdNamespace + "MultiMedias");
            foreach (var ctMultiMedia in _ctMultiMedias)
            {
                XElement multiMedia = new XElement(Const.OfdNamespace + "MultiMedia", new XAttribute("ID", ctMultiMedia.Id), new XAttribute("Type", ctMultiMedia.Type), new XAttribute("Format", ctMultiMedia.Format));
                multiMedia.Add(new XElement(Const.OfdNamespace + "MediaFile", ctMultiMedia.MediaFile.Value));
                multiMedias.Add(multiMedia);
            }

            _documentResElement.Add(multiMedias);
        }

        public void WritePublicRes()
        {
            _publicResElement = new XElement(Const.OfdNamespace + "Res", new XAttribute(XNamespace.Xmlns + "ofd", Const.OfdXmlns), new XAttribute("BaseLoc", "Res"));

            XElement colorSpaces = new XElement(Const.OfdNamespace + "ColorSpaces");
            colorSpaces.Add(new XElement(Const.OfdNamespace + "ColorSpace", new XAttribute("ID", "5"), new XAttribute("Type", "RGB"), new XAttribute("BitsPerComponent", "8")));
            _publicResElement.Add(colorSpaces);

            XElement fonts = new XElement(Const.OfdNamespace + "Fonts");
            List<CtFont> ctFonts = new List<CtFont>
            {
                new CtFont {Id = new Id(29), FontName = "楷体", FamilyName = "楷体"},
                new CtFont {Id = new Id(61), FontName = "KaiTi", FamilyName = "KaiTi"},
                new CtFont {Id = new Id(63), FontName = "宋体", FamilyName = "宋体"},
                new CtFont {Id = new Id(66), FontName = "Courier New", FamilyName = "Courier New"}
            };
            foreach (var ctFont in ctFonts)
            {
                fonts.Add(new XElement(Const.OfdNamespace + "Font", new XAttribute("ID", ctFont.Id.ToString()), new XAttribute("FontName", ctFont.FontName), new XAttribute("FamilyName", ctFont.FamilyName)));
            }

            _publicResElement.Add(fonts);
        }

        public void WritePages()
        {
            _pageElement = new XElement(Const.OfdNamespace + "Page", new XAttribute(XNamespace.Xmlns + "ofd", Const.OfdXmlns));
            foreach (var po in _pageObjects)
            {
                _pageElement.Add(new XElement(Const.OfdNamespace + "Area", new XElement(Const.OfdNamespace + "PhysicalBox", po.Area.Physical.ToString())));
                _pageElement.Add(new XElement(Const.OfdNamespace + "Template", new XAttribute("TemplateID", po.Template.TemplateId), new XAttribute("ZOrder", po.Template.ZOrder)));

                XElement contentElement = new XElement(Const.OfdNamespace + "Content");
                foreach (var layer in po.Content.Layers)
                {
                    XElement layerElement = new XElement(Const.OfdNamespace + "Layer", new XAttribute("ID", layer.Id.ToString()));

                    foreach (var pb in layer.PageBlocks)
                    {
                        if (pb.TextObject != null)
                        {
                            XElement textObjectElement = new XElement(Const.OfdNamespace + "TextObject", new XAttribute("ID", pb.TextObject.Id.ToString()), new XAttribute("Boundary", pb.TextObject.Boundary.ToString()), new XAttribute("Font", pb.TextObject.Font.Id.ToString()), new XAttribute("Size", pb.TextObject.Size));
                            textObjectElement.Add(new XElement(Const.OfdNamespace + "FillColor", new XAttribute("Value", pb.TextObject.FillColor.Value.ToString())));
                            textObjectElement.Add(new XElement(Const.OfdNamespace + "TextCode", pb.TextObject.TextCode.Value, new XAttribute("X", pb.TextObject.TextCode.X), new XAttribute("Y", pb.TextObject.TextCode.Y), new XAttribute("DeltaX", "g " + pb.TextObject.TextCode.DeltaX)));
                            layerElement.Add(textObjectElement);
                        }

                        if (pb.PathObject != null)
                        {
                            XElement pathObjectElement = new XElement(Const.OfdNamespace + "PathObject", new XAttribute("ID", pb.PathObject.Id.ToString()), new XAttribute("Boundary", pb.PathObject.Boundary.ToString()), new XAttribute("LineWidth", pb.PathObject.LineWidth));
                            pathObjectElement.Add(new XElement(Const.OfdNamespace + "StrokeColor", new XAttribute("Value", pb.PathObject.StrokeColor.Value.ToString())));
                            pathObjectElement.Add(new XElement(Const.OfdNamespace + "AbbreviatedData", pb.PathObject.AbbreviatedData));
                            layerElement.Add(pathObjectElement);
                        }

                        if (pb.ImageObject != null)
                        {
                            layerElement.Add(new XElement(Const.OfdNamespace + "ImageObject", new XAttribute("ID", pb.ImageObject.Id), new XAttribute("Boundary", pb.ImageObject.Boundary), new XAttribute("CTM", pb.ImageObject.Ctm), new XAttribute("ResourceID", pb.ImageObject.ResourceId)));
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

            _attachmentElement = new XElement(Const.OfdNamespace + "Attachments", new XAttribute(XNamespace.Xmlns + "ofd", Const.OfdXmlns));
            foreach (var attachment in _attachments)
            {
                XElement newAttachment = new XElement(Const.OfdNamespace + "Attachment", new XAttribute("ID", attachment.Id), new XAttribute("Name", attachment.Name), new XAttribute("Format", attachment.Format), new XAttribute("Visible", attachment.Visible));
                newAttachment.Add(new XElement(Const.OfdNamespace + "FileLoc", attachment.FileLoc.Value));
                _attachmentElement.Add(newAttachment);
            }
        }

        public void WriteTemplate()
        {
            _templatePages.Add(new PageObject
            {
                Content = new Content
                {
                    Layers = new List<Layer>
                    {
                        new Layer
                        {
                            Id = new Id(303),
                            DrawParam = new RefId
                            {
                                Id = new Id(4)
                            },
                            PageBlocks = new List<PageBlock>
                            {
                                new PageBlock
                                {
                                    PathObject = new Primitives.Graph.Path
                                    {
                                        Id = new Id(6),
                                        Boundary = new Box(68.5, 17.8, 73, 0.4),
                                        LineWidth = 0.25,
                                        FillColor = new Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value = new Primitives.Array("156 82 35"),
                                            ColorSpace = new RefId
                                            {
                                                Id = new Id(5)
                                            }
                                        },
                                        StrokeColor = new Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value = new Primitives.Array("156 82 35"),
                                            ColorSpace = new RefId
                                            {
                                                Id = new Id(5)
                                            }
                                        },
                                        AbbreviatedData = "M 0 0.2 L 73 0.2",
                                    }
                                },
                                new PageBlock
                                {
                                    PathObject = new Primitives.Graph.Path
                                    {
                                        Id = new Id(7),
                                        Boundary = new Box(68.5, 18.8, 73, 0.4),
                                        LineWidth = 0.25,
                                        FillColor = new Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value = new Primitives.Array("156 82 35"),
                                            ColorSpace = new RefId
                                            {
                                                Id = new Id(5)
                                            }
                                        },
                                        StrokeColor = new Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value = new Primitives.Array("156 82 35"),
                                            ColorSpace = new RefId
                                            {
                                                Id = new Id(5)
                                            }
                                        },
                                        AbbreviatedData = "M 0 0.2 L 73 0.2",
                                    }
                                },
                                new PageBlock
                                {
                                    PathObject = new Primitives.Graph.Path
                                    {
                                        Id = new Id(8),
                                        Boundary = new Box(4.5, 29.8, 201, 0.4),
                                        LineWidth = 0.25,
                                        FillColor = new Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value = new Primitives.Array("156 82 35"),
                                            ColorSpace = new RefId
                                            {
                                                Id = new Id(5)
                                            }
                                        },
                                        StrokeColor = new Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value = new Primitives.Array("156 82 35"),
                                            ColorSpace = new RefId
                                            {
                                                Id = new Id(5)
                                            }
                                        },
                                        AbbreviatedData = "M 0 0.2 L 201 0.2",
                                    }
                                },
                                new PageBlock
                                {
                                    TextObject = new CtText
                                    {
                                        Id = new Id(32),
                                        Boundary = new Box(148.5, 18.5, 16, 3.6),
                                        Font = new RefId {Id = new Id(29)},
                                        Size = 3.175,
                                        FillColor = new Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value = new Primitives.Array("156 82 35")
                                        },
                                        TextCode = new TextCode
                                        {
                                            X = 0.1,
                                            Y = 2.734,
                                            DeltaX = new Primitives.Array("3.175 3.175 3.175 3.175"),
                                            Value = "开票日期："
                                        }
                                    }
                                },
                                new PageBlock
                                {
                                    TextObject = new CtText
                                    {
                                        Id = new Id(33),
                                        Boundary = new Box(148.5, 24, 16, 3.6),
                                        Font = new RefId {Id = new Id(29)},
                                        Size = 3.175,
                                        FillColor = new Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value = new Primitives.Array("156 82 35")
                                        },
                                        TextCode = new TextCode
                                        {
                                            X = 0.1,
                                            Y = 2.734,
                                            DeltaX = new Primitives.Array("4.86 4.87 3.175"),
                                            Value = "校验码："
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });

            foreach (var pageObject in _templatePages)
            {
                XElement pageElement = new XElement(Const.OfdNamespace + "Page", new XAttribute(XNamespace.Xmlns + "ofd", Const.OfdXmlns));
                XElement contentElement = new XElement(Const.OfdNamespace + "Content");
                foreach (var layer in pageObject.Content.Layers)
                {
                    XElement layerElement = new XElement(Const.OfdNamespace + "Layer", new XAttribute("DrawParam", layer.DrawParam), new XAttribute("ID", layer.Id));
                    foreach (var pageBlock in layer.PageBlocks)
                    {
                        if (pageBlock.PathObject != null)
                        {
                            XElement pathElement = new XElement(Const.OfdNamespace + "PathObject", new XAttribute("ID", pageBlock.PathObject.Id), new XAttribute("Boundary", pageBlock.PathObject.Boundary), new XAttribute("LineWidth", pageBlock.PathObject.LineWidth));
                            pathElement.Add(new XElement(Const.OfdNamespace + "FillColor", new XAttribute("Value", pageBlock.PathObject.FillColor.Value), new XAttribute("ColorSpace", pageBlock.PathObject.FillColor.ColorSpace)));
                            pathElement.Add(new XElement(Const.OfdNamespace + "StrokeColor", new XAttribute("Value", pageBlock.PathObject.StrokeColor.Value), new XAttribute("ColorSpace", pageBlock.PathObject.StrokeColor.ColorSpace)));
                            pathElement.Add(new XElement(Const.OfdNamespace + "AbbreviatedData", new XAttribute("Value", pageBlock.PathObject.AbbreviatedData)));
                            layerElement.Add(pathElement);
                        }

                        if (pageBlock.TextObject != null)
                        {
                            XElement textObjectElement = new XElement(Const.OfdNamespace + "TextObject", new XAttribute("ID", pageBlock.TextObject.Id.ToString()), new XAttribute("Boundary", pageBlock.TextObject.Boundary.ToString()), new XAttribute("Font", pageBlock.TextObject.Font.Id.ToString()), new XAttribute("Size", pageBlock.TextObject.Size));
                            textObjectElement.Add(new XElement(Const.OfdNamespace + "FillColor", new XAttribute("Value", pageBlock.TextObject.FillColor.Value.ToString())));
                            textObjectElement.Add(new XElement(Const.OfdNamespace + "TextCode", pageBlock.TextObject.TextCode.Value, new XAttribute("X", pageBlock.TextObject.TextCode.X), new XAttribute("Y", pageBlock.TextObject.TextCode.Y), new XAttribute("DeltaX", pageBlock.TextObject.TextCode.DeltaX)));
                            layerElement.Add(textObjectElement);
                        }
                    }

                    contentElement.Add(layerElement);
                }

                pageElement.Add(contentElement);
                _templateElements.Add(pageElement);
            }
        }

        public void WriteCustomerTag(XElement content)
        {
            _customerTagsElement = new XElement(Const.OfdNamespace + "CustomTags", new XAttribute(XNamespace.Xmlns + "ofd", Const.OfdXmlns));
            CustomTag customTag = new CustomTag { FileLoc = new Location { Value = "CustomTag.xml" }, TypeId = "0" };
            _customTags.Add(customTag);
            foreach (var tg in _customTags)
            {
                XElement customerTagElement = new XElement(Const.OfdNamespace + "CustomTag");
                customerTagElement.Add(new XAttribute("TypeID", tg.TypeId));
                customerTagElement.Add(new XElement(Const.OfdNamespace + "FileLoc", tg.FileLoc));
                _customerTagsElement.Add(customerTagElement);
            }

            //tags内容
            _customTagElements.Add(content);
        }

        public void WriteAnnotation()
        {
            _annotationsElement = new XElement(Const.OfdNamespace + "Annotations", new XAttribute(XNamespace.Xmlns + "ofd", Const.OfdXmlns));
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
                    PathObject = new Primitives.Graph.Path
                    {
                        Boundary = new Box(4.5, 104, 115, 20)
                    }
                }
            };
            _annotationsElement.Add(new XElement(Const.OfdNamespace + "Page", new XAttribute("PageID", "1"), new XElement(Const.OfdNamespace + "FileLoc", "Page_0/Annotation.xml")));

            _annotationElement = new XElement(Const.OfdNamespace + "PageAnnot", new XAttribute(XNamespace.Xmlns + "ofd", Const.OfdXmlns));
            XElement annotElement = new XElement(Const.OfdNamespace + "Annot");
            annotElement.Add(new XAttribute("Type", annotationInfo.Type));
            annotElement.Add(new XAttribute("ID", annotationInfo.Id));
            annotElement.Add(new XAttribute("Subtype", annotationInfo.Subtype));

            XElement parametersElement = new XElement(Const.OfdNamespace + "Parameters");
            foreach (var p in annotationInfo.Parameters)
            {
                parametersElement.Add(new XElement(Const.OfdNamespace + "Parameter", p.Value, new XAttribute("Name", p.Name)));
            }

            annotElement.Add(parametersElement);
            annotElement.Add(new XElement(Const.OfdNamespace + "Appearance", new XAttribute("Boundary", annotationInfo.Appearance.PathObject.Boundary)));
            _annotationElement.Add(annotElement);
        }

        public OfdWriter WriteCert(byte[] sealCert, byte[] signerCert)
        {
            _sealCert = sealCert;
            _signerCert = signerCert;
            return this;
        }

        public void WriteSignature()
        {
            _signaturesElement = new XElement(Const.OfdNamespace + "Signatures", new XAttribute(XNamespace.Xmlns + "ofd", Const.OfdXmlns));
            _signaturesElement.Add(new XElement(Const.OfdNamespace + "MaxSignId", "2"));
            _signaturesElement.Add(new XElement(Const.OfdNamespace + "Signature", new XAttribute("ID", 2), new XAttribute("BaseLoc", "/Doc_0/Signs/Sign_0/Signature.xml")));

            _signatureElement = new XElement(Const.OfdNamespace + "Signature", new XAttribute(XNamespace.Xmlns + "ofd", Const.OfdXmlns), new XAttribute("ID", 2), new XAttribute("Type", "Seal"));

            string c = _annotationElement.GetElementContentTest();

            SignedInfo signedInfo = new SignedInfo
            {
                Provider = new Provider { Company = "gomain", ProviderName = "gomain_eseal", Version = "2.0" },
                SignatureMethod = GMObjectIdentifiers.sm2sign_with_sm3.Id,
                SignatureDateTime = "20220124060837.228Z",
                ReferenceCollect = new ReferenceCollect
                {
                    CheckMethod = GMObjectIdentifiers.sm3.Id,
                    Items = new List<Primitives.Signatures.Reference>
                    {
                        new Primitives.Signatures.Reference
                        {
                            CheckValue = new CheckValue
                            {
                                Value= Convert.ToBase64String(Sm2Utils.Digest (GMObjectIdentifiers.sm3.Id,c,Encoding.UTF8)),//   "afw9LQlHy/iFpYgqFX2sBVWELplp+aIu+udBowwTztI="
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
            var signedInfoElement = new XElement(Const.OfdNamespace + "SignedInfo");
            signedInfoElement.Add(new XElement(Const.OfdNamespace + "Provider", new XAttribute("ProviderName", signedInfo.Provider.ProviderName), new XAttribute("Version", signedInfo.Provider.Version), new XAttribute("Company", signedInfo.Provider.Company)));
            signedInfoElement.Add(new XElement(Const.OfdNamespace + "SignatureMethod", signedInfo.SignatureMethod));
            signedInfoElement.Add(new XElement(Const.OfdNamespace + "SignatureDateTime", signedInfo.SignatureDateTime));

            XElement referencesElement = new XElement(Const.OfdNamespace + "References", new XAttribute("CheckMethod", signedInfo.ReferenceCollect.CheckMethod));
            foreach (var reference in signedInfo.ReferenceCollect.Items)
            {
                XElement referenceElement = new XElement(Const.OfdNamespace + "Reference", new XAttribute("FileRef", reference.FileRef));
                referenceElement.Add(new XElement(Const.OfdNamespace + "CheckValue", reference.CheckValue.Value));
                referencesElement.Add(referenceElement);
            }
            signedInfoElement.Add(referencesElement);
            signedInfoElement.Add(new XElement(Const.OfdNamespace + "StampAnnot", new XAttribute("ID", signedInfo.StampAnnot.Id), new XAttribute("PageRef", signedInfo.StampAnnot.PageRef), new XAttribute("Boundary", signedInfo.StampAnnot.Boundary)));

            _signatureElement.Add(signedInfoElement);
            _signatureElement.Add(new XElement(Const.OfdNamespace + "SignedValue", digestInfo.SignedValue));
        }

        /// <summary>
        /// 创建签章签名值文件测试
        /// </summary>
        private byte[] CreateSignedValueDataFileContent()
        {

            SesSignatureInfo t = new SesSignatureInfo
            {
                dataHash = new byte[128],
                PropertyInfo = "/Doc_0/Signs/Sign_0/Signature.xml",
                manufacturer = "GOMAIN",
                sealName = "测试全国统一发票监制章国家税务总局重庆市税务局",
                esId = "50011200000001",
                sealCert = _sealCert,
                sealPicture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Files", "image_78.jb2")),
                sealSign = new byte[128],
                signature = new byte[128],
                signerCert = _signerCert
            };
            return Sm2Utils.CreateSignedValueData(t);
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

                foreach (var multiMedia in _ctMultiMedias)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "Files", multiMedia.MediaFile.Value);
                    if (_ctMultiMedias.IndexOf(multiMedia) == 0)
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