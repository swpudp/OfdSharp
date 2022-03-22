using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfdSharp;
using OfdSharp.Crypto;
using OfdSharp.Invoice;
using OfdSharp.Primitives;
using OfdSharp.Primitives.CustomTags;
using OfdSharp.Primitives.Doc;
using OfdSharp.Primitives.Entry;
using OfdSharp.Primitives.Fonts;
using OfdSharp.Primitives.Graph;
using OfdSharp.Primitives.Image;
using OfdSharp.Primitives.Pages.Description.Color;
using OfdSharp.Primitives.Pages.Object;
using OfdSharp.Primitives.Resources;
using OfdSharp.Primitives.Text;
using OfdSharp.Reader;
using OfdSharp.Verify;
using OfdSharp.Writer;

namespace UnitTests.Writer
{
    [TestClass]
    public class OfdWriterTests
    {
        private const string sealPub = "04fe69384d7f63148445ffbf77ebb635ccaba6a1b1c47484898449f360b0b337697b7b3478ba5c8dbc16a93acbf6a16f4b82f3fefe56e9224ee380f9bc75859bd3";
        private const string sealPri = "4a8b59be706e1de560e23b01dee790d55d51b57b768f8f13930841299a73017e";
        private static CipherKeyPair sealKey = new CipherKeyPair(sealPub, sealPri);

        private const string signerPub = "04b7443d2949201fe7d564ab0638e44a6f2ea8f85a7046185b2d7ccc3e80cbb25fa38adba287070d762ea9c6816bbe266d088ed69862a15695736fb8ad23489c58";
        private const string signerPri = "4f58588c3aecd59aa16c9f18f1b7e4b81f7a331f75ceb94dc9ae6dbf40faf2b4";
        private static CipherKeyPair signerKey = new CipherKeyPair(signerPub, signerPri);

        [TestMethod]
        public void WriteOfdRootTest()
        {
            OfdDocument ofdDocument = new OfdDocument(new OfdDocumentInfo());
            OfdWriter writer = new OfdWriter(ofdDocument, false);
            CtDocInfo docInfo = new CtDocInfo
            {
                DocId = Guid.NewGuid().ToString("N"),
                Title = "测试",
                Author = "test-001",
                CreationDate = DateTime.Now,
                Subject = "开发ofd测试",
                Abstract = "测试123",
                DocUsage = DocUsage.Normal.ToString(),
                Keywords = new List<string> { "invoice" },
                CustomDataList = new List<CustomData>
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
                }
            };
            writer.WriteOfdRoot(docInfo, 0);

            CommonData commonData = new CommonData
            {
                ColorSpace = new RefId { Id = new Id(0) },
                DocumentRes = new Location { Value = "DocumentRes.xml" },
                MaxUnitId = new Id(100),
                PublicRes = new Location { Value = "PublicRes.xml" },
                PageArea = new PageArea { Application = new Box(0, 0, 1000, 100) },
                TemplatePages = new List<TemplatePage>(),
            };
            var pageNodes = new List<OfdSharp.Primitives.Pages.Tree.PageNode>
            {
                new OfdSharp.Primitives.Pages.Tree.PageNode { Id = new Id(20), BaseLoc = new Location { Value = "Pages/Page_0/Content.xml" } }
            };
            writer.WriteDocument(commonData, pageNodes, true);



