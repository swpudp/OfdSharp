using OfdSharp.Primitives;
using OfdSharp.Primitives.Fonts;
using OfdSharp.Primitives.Ofd;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using OfdSharp.Primitives.Pages.Object;
using OfdSharp.Primitives.Pages.Tree;
using OfdSharp.Primitives.Text;
using OfdSharp.Primitives.Attachments;
using OfdSharp.Primitives.Resources;
using System.Diagnostics;
using OfdSharp.Primitives.CustomTags;
using OfdSharp.Primitives.Annotations;

namespace OfdSharp.Writer
{
    public class OfdWriter
    {
        public bool IsFormating { get; }
        private XElement _rootElement;
        private XElement _documentElement;
        private XElement _documentResElement;
        private XElement _publicResElement;
        private XElement _pageElement;
        private XElement _attachmentElement;
        private XElement _customerTagsElement;
        private XElement _annotationsElement;
        private static readonly XNamespace OfdNamespace = "http://www.ofdspec.org/2016";
        private static readonly string OfdXmls = "http://www.ofdspec.org/2016";

        //页面
        private List<PageNode> _pageNodes = new List<PageNode>();
        private List<PageObject> _pageObjects = new List<PageObject>();

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

        public OfdWriter()
        {
            IsFormating = false;
        }

        public OfdWriter(bool isFormating)
        {
            IsFormating = isFormating;
        }

        public void WriteOfdRoot()
        {
            XElement docInfo = new XElement(OfdNamespace + "DocInfo");
            docInfo.Add(new XElement(OfdNamespace + "DocID", "c9a4ced42f284320964bf1e630d1fa2a"));
            docInfo.Add(new XElement(OfdNamespace + "Author", "China Tax"));
            docInfo.Add(new XElement(OfdNamespace + "CreationDate", "2020-11-16"));
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
            XElement customDataElement = new XElement(OfdNamespace + "CustomDatas");
            foreach (var item in customDatas)
            {
                customDataElement.Add(new XElement(OfdNamespace + "CustomData", new XAttribute("Name", item.Name), item.Value));
            }

            docInfo.Add(customDataElement);
            XElement docBody = new XElement(OfdNamespace + "DocBody", docInfo);
            docBody.Add(new XElement(OfdNamespace + "DocRoot", "Doc_0/Document.xml"));
            docBody.Add(new XElement(OfdNamespace + "Signatures", "Doc_0/Signs/Signatures.xml"));
            _rootElement = new XElement(OfdNamespace + "OFD", new XAttribute(XNamespace.Xmlns + "ofd", OfdXmls), new XAttribute("DocType", "OFD"), new XAttribute("Version", "1.1"), docBody);
        }

