using OfdSharp.Primitives;
using OfdSharp.Primitives.Annotations;
using OfdSharp.Primitives.Attachments;
using OfdSharp.Primitives.CustomTags;
using OfdSharp.Primitives.Doc;
using OfdSharp.Primitives.Entry;
using OfdSharp.Primitives.Pages.Object;
using OfdSharp.Primitives.Resources;
using OfdSharp.Primitives.Signature;
using OfdSharp.Sign;
using OfdSharp.Writer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using OfdSharp.Primitives.Composite;
using OfdSharp.Primitives.Fonts;
using OfdSharp.Primitives.Pages.Description.ColorSpace;
using OfdSharp.Primitives.Pages.Description.DrawParam;

namespace OfdSharp
{
    /// <summary>
    /// OFD文档
    /// </summary>
    public class OfdDocument
    {
        private readonly DocumentResource _resource;
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
        private readonly CtDocument _ctDocument;
        private readonly int _index;
        private readonly IdGen _idGen = new IdGen();

        internal OfdDocument(int index)
        {
            _index = index;
            _resource = new DocumentResource
            {
                BaseLoc = new CtLocation("Res"),
                MultiMedias = new List<CtMultiMedia>(),
                Fonts = new List<CtFont>(),
                ColorSpaces = new List<CtColorSpace>(),
                CompositeGraphicUnits = new List<VectorGraph>(),
                DrawParams = new List<CtDrawParam>()
            };
            _customTags = new List<CustomTag>();
            _tagElements = new List<XElement>();

            _attachments = new List<Attachment>();
            _attachmentElements = new List<XElement>();
            _ofdPages = new List<OfdPage>();
            _ctDocument = new CtDocument
            {
                CommonData = new CommonData
                {
                    //ColorSpace = new RefId(180), //todo ColorSpace在哪里定义？
                    //DocumentRes = new Location("DocumentRes.xml"),//todo DocumentRes 如何判断要写入？
                    //PublicRes = new Location("PublicRes.xml"),
                    //TemplatePages = new List<TemplatePage>(),//todo 页面模板如何使用
                }
            };
        }

        public DocumentResource DocumentResource => _resource;
        public List<OfdPage> Pages => _ofdPages;
        public List<Attachment> Attachments => _attachments;
        public List<XElement> AttachmentElements => _attachmentElements;

        internal IdGen IdGen => _idGen;

        public OfdPage AddPage()
        {
            return AddPage(PageSize.A4);
        }

        public OfdPage AddPage(PageSize pageSize)
        {
            _ctDocument.CommonData.PageArea = new PageArea
            {
                Physical = new CtBox(pageSize.X, pageSize.Y, pageSize.Width, pageSize.Height)
            };
            int pageIndex = Math.Max(0, _ofdPages.Count - 1);
            OfdPage ofdPage = new OfdPage(pageIndex, _idGen, pageSize);
            _ofdPages.Add(ofdPage);

            _refPage = new RefPage
            {
                PageId = new CtRefId(100), FileLoc = new CtLocation($"Page_{pageIndex}/Annotation.xml")
            };
            return ofdPage;
        }

        private List<PageObject> GetPageObjects()
        {
            return _ofdPages.Select(page => page.CreatePageObject()).ToList();
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
                Id = _idGen.NewId(),
                Name = name,
                FileLoc = new CtLocation(fileName),
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
            //    Id = _idGen.NewId(),
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
            //_ofdRoot.DocBodyList[_index].Signatures = new Location($"Doc_{_index}/Signs/Signatures.xml");
        }

        internal byte[] Save(OfdRoot ofdRoot)
        {
            OfdWriter writer = new OfdWriter(this);

            //先写底层xml
            var pageObjects = GetPageObjects();
            writer.WritePages(pageObjects);

            _customTags.ForEach(item => writer.WriteCustomerTag(item, _tagElements[_customTags.IndexOf(item)]));

            if (_annotationInfo != null)
            {
                writer.WriteAnnotation(_annotationInfo, _refPage);
            }

            //DOC_N
            _ctDocument.Pages = _ofdPages.Select(f => f.PageNode).ToList();
            _resource.Fonts.AddRange(FontCache.GetCtFonts());

            if (writer.WritePublicRes(_resource))
            {
                _ctDocument.CommonData.PublicRes = new CtLocation("PublicRes.xml");
            }

            if (writer.WriteDocumentRes(_resource))
            {
                _ctDocument.CommonData.DocumentRes = new CtLocation("DocumentRes.xml");
            }

            _ctDocument.CommonData.MaxUnitId = _idGen.MaxId;
            writer.WriteDocument(_ctDocument);

            //Root
            writer.WriteOfdRoot(ofdRoot);

            //sign
            if (_signedInfo != null)
            {
                writer.WriteSignature(_signedInfo);
                writer.ExecuteSign(_sesSealConfig);
            }

            return writer.Flush();
        }
    }
}