            DocumentResource res = new DocumentResource
            {
                BaseLoc = new Location { Value = "Res" },
                DrawParams = new List<OfdSharp.Primitives.Pages.Description.DrawParam.CtDrawParam> {

                    new OfdSharp.Primitives.Pages.Description.DrawParam.CtDrawParam
                    {
                        Id = new Id(20),
                        LineWidth = 0.25,
                        FillColor = new OfdSharp.Primitives.Pages.Description.Color.CtColor
                        {
                            Value=new OfdSharp.Primitives.Array("10 10 10 20"),
                            ColorSpace=new RefId { Id = new Id(25) },
                        },
                        StrokeColor = new OfdSharp.Primitives.Pages.Description.Color.CtColor
                        {
                            Value=new OfdSharp.Primitives.Array("10 20 10 20"),
                            ColorSpace=new RefId { Id = new Id(25) },
                        }
                    }
                },
                MultiMedias = new List<CtMultiMedia>
                {
                   new CtMultiMedia { Format = "GBIG2", Type = MediaType.Image, Id = new Id(78), MediaFile = new Location { Value = "image_78.jb2" } },
                   new CtMultiMedia { Format = "GBIG2", Type = MediaType.Image, Id = new Id(79), MediaFile = new Location { Value = "image_80.jb2" } }
                },
                ColorSpaces = new List<OfdSharp.Primitives.Pages.Description.ColorSpace.CtColorSpace>
                {
                    new OfdSharp.Primitives.Pages.Description.ColorSpace.CtColorSpace
                    {
                        Id = new Id(20),
                        BitsPerComponent=OfdSharp.Primitives.Pages.Description.ColorSpace.BitsPerComponent.Bit8,
                        Type= OfdSharp.Primitives.Pages.Description.ColorSpace.ColorSpaceType.RGB
                    }
                },
                Fonts = new List<CtFont>
                {
                    new CtFont {Id = new Id(29), FontName = "楷体", FamilyName = "楷体"},
                    new CtFont {Id = new Id(61), FontName = "KaiTi", FamilyName = "KaiTi"},
                    new CtFont {Id = new Id(63), FontName = "宋体", FamilyName = "宋体"},
                    new CtFont {Id = new Id(66), FontName = "Courier New", FamilyName = "Courier New"}
                }
            };
            writer.WriteDocumentRes(res);
            writer.WritePublicRes(res);
            writer.WritePages(GetPageObjects());
            InvoiceInfo invoiceInfo = CreateInvoiceInfo();
            XElement attachment = CreateInvoiceElement(invoiceInfo);
            XElement tag = CreateInvoiceTagElement();

            writer.AddAttachment("original_invoice", "original_invoice.xml", "xml", false, attachment);
            writer.WriteTemplate(GetTemplatePageObjects());

            var customTags = new List<CustomTag>();
            CustomTag customTag = new CustomTag { FileLoc = new Location { Value = "CustomTag.xml" }, TypeId = "0" };
            customTags.Add(customTag);
            writer.WriteCustomerTag(customTags, tag);
            writer.WriteAnnotation();


            writer.WriteCert(sealKey, signerKey).WriteSignature();

            byte[] content = writer.Flush();
            string fileName = Path.Combine(Directory.GetCurrentDirectory(), "test-root.ofd");
            File.WriteAllBytes(fileName, content);
            Assert.IsTrue(File.Exists(fileName));
        }

        private List<PageObject> GetTemplatePageObjects()
        {
            var templatePages = new List<PageObject>();
            templatePages.Add(new PageObject
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
                                    PathObject = new OfdSharp.Primitives.Graph.CtPath
                                    {
                                        Id = new Id(6),
                                        Boundary = new Box(68.5, 17.8, 73, 0.4),
                                        LineWidth = 0.25,
                                        FillColor = new OfdSharp.Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value = new OfdSharp.Primitives.Array("156 82 35"),
                                            ColorSpace = new RefId
                                            {
                                                Id = new Id(5)
                                            }
                                        },
                                        StrokeColor = new OfdSharp.Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value = new OfdSharp.Primitives.Array("156 82 35"),
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
                                    PathObject = new OfdSharp.Primitives.Graph.CtPath
                                    {
                                        Id = new Id(7),
                                        Boundary = new Box(68.5, 18.8, 73, 0.4),
                                        LineWidth = 0.25,
                                        FillColor = new OfdSharp.Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value = new OfdSharp.Primitives.Array("156 82 35"),
                                            ColorSpace = new RefId
                                            {
                                                Id = new Id(5)
                                            }
                                        },
                                        StrokeColor = new OfdSharp.Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value = new OfdSharp.Primitives.Array("156 82 35"),
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
                                    PathObject = new OfdSharp.Primitives.Graph.CtPath
                                    {
                                        Id = new Id(8),
                                        Boundary = new Box(4.5, 29.8, 201, 0.4),
                                        LineWidth = 0.25,
                                        FillColor = new OfdSharp.Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value = new OfdSharp.Primitives.Array("156 82 35"),
                                            ColorSpace = new RefId
                                            {
                                                Id = new Id(5)
                                            }
                                        },
                                        StrokeColor = new OfdSharp.Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value = new OfdSharp.Primitives.Array("156 82 35"),
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
                                        FillColor = new OfdSharp.Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value = new OfdSharp.Primitives.Array("156 82 35")
                                        },
                                        TextCode = new TextCode
                                        {
                                            X = 0.1,
                                            Y = 2.734,
                                            DeltaX = new OfdSharp.Primitives.Array("3.175 3.175 3.175 3.175"),
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
                                        FillColor = new OfdSharp.Primitives.Pages.Description.Color.CtColor
                                        {
                                            Value = new OfdSharp.Primitives.Array("156 82 35")
                                        },
                                        TextCode = new TextCode
                                        {
                                            X = 0.1,
                                            Y = 2.734,
                                            DeltaX = new OfdSharp.Primitives.Array("4.86 4.87 3.175"),
                                            Value = "校验码："
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });
            return templatePages;
        }