        public void WriteDocument()
        {
            XElement commonDataElement = new XElement(OfdNamespace + "CommonData");
            commonDataElement.Add(new XElement(OfdNamespace + "MaxUnitID", "97"));
            commonDataElement.Add(new XElement(OfdNamespace + "TemplatePage", new XAttribute("ID", "2"), new XAttribute("BaseLoc", "Tpls/Tpl_0/Content.xml")));
            commonDataElement.Add(new XElement(OfdNamespace + "PageArea", new XElement(OfdNamespace + "PhysicalBox", "0 0 210 140")));
            commonDataElement.Add(new XElement(OfdNamespace + "DocumentRes", "DocumentRes.xml"));
            commonDataElement.Add(new XElement(OfdNamespace + "PublicRes", "PublicRes.xml"));

            _documentElement = new XElement(OfdNamespace + "Document", new XAttribute(XNamespace.Xmlns + "ofd", OfdXmls));
            _documentElement.Add(commonDataElement);

            //Pages
            XElement pagesElement = new XElement(OfdNamespace + "Pages");
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
                                        Size =6.61,
                                        FillColor = new Primitives.Pages.Description.Color.CtColor{Value=new Primitives.Array("156 82 35")},
                                        TextCode =  new TextCode{ X = 0,Y=5.683674,DeltaX=new Primitives.Array("10 6.61") ,Value="北京增值税电子普通发票"}
                                    }
                                },
                                new PageBlock
                                {
                                    PathObject = new Primitives.Graph.Path
                                    {
                                        Id = new Id(304),
                                        Boundary = new Box(68.5, 18 ,73, 0.25),
                                        LineWidth = 0.25,
                                        StrokeColor =new Primitives.Pages.Description.Color.CtColor{Value=new Primitives.Array("156 82 35")},
                                        AbbreviatedData ="M 0 0 L 73 0",
                                    }
                                },
                                new PageBlock
                                {
                                    PathObject = new Primitives.Graph.Path
                                    {
                                        Id = new Id(305),
                                        Boundary = new Box(68.5 ,19, 73 ,0.25),
                                        LineWidth = 0.25,
                                        StrokeColor =new Primitives.Pages.Description.Color.CtColor{Value=new Primitives.Array("156 82 35")},
                                        AbbreviatedData ="M 0 0 L 73 0",
                                    }
                                },
                                new PageBlock
                                {
                                    ImageObject=new Primitives.Image.CtImage
                                    {
                                        Id=new Id(310),
                                        Ctm=new Primitives.Array("20 0 0 20 0 0"),
                                        Boundary=new Box(8.5 ,4 ,20, 20),
                                        ResourceId=new RefId{Id=new Id(311)}
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
                pagesElement.Add(new XElement(OfdNamespace + "Page", new XAttribute("ID", pg.Id.ToString()), new XAttribute("BaseLoc", pg.BaseLoc.Value)));
            }

            _documentElement.Add(pagesElement);

            //Annotations
            XElement annotationsElement = new XElement(OfdNamespace + "Annotations", "Annots/Annotations.xml");
            _documentElement.Add(annotationsElement);

            //Attachments
            XElement attachmentsElement = new XElement(OfdNamespace + "Attachments", "Attachs/Attachments.xml");
            _documentElement.Add(attachmentsElement);

            //CustomTags
            XElement customTagsElement = new XElement(OfdNamespace + "CustomTags", "Tags/CustomTags.xml");
            _documentElement.Add(customTagsElement);
        }

        public void WriteDocumentRes()
        {
            _documentResElement = new XElement(OfdNamespace + "Res", new XAttribute(XNamespace.Xmlns + "ofd", OfdXmls), new XAttribute("BaseLoc", "Res"));
            //DrawParams
            XElement drawParams = new XElement(OfdNamespace + "DrawParams");
            XElement drawParam = new XElement(OfdNamespace + "DrawParam", new XAttribute("ID", "4"), new XAttribute("LineWidth", "0.25"));
            drawParam.Add(new XElement(OfdNamespace + "FillColor", new XAttribute("Value", "156 82 35"), new XAttribute("ColorSpace", "5")));
            drawParam.Add(new XElement(OfdNamespace + "StrokeColor", new XAttribute("Value", "156 82 35"), new XAttribute("ColorSpace", "5")));
            drawParams.Add(drawParam);
            _documentResElement.Add(drawParams);

            _ctMultiMedias.Add(new CtMultiMedia { Format = "GBIG2", Type = MediaType.Image, Id = new Id(78), MediaFile = new Location { Value = "image_78.jb2" } });
            _ctMultiMedias.Add(new CtMultiMedia { Format = "GBIG2", Type = MediaType.Image, Id = new Id(79), MediaFile = new Location { Value = "image_80.jb2" } });
            //MultiMedias
            XElement multiMedias = new XElement(OfdNamespace + "MultiMedias");
            foreach (var ctMultiMedia in _ctMultiMedias)
            {
                XElement multiMedia = new XElement(OfdNamespace + "MultiMedia", new XAttribute("ID", ctMultiMedia.Id), new XAttribute("Type", ctMultiMedia.Type), new XAttribute("Format", ctMultiMedia.Format));
                multiMedia.Add(new XElement(OfdNamespace + "MediaFile", ctMultiMedia.MediaFile.Value));
                multiMedias.Add(multiMedia);
            }
            _documentResElement.Add(multiMedias);
        }

