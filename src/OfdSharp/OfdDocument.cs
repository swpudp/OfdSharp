using OfdSharp.Crypto;
using OfdSharp.Primitives;
using OfdSharp.Primitives.Annotations;
using OfdSharp.Primitives.Attachments;
using OfdSharp.Primitives.CustomTags;
using OfdSharp.Primitives.Doc;
using OfdSharp.Primitives.Entry;
using OfdSharp.Primitives.Graph;
using OfdSharp.Primitives.Image;
using OfdSharp.Primitives.Pages.Description.Color;
using OfdSharp.Primitives.Pages.Object;
using OfdSharp.Primitives.Resources;
using OfdSharp.Primitives.Signature;
using OfdSharp.Primitives.Text;
using OfdSharp.Sign;
using OfdSharp.Writer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace OfdSharp
{
    /// <summary>
    /// OFD文档
    /// </summary>
    public class OfdDocument
    {
        private readonly DocumentResource _resource;
        private int _unitId;
        private static readonly Dictionary<DocUsage, string> DocUsages = new Dictionary<DocUsage, string>
        {
            [DocUsage.Normal] = "Normal",
            [DocUsage.EBook] = "EBook",
            [DocUsage.ENewsPaper] = "EMagazine",
            [DocUsage.EMagazine] = "EMagazine"
        };
        private readonly List<CustomTag> _customTags;
        private readonly List<XElement> _tagElements;
        //附件
        private readonly List<Attachment> _attachments;
        private readonly List<XElement> _attachmentElements;
        private readonly List<OfdPage> _ofdPages;
        private AnnotationInfo _annotationInfo;
        private RefPage _refPage;
        private SignedInfo _signedInfo;
        private SesSealConfig _sesSealConfig;
        //private CommonData _commonData;
        private readonly OfdRoot _ofdRoot;
        private readonly CtDocument _ctDocument;


        public OfdDocument(OfdDocumentInfo documentInfo)
        {
            _resource = new DocumentResource
            {
                BaseLoc = new Location("Res"),
                DrawParams = new List<Primitives.Pages.Description.DrawParam.CtDrawParam>
                {
                    new Primitives.Pages.Description.DrawParam.CtDrawParam
                    {
                        StrokeColor = new CtColor
                        {
                            ColorSpace = new RefId(10)
                        },
                        FillColor = new CtColor
                        {
                            ColorSpace = new RefId(10)
                        }
                    }
                },//todo DrawParams从哪里来？
                ColorSpaces = new List<Primitives.Pages.Description.ColorSpace.CtColorSpace>
                {
                    new Primitives.Pages.Description.ColorSpace.CtColorSpace
                    {
                        Type = Primitives.Pages.Description.ColorSpace.ColorSpaceType.RGB,
                        BitsPerComponent = Primitives.Pages.Description.ColorSpace.BitsPerComponent.Bit8
                    }
                },//todo CtColorSpace从哪里来？
                CompositeGraphicUnits = new List<Primitives.Composite.VectorGraph>(),//todo CompositeGraphicUnits从哪里来？
                Fonts = new List<Primitives.Fonts.CtFont>
                {
                    new Primitives.Fonts.CtFont
                    {
                        Id = new Id(200),
                        FontName="宋体"
                    }
                },//todo Fonts从哪里来？
                MultiMedias = new List<CtMultiMedia>()//todo MultiMedias从哪里来？
            };
            _customTags = new List<CustomTag>();
            _tagElements = new List<XElement>();

            _attachments = new List<Attachment>();
            _attachmentElements = new List<XElement>();
            _ofdPages = new List<OfdPage>();

            _ofdRoot = new OfdRoot
            {
                DocBody = new DocBody
                {
                    DocInfo = new CtDocInfo
                    {
                        DocId = Guid.NewGuid().ToString("N"),
                        Title = documentInfo.Title,
                        Author = documentInfo.Author,
                        Subject = documentInfo.Subject,
                        Abstract = documentInfo.Abstract,
                        CreationDate = DateTime.Now,
                        DocUsage = GetDocUsageDesc(documentInfo.DocUsage),
                        Cover = documentInfo.Cover,
                        Keywords = documentInfo.Keywords,
                        Creator = documentInfo.Creator,
                        CreatorVersion = documentInfo.CreatorVersion,
                        CustomDataList = documentInfo.CustomDataList
                    }
                }
            };
            _ctDocument = new CtDocument
            {
                CommonData = new CommonData
                {
                    ColorSpace = new RefId(180),//todo ColorSpace在哪里定义？
                    DocumentRes = new Location("DocumentRes.xml"),
                    //PublicRes = new Location("PublicRes.xml"),
                    //TemplatePages = new List<TemplatePage>(),//todo 页面模板如何使用
                }
            };
        }

        private static string GetDocUsageDesc(DocUsage? docUsage)
        {
            return docUsage.HasValue ? DocUsages[docUsage.Value] : DocUsage.Normal.ToString();
        }
        public DocumentResource DocumentResource => _resource;
        public List<OfdPage> Pages => _ofdPages;
        public List<Attachment> Attachments => _attachments;
        public List<XElement> AttachmentElements => _attachmentElements;


        public OfdPage AddPage()
        {
            return AddPage(PageSize.A4);
        }

        public OfdPage AddPage(PageSize pageSize)
        {
            _ctDocument.CommonData.PageArea.Physical = new Box(pageSize.X, pageSize.Y, pageSize.Width, pageSize.Height);
            Interlocked.Increment(ref _unitId);
            var pageNode = new Primitives.Pages.Tree.PageNode
            {
                Id = new Id(_unitId),
                BaseLoc = new Location($"Pages/Page_{Math.Max(0, _ofdPages.Count - 1)}/Content.xml")
            };
            OfdPage ofdPage = new OfdPage(pageNode, pageSize);
            _ofdPages.Add(ofdPage);

            _refPage = new RefPage { PageId = new RefId(100), FileLoc = new Location($"Page_{_ofdPages.Count - 1}/Annotation.xml") };

            _ofdRoot.DocBody.DocRoot = new Location($"Doc_{0}/Document.xml");

            return ofdPage;
        }

        private List<PageObject> GetPageObjects()
        {
            List<PageObject> pageObjects = new List<PageObject>();
            foreach (var page in _ofdPages)
            {
                PageObject pg = page.CreatePageObject();
                pageObjects.Add(pg);
            }
            return pageObjects;
        }

        public void AddCustomTag(CustomTag customTag, XElement tagElement)
        {
            _customTags.Add(customTag);
            _tagElements.Add(tagElement);
        }

        public void AddAttachment(string name, string fileName, string format, bool visible, XElement content)
        {
            Attachment attachment = new Attachment
            {
                Id = new Id(10),
                Name = name,
                FileLoc = new Location(fileName),
                Visible = visible,
                Format = format
            };
            _attachments.Add(attachment);
            _attachmentElements.Add(content);
        }

        public void AddAnnotationInfo(AnnotationInfo annotationInfo)
        {
            _annotationInfo = annotationInfo;
            //todo AnnotationInfo的作用是什么？
            //_annotationInfo = new AnnotationInfo
            //{
            //    Type = AnnotationType.Stamp,
            //    Id = new Id(93),
            //    Subtype = "SignatureInFile",
            //    Parameters = new List<Parameter>
            //    {
            //        new Parameter
            //        {
            //            Name = "fp.NativeSign",
            //            Value = "original_invoice"
            //        }
            //    },
            //    Appearance = new PageBlock
            //    {
            //        PathObject = new Primitives.Graph.CtPath
            //        {
            //            Boundary = new Box(4.5, 104, 115, 20)
            //        }
            //    }
            //};
        }

        public void Sign(Provider provider, StampAnnot stampAnnot, SesSealConfig sesSealConfig)
        {
            _sesSealConfig = sesSealConfig;
            //todo 实例化SesSealConfig
            //SesSealConfig config = new SesSealConfig
            //{
            //    Manufacturer = "GOMAIN",
            //    SealName = "测试全国统一发票监制章国家税务总局重庆市税务局",
            //    EsId = "50011200000001",
            //    SealCert = _sealCert.GetEncoded(),
            //    SealPrivateKey = _sealKey.PrivateKey,
            //    SealPicture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Files", "image_78.jb2")),
            //    SealType = "ofd",
            //    SealWidth = 30,
            //    SealHeight = 20,
            //    SignerPrivateKey = _signerKey.PrivateKey,
            //    SignerCert = _signerCert.GetEncoded()
            //};
            _signedInfo = new SignedInfo
            {
                Provider = provider, //new Provider { Company = "gomain", ProviderName = "gomain_eseal", Version = "2.0" },
                SignatureMethod = SesSigner.SignatureMethod.Id,
                SignatureDateTime = DateTime.Now.ToString("yyyyMMddhh:mm:ss.fffz"), //"20220124060837.228Z",
                ReferenceCollect = new ReferenceCollect
                {
                    CheckMethod = SesSigner.DigestMethod.Id,
                    Items = new List<Primitives.Signatures.Reference>()
                    //{
                    //    new Primitives.Signatures.Reference
                    //    {
                    //        CheckValue = new CheckValue
                    //        {
                    //            Value= Convert.ToBase64String(Sm2Utils.Digest (c,Encoding.UTF8)),
                    //        },
                    //        FileRef="/Doc_0/Annots/Page_0/Annotation.xml"
                    //    }
                    //}
                },
                StampAnnot = stampAnnot
            };
            //DigestInfo digestInfo = new DigestInfo
            //{
            //    SignedInfo = signedInfo,
            //    SignedValue = "/Doc_0/Signs/Sign_0/SignedValue.dat"
            //};
            _ofdRoot.DocBody.Signatures = new Location($"Doc_{0}/Signs/Signatures.xml");
        }

        public byte[] Save()
        {
            OfdWriter writer = new OfdWriter(this, false);

            _ctDocument.CommonData.MaxUnitId = new Id(1000);

            writer.WriteOfdRoot(_ofdRoot);

            _ctDocument.Pages = _ofdPages.Select(f => f.PageNode).ToList();


            writer.WriteDocument(_ctDocument);


            writer.WriteDocumentRes(_resource);

            var pageObjects = GetPageObjects();
            writer.WritePages(pageObjects);

            _customTags.ForEach((item) => writer.WriteCustomerTag(item, _tagElements[_customTags.IndexOf(item)]));

            if (_annotationInfo != null)
            {
                writer.WriteAnnotation(_annotationInfo, _refPage);
            }
            if (_signedInfo != null)
            {
                writer.WriteSignature(_signedInfo);
                writer.ExecuteSign(_sesSealConfig);
            }
            return writer.Flush();
        }
    }
}
