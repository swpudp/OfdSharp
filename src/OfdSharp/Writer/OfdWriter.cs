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

namespace OfdSharp.Writer
{
    public class OfdWriter
    {
        private XElement _rootElement;
        private XElement _documentElement;
        private XElement _documentResElement;
        private XElement _publicResElement;
        private XElement _pageElement;
        private static readonly XNamespace OfdNamespace = "http://www.ofdspec.org/2016";
        private static readonly string OfdXmls = "http://www.ofdspec.org/2016";

        private List<PageNode> _pageNodes = new List<PageNode>();
        private List<PageObject> _pageObjects = new List<PageObject>();

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
            XElement attachmentsElement = new XElement(OfdNamespace + "Attachments");
            _documentElement.Add(annotationsElement);

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

            //MultiMedias
            XElement multiMedias = new XElement(OfdNamespace + "MultiMedias");
            XElement multiMedia = new XElement(OfdNamespace + "MultiMedia", new XAttribute("ID", "78"), new XAttribute("Type", "Image"), new XAttribute("Format", "GBIG2"));
            multiMedia.Add(new XElement(OfdNamespace + "MediaFile", "image_78.jb2"));
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
                    }
                    contentElement.Add(layerElement);
                }
                _pageElement.Add(contentElement);
            }
        }

        public byte[] Flush()
        {
            var stream = new MemoryStream();
            ZipArchive zipArchive = new ZipArchive(stream, ZipArchiveMode.Create, true);
            try
            {
                zipArchive.CreateEntry("OFD.xml", _rootElement);
                zipArchive.CreateEntry("Doc_0/Document.xml", _documentElement);
                zipArchive.CreateEntry("Doc_0/DocumentRes.xml", _documentResElement);
                zipArchive.CreateEntry("Doc_0/PublicRes.xml", _publicResElement);
                zipArchive.CreateEntry("Doc_0/Pages/Page_0/Content.xml", _pageElement);
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
        public static void CreateEntry(this ZipArchive zipArchive, string entryName, XElement content)
        {
            ZipArchiveEntry ofd = zipArchive.CreateEntry(entryName);
            using (StreamWriter sw = new StreamWriter(ofd.Open()))
            {
                sw.WriteLine(DefaultDeclaration.ToString());
                sw.WriteLine(content.ToString(SaveOptions.DisableFormatting));
            }
        }
    }
}