        private List<PageObject> GetPageObjects()
        {
            List<PageObject> pageObjects = new List<PageObject>();
            pageObjects.Add(new PageObject
            {
                PageRes = new Location { Value = "Pages/Page_0/Content.xml" },
                Area = new PageArea { Physical = new Box(0, 0, 210, 297) },
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
                                        FillColor = new CtColor {Value = new OfdSharp.Primitives.Array("156 82 35")},
                                        TextCode = new TextCode {X = 0, Y = 5.683674, DeltaX = new OfdSharp.Primitives.Array("10 6.61"), Value = "北京增值税电子普通发票"}
                                    }
                                },
                                new PageBlock
                                {
                                    PathObject = new CtPath
                                    {
                                        Id = new Id(304),
                                        Boundary = new Box(68.5, 18, 73, 0.25),
                                        LineWidth = 0.25,
                                        StrokeColor = new CtColor {Value = new OfdSharp.Primitives.Array("156 82 35")},
                                        AbbreviatedData = "M 0 0 L 73 0",
                                    }
                                },
                                new PageBlock
                                {
                                    PathObject = new CtPath
                                    {
                                        Id = new Id(305),
                                        Boundary = new Box(68.5, 19, 73, 0.25),
                                        LineWidth = 0.25,
                                        StrokeColor = new CtColor {Value = new OfdSharp.Primitives.Array("156 82 35")},
                                        AbbreviatedData = "M 0 0 L 73 0",
                                    }
                                },
                                new PageBlock
                                {
                                    ImageObject = new CtImage
                                    {
                                        Id = new Id(310),
                                        Ctm = new OfdSharp.Primitives.Array("20 0 0 20 0 0"),
                                        Boundary = new Box(8.5, 4, 20, 20),
                                        ResourceId = new RefId {Id = new Id(311)}
                                    }
                                }
                            }
                        }
                    }
                }
            });
            return pageObjects;
        }

        /// <summary>
        /// 新创建文件验证
        /// </summary>
        [TestMethod]
        public void ValidateAfterWrite()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "test-root.ofd");
            OfdReader reader = new OfdReader(filePath);
            VerifyResult verifyResult = OfdValidator.Validate(reader);
            Assert.IsNotNull(verifyResult);
            Assert.AreEqual(VerifyResult.Success, verifyResult);
        }

        private static InvoiceInfo CreateInvoiceInfo()
        {
            InvoiceInfo invoiceInfo = new InvoiceInfo
            {
                DocId = "01",
                AreaCode = "100",
                TypeCode = "0",
                InvoiceCode = "011002680026",
                InvoiceNo = "71500013",
                IssueDate = "2020年02月20日",
                InvoiceCheckCode = "03272875819686271285",
                MachineNo = string.Empty,
                GraphCode = "iVBORw0KGgoAAAANSUhEUgAAAJQAAACUCAIAAAD6XpeDAAAC+UlEQVR42u3aUU7EMAwEUO5/aTgBEm1nHHd5/UTsbpsXKWO7X1+F6/sP12//f/V7nnz/k+d68ryrL3jw4MH7J3iNxXoCllrcUxul/Vvw4MGDBy+DlzrAU2DtYNKGaawzPHjw4MF7B96pUPMkOJwKZfDgwYMH7314pwCuLnq7MQ0PHjx48HbhTQ4/ry7oZLN4sthfN1WABw8evA/GS13thvXb/776ggQPHrwPxvsuX41g8iS8nBrGVtYWHjx48ODdrvlONYsbvzU5jD3VfIcHDx48eNeesXEIpwaYjYZ1quncDjLw4MGDB28X3oYXhxrh4kkYGR0Ow4MHDx68scK8HQra4aj9ItaT4v1P3wkPHjx48G7jPVmU1OZohKb2hph8dnjw4MGDl8Frh4vJQrgd0NY1puHBgwcPXry3nLrRtxTmsXAxuEHhwYMHD14X79QhvHnQ2tjQ8ODBgwcvvz7biugUUqOBMBle4MGDBw9efp7XKIpPvaiTSmvtxvTl9YcHDx48eJHzr/3iTbu4TgWW9vA29ezw4MGDB6+Ll3rgdrHcbr6n1hAePHjw4M3htcNLo0h/C9KxqQI8ePDgwasWxY3AkiquG+HoNY1pePDgwYM3Vjg3CtJ2UdweIFea4/DgwYMH73a9u23I2fjd1Gcn1xMePHjw4GXwrh68k8V7qkHcBlsxVYAHDx48eMfBNjSOUwFq3VQBHjx48ODdxouloEKI2Jz6VgQWePDgwYN3++BtD10bhXP73lJruPrtMXjw4MH7YLz2yzapgrcRjtphLRVw4MGDBw/e/aFr49rwW082UypcVIIMPHjw4ME7cdbWQ0ojjDRyQAogNSiGBw8ePHjnb7qxoO0NN3nPlz8LDx48ePBu4214UacdNFKD5ckAWJ8qwIMHDx68eKO23fxtBKjGsLpSpMODBw8evFV4jaI7tTka99BuMsCDBw8evMwhmSp4Gws6OXGe3KwrhrHw4MGD98F4o43U8iB0spGdWvRRJHjw4MH7f3g/srP3AoM7TQkAAAAASUVORK5CYII=",
                TaxControlCode = "0092--9<+4-+/8+>-0<1<>157731<+/82/>8*//1+>75-36+9960016643*160///7-25/*<7>34+02>/-24892577>8548+690161611992<795",
                Buyer = new BuyerInfo
                {
                    BuyerName = "百望股份有限公司",
                    BuyerTaxNo = "91110108339805094M",
                    BuyerAddressTel = "北京市海淀区北清路81号一区1号楼14层15层 010-84782665",
                    BuyerBankAccount = "中国建设银行北京苏州桥支行11001079800053014887"
                },
                Seller = new SellerInfo
                {
                    SellerName = "ukey测试",
                    SellerTaxNo = "91110101202001201",
                    SellerAddressTel = "重庆市沙坪坝区狮子路11号",
                    SellerBankAccount = "重庆商业银行624562566022125"
                },
                TaxInclusiveTotalAmount = "10000.00",
                TaxInclusiveTotalAmountWithWords = "壹万圆整",
                TaxExclusiveTotalAmount = "9709.00",
                TaxTotalAmount = "291.00",
                Note = "稻谷交易",
                InvoiceClerk = "李大山",
                Payee = "付强",
                Checker = "王宝宝",
                Signature = string.Empty,
                DeductibleAmount = string.Empty,
                OriginalInvoiceCode = string.Empty,
                OriginalInvoiceNo = string.Empty,
                GoodsInfos = Enumerable.Range(0, 2).Select(f => new GoodsInfo
                {
                    LineNo = f,
                    LineNature = 0,
                    Item = "*谷物*稻谷",
                    Code = "1010101010000000000",
                    Specification = string.Empty,
                    MeasurementDimension = "克",
                    Price = "97.09",
                    Quantity = "1",
                    Amount = "97.09",
                    TaxScheme = "3%",
                    TaxAmount = "2.91",
                    PreferentialMark = string.Empty,
                    FreeTaxMark = string.Empty,
                    VATSpecialManagement = string.Empty
                }).ToList()
            };
            return invoiceInfo;
        }


        private static XElement CreateInvoiceElement(InvoiceInfo invoiceInfo)
        {
            XNamespace invoiceNamespace = "http://www.edrm.org.cn/schema/e-invoice/2019";
            string invoiceXmls = "http://www.edrm.org.cn/schema/e-invoice/2019";
            XElement element = new XElement("eInvoice", new XAttribute(XNamespace.Xmlns + "fp", invoiceXmls), new XAttribute("Version", "1.0"));

            element.Add(new XElement(invoiceNamespace + "DocID", invoiceInfo.DocId));
            element.Add(new XElement(invoiceNamespace + "AreaCode", invoiceInfo.AreaCode));
            element.Add(new XElement(invoiceNamespace + "TypeCode", invoiceInfo.TypeCode));
            element.Add(new XElement(invoiceNamespace + "InvoiceSIA2", invoiceInfo.InvoiceSIA2));
            element.Add(new XElement(invoiceNamespace + "InvoiceSIA1", invoiceInfo.InvoiceSIA1));
            element.Add(new XElement(invoiceNamespace + "InvoiceCode", invoiceInfo.InvoiceCode));
            element.Add(new XElement(invoiceNamespace + "InvoiceNo", invoiceInfo.InvoiceNo));
            element.Add(new XElement(invoiceNamespace + "IssueDate", new XCData(invoiceInfo.IssueDate)));
            element.Add(new XElement(invoiceNamespace + "InvoiceCheckCode", new XCData(invoiceInfo.InvoiceCheckCode)));
            element.Add(new XElement(invoiceNamespace + "MachineNo", invoiceInfo.MachineNo));
            element.Add(new XElement(invoiceNamespace + "GraphCode", new XCData(invoiceInfo.GraphCode)));
            element.Add(new XElement(invoiceNamespace + "TaxControlCode", new XCData(invoiceInfo.TaxControlCode)));

            XElement buyerElement = new XElement(invoiceNamespace + "Buyer");
            buyerElement.Add(new XElement("BuyerName", new XCData(invoiceInfo.Buyer.BuyerName)));
            buyerElement.Add(new XElement("BuyerTaxID", invoiceInfo.Buyer.BuyerTaxNo));
            buyerElement.Add(new XElement("BuyerAddrTel", new XCData(invoiceInfo.Buyer.BuyerAddressTel)));
            buyerElement.Add(new XElement("BuyerFinancialAccount", new XCData(invoiceInfo.Buyer.BuyerBankAccount)));
            element.Add(buyerElement);

            XElement sellerElement = new XElement(invoiceNamespace + "Seller");
            sellerElement.Add(new XElement("SellerName", new XCData(invoiceInfo.Seller.SellerName)));
            sellerElement.Add(new XElement("SellerTaxID", invoiceInfo.Seller.SellerTaxNo));
            sellerElement.Add(new XElement("SellerAddrTel", new XCData(invoiceInfo.Seller.SellerAddressTel)));
            sellerElement.Add(new XElement("SellerFinancialAccount", new XCData(invoiceInfo.Seller.SellerBankAccount)));
            element.Add(sellerElement);

            element.Add(new XElement(invoiceNamespace + "TaxInclusiveTotalAmount", invoiceInfo.TaxInclusiveTotalAmount));
            element.Add(new XElement(invoiceNamespace + "TaxInclusiveTotalAmountWithWords", invoiceInfo.TaxInclusiveTotalAmountWithWords));
            element.Add(new XElement(invoiceNamespace + "TaxExclusiveTotalAmount", invoiceInfo.TaxExclusiveTotalAmount));
            element.Add(new XElement(invoiceNamespace + "TaxTotalAmount", invoiceInfo.TaxTotalAmount));
            element.Add(new XElement(invoiceNamespace + "Note", new XCData(invoiceInfo.Note)));
            element.Add(new XElement(invoiceNamespace + "InvoiceClerk", new XCData(invoiceInfo.InvoiceClerk)));
            element.Add(new XElement(invoiceNamespace + "Payee", new XCData(invoiceInfo.Payee)));
            element.Add(new XElement(invoiceNamespace + "Checker", new XCData(invoiceInfo.Checker)));
            element.Add(new XElement(invoiceNamespace + "Signature", invoiceInfo.Signature));
            element.Add(new XElement(invoiceNamespace + "DeductibleAmount", invoiceInfo.DeductibleAmount));
            element.Add(new XElement(invoiceNamespace + "OriginalInvoiceCode", invoiceInfo.OriginalInvoiceCode));
            element.Add(new XElement(invoiceNamespace + "OriginalInvoiceNo", invoiceInfo.OriginalInvoiceNo));

            XElement goodsElement = new XElement(invoiceNamespace + "GoodsInfos");
            foreach (GoodsInfo goods in invoiceInfo.GoodsInfos)
            {
                XElement goodsInfoElement = new XElement(invoiceNamespace + "GoodsInfo");
                goodsInfoElement.Add(new XElement(invoiceNamespace + "LineNo", goods.LineNo));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "LineNature", goods.LineNature));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "Item", new XCData(goods.Item)));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "Code", goods.Code));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "Specification", goods.Specification));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "MeasurementDimension", new XCData(goods.MeasurementDimension)));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "Price", goods.Price));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "Quantity", goods.Quantity));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "Amount", goods.Amount));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "TaxScheme", goods.TaxScheme));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "TaxAmount", goods.TaxAmount));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "PreferentialMark", goods.PreferentialMark));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "FreeTaxMark", goods.FreeTaxMark));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "VATSpecialManagement", goods.VATSpecialManagement));
                goodsElement.Add(goodsInfoElement);
            }
            element.Add(goodsElement);

            XElement systemInfoElement = new XElement(invoiceNamespace + "SystemInfos");
            systemInfoElement.Add(new XElement(invoiceNamespace + "SystemCode", "bw"));
            systemInfoElement.Add(new XElement(invoiceNamespace + "SystemName", "百望云"));
            systemInfoElement.Add(new XElement(invoiceNamespace + "SystemType", "2"));
            element.Add(systemInfoElement);

            return element;
        }

        private static XElement CreateInvoiceTagElement()
        {
            XNamespace invoiceNamespace = "http://www.edrm.org.cn/schema/e-invoice/2019";
            string invoiceXmls = "http://www.edrm.org.cn/schema/e-invoice/2019";
            XElement element = new XElement("eInvoice", new XAttribute(XNamespace.Xmlns + "fp", invoiceXmls));
            element.Add(new XElement("InvoiceCode", new XElement(invoiceNamespace + "ObjectRef", "64", new XAttribute("PageRef", "1"))));

            XElement buyerElement = new XElement("Buyer");
            buyerElement.Add(new XElement("BuyerName", new XElement(invoiceNamespace + "ObjectRef", "65", new XAttribute("PageRef", "1"))));
            buyerElement.Add(new XElement("BuyerTaxID", new XElement(invoiceNamespace + "ObjectRef", "66", new XAttribute("PageRef", "1"))));
            buyerElement.Add(new XElement("BuyerAddrTel", new XElement(invoiceNamespace + "ObjectRef", "76", new XAttribute("PageRef", "1"))));
            buyerElement.Add(new XElement("BuyerFinancialAccount", new XElement(invoiceNamespace + "ObjectRef", "79", new XAttribute("PageRef", "1"))));
            element.Add(buyerElement);

            XElement sellerElement = new XElement("Seller");
            sellerElement.Add(new XElement("SellerName", new XElement(invoiceNamespace + "ObjectRef", "68", new XAttribute("PageRef", "1"))));
            sellerElement.Add(new XElement("SellerTaxID", new XElement(invoiceNamespace + "ObjectRef", "69", new XAttribute("PageRef", "1"))));
            sellerElement.Add(new XElement("SellerAddrTel", new XElement(invoiceNamespace + "ObjectRef", "73", new XAttribute("PageRef", "1"))));
            sellerElement.Add(new XElement("SellerFinancialAccount", new XElement(invoiceNamespace + "ObjectRef", "77", new XAttribute("PageRef", "1"))));
            element.Add(sellerElement);

            return element;
        }
    }
}