using OfdSharp.Primitives;
using OfdSharp.Primitives.Doc;
using OfdSharp.Primitives.Entry;
using OfdSharp.Primitives.Graph;
using OfdSharp.Primitives.Image;
using OfdSharp.Primitives.Pages.Description.Color;
using OfdSharp.Primitives.Pages.Object;
using OfdSharp.Primitives.Resources;
using OfdSharp.Primitives.Text;
using OfdSharp.Writer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OfdSharp
{
    /// <summary>
    /// OFD文档
    /// </summary>
    public class OfdDocument
    {
        private readonly CtDocInfo _docInfo;
        private readonly DocumentResource _resource;
        private int _unitId;
        private static readonly Dictionary<DocUsage, string> DocUsages = new Dictionary<DocUsage, string>
        {
            [DocUsage.Normal] = "Normal",
            [DocUsage.EBook] = "EBook",
            [DocUsage.ENewsPaper] = "EMagazine",
            [DocUsage.EMagazine] = "EMagazine"
        };
        public OfdDocument(OfdDocumentInfo documentInfo)
        {
            _docInfo = new CtDocInfo
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
            };
            _resource = new DocumentResource
            {
                BaseLoc = new Primitives.Location { Value = "Res" },
                DrawParams = new List<Primitives.Pages.Description.DrawParam.CtDrawParam>(),//todo DrawParams从哪里来？
                ColorSpaces = new List<Primitives.Pages.Description.ColorSpace.CtColorSpace>(),//todo CtColorSpace从哪里来？
                CompositeGraphicUnits = new List<Primitives.Composite.VectorGraph>(),//todo CompositeGraphicUnits从哪里来？
                Fonts = new List<Primitives.Fonts.CtFont>(),//todo Fonts从哪里来？
                MultiMedias = new List<CtMultiMedia>()//todo MultiMedias从哪里来？
            };
        }

        private static string GetDocUsageDesc(DocUsage? docUsage)
        {
            return docUsage.HasValue ? null : DocUsages[docUsage.Value];
        }

        public DocumentResource GetDocumentResource()
        {
            return _resource;
        }

        public List<OfdPage> Pages { get; }

        public OfdPage AddPage()
        {
            Interlocked.Increment(ref _unitId);
            var pageNode = new Primitives.Pages.Tree.PageNode
            {
                Id = new Primitives.Id(_unitId),
                BaseLoc = new Primitives.Location
                {
                    Value = $"Pages/Page_{Math.Max(0, Pages.Count - 1)}/Content.xml"
                }
            };
            OfdPage ofdPage = new OfdPage(pageNode);
            Pages.Add(ofdPage);
            return ofdPage;
        }

        private List<PageObject> GetPageObjects()
        {
            List<PageObject> pageObjects = new List<PageObject>();
            foreach (var page in Pages)
            {
                PageObject pg = page.CreatePageObject();
                pageObjects.Add(pg);
            }
            return pageObjects;
        }

        public void Save()
        {
            OfdWriter writer = new OfdWriter(this, false);
            writer.WriteOfdRoot(_docInfo, 0);
            CommonData commonData = new CommonData
            {
                ColorSpace = new Primitives.RefId { Id = new Primitives.Id(0) },//todo ColorSpace在哪里定义？
                DocumentRes = new Primitives.Location { Value = "DocumentRes.xml" },
                MaxUnitId = new Primitives.Id(100),//todo MaxUnitId如何计算？
                PublicRes = new Primitives.Location { Value = "PublicRes.xml" },
                PageArea = new PageArea { Application = new Primitives.Box(0, 0, 1000, 100) },//todo 页面区域在哪里？
                TemplatePages = new List<Primitives.Pages.Object.TemplatePage>(),//todo 页面模板如何使用
            };
            var pageNodes = Pages.Select(f => f.PageNode).ToList();
            writer.WriteDocument(commonData, pageNodes, false);
            writer.WriteDocumentRes(_resource);

            var pageObjects = GetPageObjects();
            writer.WritePages(pageObjects);
        }
    }
}