        public void WritePublicRes()
        {
            _publicResElement = new XElement(OfdNamespace + "Res", new XAttribute(XNamespace.Xmlns + "ofd", OfdXmls), new XAttribute("BaseLoc", "Res"));

            XElement colorSpaces = new XElement(OfdNamespace + "ColorSpaces");
            colorSpaces.Add(new XElement(OfdNamespace + "ColorSpace", new XAttribute("ID", "5"), new XAttribute("Type", "RGB"), new XAttribute("BitsPerComponent", "8")));
            _publicResElement.Add(colorSpaces);

            XElement fonts = new XElement(OfdNamespace + "Fonts");
            List<CtFont> ctFonts = new List<CtFont>
            {
                new CtFont {Id = new Id(29), FontName = "楷体", FamilyName = "楷体"},
                new CtFont {Id = new Id(61), FontName = "KaiTi", FamilyName = "KaiTi"},
                new CtFont {Id = new Id(63), FontName = "宋体", FamilyName = "宋体"},
                new CtFont {Id = new Id(66), FontName = "Courier New", FamilyName = "Courier New"}
            };
            foreach (var ctFont in ctFonts)
            {
                fonts.Add(new XElement(OfdNamespace + "Font", new XAttribute("ID", ctFont.Id.ToString()), new XAttribute("FontName", ctFont.FontName), new XAttribute("FamilyName", ctFont.FamilyName)));
            }

            _publicResElement.Add(fonts);
        }

