using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using OfdSharp.Primitives.Signature;
using OfdSharp.Primitives.Signatures;
using OfdSharp.Primitives.Text;
using OfdSharp.Reader;
using OfdSharp.Sign;
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
            var ofdRoot = new OfdRoot
            {
                DocBody = new DocBody
                {
                    DocInfo = docInfo,
                    DocRoot = new Location($"Doc_{0}/Document.xml"),
                    Signatures = new Location($"Doc_{0}/Signs/Signatures.xml")
                }
            };
            writer.WriteOfdRoot(ofdRoot);

            CommonData commonData = new CommonData
            {
                MaxUnitId = new Id(100),
                PageArea = new PageArea { Application = new Box(0, 0, 1000, 100), Physical = new Box(0, 0, 100, 100) },
                ColorSpace = new RefId(190),
                DocumentRes = new Location("DocumentRes.xml"),
                //MaxUnitId = new Id(100),
                PublicRes = new Location("PublicRes.xml"),
                //PageArea = new PageArea { Application = new Box(0, 0, 1000, 100), Physical = new Box(0, 0, 100, 100) },
                TemplatePages = new List<TemplatePage>(),
            };
            var pageNodes = new List<OfdSharp.Primitives.Pages.Tree.PageNode>
            {
                new OfdSharp.Primitives.Pages.Tree.PageNode { Id = new Id(20), BaseLoc = new Location ( "Pages/Page_0/Content.xml" ) }
            };
            CtDocument ctDocument = new CtDocument
            {
                CommonData = commonData,
                Pages = pageNodes
            };
            writer.WriteDocument(ctDocument);

            DocumentResource res = new DocumentResource
            {
                BaseLoc = new Location("Res"),
                DrawParams = new List<OfdSharp.Primitives.Pages.Description.DrawParam.CtDrawParam> {

                    new OfdSharp.Primitives.Pages.Description.DrawParam.CtDrawParam
                    {
                        Id = new Id(20),
                        LineWidth = 0.25,
                        FillColor = new OfdSharp.Primitives.Pages.Description.Color.CtColor("10 10 10 20")
                        {
                            ColorSpace=new RefId (25) ,
                        },
                        StrokeColor = new OfdSharp.Primitives.Pages.Description.Color.CtColor("10 20 10 20")
                        {
                            ColorSpace=new RefId (25) ,
                        }
                    }
                },
                MultiMedias = new List<CtMultiMedia>
                {
                   new CtMultiMedia { Format = "GBIG2", Type = MediaType.Image, Id = new Id(78), MediaFile = new Location ( "image_78.jb2" ) },
                   new CtMultiMedia { Format = "GBIG2", Type = MediaType.Image, Id = new Id(79), MediaFile = new Location ( "image_80.jb2" ) }
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


            //XElement attachment = InvoiceInfoBuilder.CreateInvoiceElement(invoiceInfo);
            //writer.AddAttachment("original_invoice", "original_invoice.xml", "xml", false, attachment);

            writer.WriteTemplate(GetTemplatePageObjects());

            CustomTag customTag = new CustomTag { FileLoc = new Location("CustomTag.xml"), TypeId = "0" };
            XElement tag = InvoiceInfoBuilder.CreateInvoiceTagElement();
            writer.WriteCustomerTag(customTag, tag);



            writer.WriteAnnotation(new OfdSharp.Primitives.Annotations.AnnotationInfo
            {
                Id = new Id(1000),
                Type = OfdSharp.Primitives.Annotations.AnnotationType.Link,
                Parameters = new List<OfdSharp.Primitives.Annotations.Parameter>
                {
                    new OfdSharp.Primitives.Annotations.Parameter
                    {
                        Name=Guid.NewGuid().ToString("N"),
                        Value=Guid.NewGuid().ToString()
                    }
                },
                Appearance = new PageBlock
                {
                    PathObject = new CtPath
                    {
                        Boundary = new Box(10, 10, 10, 10),
                        Id = new Id(109)
                    },
                }



            }, new OfdSharp.Primitives.Annotations.RefPage { PageId = new RefId(100), FileLoc = new Location("Doc_0/Annots/Page_0/Annotation.xml") });

            var signInfo = new SignedInfo
            {
                Provider = new Provider { Company = "gomain", ProviderName = "gomain_eseal", Version = "2.0" },
                SignatureMethod = SesSigner.SignatureMethod.Id,
                SignatureDateTime = DateTime.Now.ToString("yyyyMMddhh:mm:ss.fffz"),
                ReferenceCollect = new ReferenceCollect
                {
                    CheckMethod = SesSigner.DigestMethod.Id,
                    Items = new List<Reference>()
                },
                StampAnnot = new StampAnnot { Boundary = new Box(10, 10, 10, 10), Id = new Id(100), PageRef = new RefId(1001) }
            };
            writer.WriteSignature(signInfo);


            //印章
            var sealCert = Sm2Utils.MakeCert(sealKey.PublicKey, sealKey.PrivateKey, "yzw", "tax");

            //签章者
            var signerCert = Sm2Utils.MakeCert(signerKey.PublicKey, signerKey.PrivateKey, "yzw", "tax");

            SesSealConfig config = new SesSealConfig
            {
                Manufacturer = "GOMAIN",
                SealName = "测试全国统一发票监制章国家税务总局重庆市税务局",
                EsId = "50011200000001",
                SealCert = sealCert.GetEncoded(),
                SealPrivateKey = sealKey.PrivateKey,
                SealPicture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Files", "image_78.jb2")),
                SealType = "ofd",
                SealWidth = 30,
                SealHeight = 20,
                SignerPrivateKey = signerKey.PrivateKey,
                SignerCert = signerCert.GetEncoded()
            };
            writer.ExecuteSign(config);

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
                            DrawParam = new RefId(4),
                            PageBlocks = new List<PageBlock>
                            {
                                new PageBlock
                                {
                                    PathObject = new OfdSharp.Primitives.Graph.CtPath
                                    {
                                        Id = new Id(6),
                                        Boundary = new Box(68.5, 17.8, 73, 0.4),
                                        LineWidth = 0.25,
                                        FillColor = new OfdSharp.Primitives.Pages.Description.Color.CtColor("156 82 35")
                                        {
                                            ColorSpace = new RefId(5)
                                        },
                                        StrokeColor = new OfdSharp.Primitives.Pages.Description.Color.CtColor("156 82 35")
                                        {
                                            ColorSpace = new RefId(5)
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
                                        FillColor = new OfdSharp.Primitives.Pages.Description.Color.CtColor("156 82 35")
                                        {
                                            ColorSpace = new RefId                                            (5)
                                        },
                                        StrokeColor = new OfdSharp.Primitives.Pages.Description.Color.CtColor("156 82 35")
                                        {
                                            ColorSpace = new RefId(5)
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
                                        FillColor = new OfdSharp.Primitives.Pages.Description.Color.CtColor("156 82 35")
                                        {
                                            ColorSpace = new RefId(5)

                                        },
                                        StrokeColor = new OfdSharp.Primitives.Pages.Description.Color.CtColor("156 82 35")
                                        {
                                            ColorSpace = new RefId(5)

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
                                        Font = new RefId (29),
                                        Size = 3.175,
                                        FillColor = new OfdSharp.Primitives.Pages.Description.Color.CtColor("156 82 35")
                                        {
                                        },
                                        TextCode = new TextCode("开票日期：")
                                        {
                                            X = 0.1,
                                            Y = 2.734,
                                            DeltaX = new OfdSharp.Primitives.Array("3.175 3.175 3.175 3.175"),
                                        }
                                    }
                                },
                                new PageBlock
                                {
                                    TextObject = new CtText
                                    {
                                        Id = new Id(33),
                                        Boundary = new Box(148.5, 24, 16, 3.6),
                                        Font = new RefId (29),
                                        Size = 3.175,
                                        FillColor = new OfdSharp.Primitives.Pages.Description.Color.CtColor("156 82 35")
                                        {
                                        },
                                        TextCode = new TextCode("校验码：")
                                        {
                                            X = 0.1,
                                            Y = 2.734,
                                            DeltaX = new OfdSharp.Primitives.Array("4.86 4.87 3.175"),
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
                PageRes = new Location("Pages/Page_0/Content.xml"),
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
                                        Font = new RefId (60),
                                        Size = 6.61,
                                        FillColor = new CtColor("156 82 35") ,
                                        TextCode = new TextCode("北京增值税电子普通发票") {X = 0, Y = 5.683674, DeltaX = new OfdSharp.Primitives.Array("10 6.61")}
                                    }
                                },
                                new PageBlock
                                {
                                    PathObject = new CtPath
                                    {
                                        Id = new Id(304),
                                        Boundary = new Box(68.5, 18, 73, 0.25),
                                        LineWidth = 0.25,
                                        StrokeColor = new CtColor("156 82 35"),
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
                                        StrokeColor = new CtColor("156 82 35") ,
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
                                        ResourceId = new RefId(311)
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
                }).ToList(),
                SystemInfo = new SystemInfo
                {
                    SystemCode = "bw",
                    SystemName = "百望",
                    SystemType = "2"
                }
            };
            return invoiceInfo;
        }





    }
}