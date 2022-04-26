using OfdSharp.Primitives;
using OfdSharp.Primitives.Composite;
using OfdSharp.Primitives.Graph;
using OfdSharp.Primitives.Image;
using OfdSharp.Primitives.Pages.Object;
using OfdSharp.Primitives.Pages.Tree;
using OfdSharp.Primitives.Text;
using System.Collections.Generic;

namespace OfdSharp
{
    public class OfdPage
    {
        private static readonly IDictionary<PageSize, Margin> MarginMapping = new Dictionary<PageSize, Margin>
        {
            [PageSize.A4] = Margin.A4
        };

        private readonly LinkedList<Paragraph> _paragraphs;
        private readonly PageSize _pageSize;
        private readonly IdGen _idGen;

        private readonly PageNode _pageNode;
        private readonly int _index;
        private readonly Margin _margin;

        public List<CtPath> Paths { get; }

        public List<CtImage> Images { get; }

        /// <summary>
        /// todo 复合对象是啥？
        /// </summary>
        public List<CompositeObject> CompositeObjects { get; }

        public PageSize PageSize => _pageSize;

        public PageNode PageNode => _pageNode;

        public int Index => _index;

        internal OfdPage(int index, IdGen idGen, PageSize pageSize)
        {
            _idGen = idGen;
            Paths = new List<CtPath>();
            Images = new List<CtImage>();
            CompositeObjects = new List<CompositeObject>();
            _pageNode = new PageNode
            {
                Id = idGen.NewId(),
                BaseLoc = new CtLocation($"Pages/Page_{index}/Content.xml")
            };
            _pageSize = pageSize;
            _margin = GetMargin(pageSize);
            _index = index;
            _paragraphs = new LinkedList<Paragraph>();
        }

        private static Margin GetMargin(PageSize pageSize)
        {
            return MarginMapping.TryGetValue(pageSize, out Margin margin) ? margin : Margin.None;
        }

        /// <summary>
        /// 新增一个段落
        /// </summary>
        /// <returns></returns>
        public Paragraph AddParagraph()
        {
            Paragraph paragraph = new Paragraph(_idGen);
            _paragraphs.AddLast(paragraph);
            return paragraph;
        }

        public void AddPath(CtPath ctPath)
        {
            Paths.Add(ctPath);
        }

        public void AddImage(CtImage image)
        {
            Images.Add(image);
        }

        public void AddCompositeObject(CompositeObject compositeObject)
        {
            CompositeObjects.Add(compositeObject);
        }

        public PageObject CreatePageObject()
        {
            var layer = new Layer
            {
                Id = _idGen.NewId(),
                PageBlocks = new List<PageBlock>()
            };
            int pIndex = 0;
            foreach (Paragraph p in _paragraphs)
            {
                pIndex++;
                List<CtText> ctTexts = p.GetCtTexts(_pageSize, _margin, pIndex);
                foreach (CtText ctText in ctTexts)
                {
                    PageBlock pg = new PageBlock
                    {
                        TextObject = ctText
                    };
                    layer.PageBlocks.Add(pg);
                }
            }

            foreach (CtPath ctPath in Paths)
            {
                PageBlock pg = new PageBlock
                {
                    PathObject = ctPath
                };
                layer.PageBlocks.Add(pg);
            }

            foreach (CtImage ctImage in Images)
            {
                PageBlock pg = new PageBlock
                {
                    ImageObject = ctImage
                };
                layer.PageBlocks.Add(pg);
            }

            foreach (CompositeObject compositeObject in CompositeObjects)
            {
                PageBlock pg = new PageBlock
                {
                    CompositeObject = compositeObject
                };
                layer.PageBlocks.Add(pg);
            }

            return new PageObject
            {
                Area = new Primitives.Doc.PageArea
                {
                    Physical = new CtBox(_pageSize.X, _pageSize.Y, _pageSize.Width, _pageSize.Height)
                },
                Content = new Content
                {
                    Layers = new List<Layer>
                    {
                        layer
                    }
                }
            };
        }
    }
}