        public void WritePages()
        {
            _pageElement = new XElement(OfdNamespace + "Page", new XAttribute(XNamespace.Xmlns + "ofd", OfdXmls));
            foreach (var po in _pageObjects)
            {
                _pageElement.Add(new XElement(OfdNamespace + "Area", new XElement(OfdNamespace + "PhysicalBox", po.Area.Physical.ToString())));
                _pageElement.Add(new XElement(OfdNamespace + "Template", new XAttribute("TemplateID", po.Template.TemplateId), new XAttribute("ZOrder", po.Template.ZOrder)));

                XElement contentElement = new XElement(OfdNamespace + "Content");
                foreach (var layer in po.Content.Layers)
                {
                    XElement layerElement = new XElement(OfdNamespace + "Layer", new XAttribute("ID", layer.Id.ToString()));

                    foreach (var pb in layer.PageBlocks)
                    {
                        if (pb.TextObject != null)
                        {
                            XElement textObjectElement = new XElement(OfdNamespace + "TextObject", new XAttribute("ID", pb.TextObject.Id.ToString()), new XAttribute("Boundary", pb.TextObject.Boundary.ToString()), new XAttribute("Font", pb.TextObject.Font.Id.ToString()), new XAttribute("Size", pb.TextObject.Size));
                            textObjectElement.Add(new XElement(OfdNamespace + "FillColor", new XAttribute("Value", pb.TextObject.FillColor.Value.ToString())));
                            textObjectElement.Add(new XElement(OfdNamespace + "TextCode", pb.TextObject.TextCode.Value, new XAttribute("X", pb.TextObject.TextCode.X), new XAttribute("Y", pb.TextObject.TextCode.Y), new XAttribute("DeltaX", "g " + pb.TextObject.TextCode.DeltaX)));
                            layerElement.Add(textObjectElement);
                        }
                        if (pb.PathObject != null)
                        {
                            XElement pathObjectElement = new XElement(OfdNamespace + "PathObject", new XAttribute("ID", pb.PathObject.Id.ToString()), new XAttribute("Boundary", pb.PathObject.Boundary.ToString()), new XAttribute("LineWidth", pb.PathObject.LineWidth));
                            pathObjectElement.Add(new XElement(OfdNamespace + "StrokeColor", new XAttribute("Value", pb.PathObject.StrokeColor.Value.ToString())));
                            pathObjectElement.Add(new XElement(OfdNamespace + "AbbreviatedData", pb.PathObject.AbbreviatedData));
                            layerElement.Add(pathObjectElement);
                        }
                        if (pb.ImageObject != null)
                        {
                            layerElement.Add(new XElement(OfdNamespace + "ImageObject", new XAttribute("ID", pb.ImageObject.Id), new XAttribute("Boundary", pb.ImageObject.Boundary), new XAttribute("CTM", pb.ImageObject.Ctm), new XAttribute("ResourceID", pb.ImageObject.ResourceId)));
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
            _attachmentElement = new XElement(OfdNamespace + "Attachments", new XAttribute(XNamespace.Xmlns + "ofd", OfdXmls));
            foreach (var attachment in _attachments)
            {
                XElement newAttachment = new XElement(OfdNamespace + "Attachment", new XAttribute("ID", attachment.Id), new XAttribute("Name", attachment.Name), new XAttribute("Format", attachment.Format), new XAttribute("Visible", attachment.Visible));
                newAttachment.Add(new XElement(OfdNamespace + "FileLoc", attachment.FileLoc.Value));
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
                                        Boundary = new Box(68.5 ,17.8 ,73, 0.4),
                                        LineWidth = 0.25,
                                        FillColor = new Primitives.Pages.Description.Color.CtColor
                                        {
                                             Value=new Primitives.Array("156 82 35"),
                                            ColorSpace=new RefId
                                            {
                                                Id = new Id(5)
                                            }
                                        },
                                        StrokeColor =new Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value=new Primitives.Array("156 82 35"),
                                            ColorSpace=new RefId
                                            {
                                                Id = new Id(5)
                                            }
                                        },
                                        AbbreviatedData ="M 0 0.2 L 73 0.2",
                                    }
                                },
                                new PageBlock
                                {
                                    PathObject = new Primitives.Graph.Path
                                    {
                                        Id = new Id(7),
                                        Boundary = new Box(68.5 ,18.8 ,73, 0.4),
                                        LineWidth = 0.25,
                                        FillColor = new Primitives.Pages.Description.Color.CtColor
                                        {
                                             Value=new Primitives.Array("156 82 35"),
                                            ColorSpace=new RefId
                                            {
                                                Id = new Id(5)
                                            }
                                        },
                                        StrokeColor =new Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value=new Primitives.Array("156 82 35"),
                                            ColorSpace=new RefId
                                            {
                                                Id = new Id(5)
                                            }
                                        },
                                        AbbreviatedData ="M 0 0.2 L 73 0.2",
                                    }
                                },
                                 new PageBlock
                                {
                                    PathObject = new Primitives.Graph.Path
                                    {
                                        Id = new Id(8),
                                        Boundary = new Box(4.5, 29.8,201, 0.4),
                                        LineWidth = 0.25,
                                        FillColor = new Primitives.Pages.Description.Color.CtColor
                                        {
                                             Value=new Primitives.Array("156 82 35"),
                                            ColorSpace=new RefId
                                            {
                                                Id = new Id(5)
                                            }
                                        },
                                        StrokeColor =new Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value=new Primitives.Array("156 82 35"),
                                            ColorSpace=new RefId
                                            {
                                                Id = new Id(5)
                                            }
                                        },
                                        AbbreviatedData ="M 0 0.2 L 201 0.2",
                                    }
                                },
                                new PageBlock
                                {
                                    TextObject = new CtText
                                    {
                                        Id = new Id(32),
                                        Boundary = new Box(148.5, 18.5 ,16,3.6),
                                        Font = new RefId {Id = new Id(29)},
                                        Size =3.175,
                                        FillColor = new Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value=new Primitives.Array("156 82 35")
                                        },
                                        TextCode =  new TextCode{
                                            X = 0.1,
                                            Y=2.734,
                                            DeltaX=new Primitives.Array("3.175 3.175 3.175 3.175") ,
                                            Value="开票日期："
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
                                        Size =3.175,
                                        FillColor = new Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value=new Primitives.Array("156 82 35")
                                        },
                                        TextCode =  new TextCode{
                                            X = 0.1,
                                            Y=2.734,
                                            DeltaX=new Primitives.Array("4.86 4.87 3.175") ,
                                            Value="校验码："
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
                XElement pageElement = new XElement(OfdNamespace + "Page", new XAttribute(XNamespace.Xmlns + "ofd", OfdXmls));
                XElement contentElement = new XElement(OfdNamespace + "Content");
                foreach (var layer in pageObject.Content.Layers)
                {
                    XElement layerElement = new XElement(OfdNamespace + "Layer", new XAttribute("DrawParam", layer.DrawParam), new XAttribute("ID", layer.Id));
                    foreach (var pageBlock in layer.PageBlocks)
                    {
                        if (pageBlock.PathObject != null)
                        {
                            XElement pathElement = new XElement(OfdNamespace + "PathObject", new XAttribute("ID", pageBlock.PathObject.Id), new XAttribute("Boundary", pageBlock.PathObject.Boundary), new XAttribute("LineWidth", pageBlock.PathObject.LineWidth));
                            pathElement.Add(new XElement(OfdNamespace + "FillColor", new XAttribute("Value", pageBlock.PathObject.FillColor.Value), new XAttribute("ColorSpace", pageBlock.PathObject.FillColor.ColorSpace)));
                            pathElement.Add(new XElement(OfdNamespace + "StrokeColor", new XAttribute("Value", pageBlock.PathObject.StrokeColor.Value), new XAttribute("ColorSpace", pageBlock.PathObject.StrokeColor.ColorSpace)));
                            pathElement.Add(new XElement(OfdNamespace + "AbbreviatedData", new XAttribute("Value", pageBlock.PathObject.AbbreviatedData)));
                            layerElement.Add(pathElement);
                        }
                        if (pageBlock.TextObject != null)
                        {
                            XElement textObjectElement = new XElement(OfdNamespace + "TextObject", new XAttribute("ID", pageBlock.TextObject.Id.ToString()), new XAttribute("Boundary", pageBlock.TextObject.Boundary.ToString()), new XAttribute("Font", pageBlock.TextObject.Font.Id.ToString()), new XAttribute("Size", pageBlock.TextObject.Size));
                            textObjectElement.Add(new XElement(OfdNamespace + "FillColor", new XAttribute("Value", pageBlock.TextObject.FillColor.Value.ToString())));
                            textObjectElement.Add(new XElement(OfdNamespace + "TextCode", pageBlock.TextObject.TextCode.Value, new XAttribute("X", pageBlock.TextObject.TextCode.X), new XAttribute("Y", pageBlock.TextObject.TextCode.Y), new XAttribute("DeltaX", pageBlock.TextObject.TextCode.DeltaX)));
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
            _customerTagsElement = new XElement(OfdNamespace + "CustomTags", new XAttribute(XNamespace.Xmlns + "ofd", OfdXmls));
            CustomTag customTag = new CustomTag { FileLoc = new Location { Value = "CustomTag.xml" }, TypeId = "0" };
            _customTags.Add(customTag);
            foreach (var tg in _customTags)
            {
                XElement customerTagElement = new XElement(OfdNamespace + "CustomTag");
                customerTagElement.Add(new XAttribute("TypeID", tg.TypeId));
                customerTagElement.Add(new XElement(OfdNamespace + "FileLoc", tg.FileLoc));
                _customerTagsElement.Add(customerTagElement);
            }
            //tags内容
            _customTagElements.Add(content);
        }

        public void WriteAnnotation()
        {
            _annotationsElement = new XElement(OfdNamespace + "Annotations", new XAttribute(XNamespace.Xmlns + "ofd", OfdXmls));
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
            _annotationsElement.Add(new XElement(OfdNamespace + "Page", new XAttribute("PageID", "1"), new XElement(OfdNamespace + "FileLoc", "Page_0/Annotation.xml")));


            _annotationElement = new XElement(OfdNamespace + "PageAnnot", new XAttribute(XNamespace.Xmlns + "ofd", OfdXmls));
            XElement annotElement = new XElement(OfdNamespace + "Annot");
            annotElement.Add(new XAttribute("Type", annotationInfo.Type));
            annotElement.Add(new XAttribute("ID", annotationInfo.Id));
            annotElement.Add(new XAttribute("Subtype", annotationInfo.Subtype));

            XElement parametersElement = new XElement(OfdNamespace + "Parameters");
            foreach (var p in annotationInfo.Parameters)
            {
                parametersElement.Add(new XElement(OfdNamespace + "Parameter", p.Value, new XAttribute("Name", p.Name)));
            }
            annotElement.Add(parametersElement);
            annotElement.Add(new XElement(OfdNamespace + "Appearance", new XAttribute("Boundary", annotationInfo.Appearance.PathObject.Boundary)));
            _annotationElement.Add(annotElement);
        }

        public byte[] Flush()
        {
            var stream = new MemoryStream();
            ZipArchive zipArchive = new ZipArchive(stream, ZipArchiveMode.Create, true);
            try
            {
                zipArchive.CreateEntry("OFD.xml", _rootElement, IsFormating);
                zipArchive.CreateEntry("Doc_0/Document.xml", _documentElement, IsFormating);
                zipArchive.CreateEntry("Doc_0/DocumentRes.xml", _documentResElement, IsFormating);
                zipArchive.CreateEntry("Doc_0/PublicRes.xml", _publicResElement, IsFormating);
                zipArchive.CreateEntry("Doc_0/Pages/Page_0/Content.xml", _pageElement, IsFormating);
                ParseAttachment();
                zipArchive.CreateEntry("Doc_0/Attachs/Attachments.xml", _attachmentElement, IsFormating);
                foreach (var attachment in _attachments)
                {
                    XElement attachmentElement = _attachmentElements.ElementAt(_attachments.IndexOf(attachment));
                    zipArchive.CreateEntry("Doc_0/Attachs/" + attachment.FileLoc.Value, attachmentElement, IsFormating);
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
                    zipArchive.CreateEntry("Doc_0/Tpls/Tpls_" + _templateElements.IndexOf(tplElement) + "/Content.xml", tplElement, IsFormating);
                }
                zipArchive.CreateEntry("Doc_0/Tags/CustomTags.xml", _customerTagsElement, IsFormating);
                foreach (var tg in _customTagElements)
                {
                    zipArchive.CreateEntry("Doc_0/Tags/CustomTag.xml", tg, IsFormating);
                }
                zipArchive.CreateEntry("Doc_0/Annots/Annotations.xml", _annotationsElement, IsFormating);
                zipArchive.CreateEntry("Doc_0/Annots/Page_0/Annotation.xml", _annotationElement, IsFormating);
            }
            finally
            {
                zipArchive.Dispose(); //LeaveOpen为true是，释放该资源，stream才不是空
                stream.Seek(0, SeekOrigin.Begin);
                stream.Flush(); //强制刷新缓冲区 这句话很关键
            }

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
            byte[] input = ReadToEnd(content);
            using (BinaryWriter writer = new BinaryWriter(ofd.Open()))
            {
                writer.Write(input);
                writer.Flush();
            }
            //var output = ofd.Open();
            //content.CopyTo(output);
            ////output.Flush();

            //content.Flush();
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

        private static byte[] ReadToEnd(Stream inputStream)
        {
            if (!inputStream.CanRead)
            {
                throw new ArgumentException();
            }
            // This is optional
            if (inputStream.CanSeek)
            {
                inputStream.Seek(0, SeekOrigin.Begin);
            }
            byte[] output = new byte[inputStream.Length];
            int bytesRead = inputStream.Read(output, 0, output.Length);
            Debug.Assert(bytesRead == output.Length, "Bytes read from stream matches stream length");
            return output;
        }
    }
}