using OfdSharp.Primitives;
using OfdSharp.Primitives.Composite;
using OfdSharp.Primitives.Graph;
using OfdSharp.Primitives.Image;
using OfdSharp.Primitives.Pages.Object;
using OfdSharp.Primitives.Pages.Tree;
using OfdSharp.Primitives.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfdSharp
{
    public class OfdPage
    {


        private readonly PageSize _pageSize;

        private readonly PageNode _pageNode;


        public List<CtText> Texts { get; }

        public List<CtPath> Paths { get; }

        public List<CtImage> Images { get; }

        /// <summary>
        /// todo 复合对象是啥？
        /// </summary>
        public List<CompositeObject> CompositeObjects { get; }

        public PageSize PageSize => _pageSize;

        public PageNode PageNode => _pageNode;

        public OfdPage(PageNode pageNode, PageSize pageSize)
        {
            Paths = new List<CtPath>();
            Texts = new List<CtText>();
            Images = new List<CtImage>();
            CompositeObjects = new List<CompositeObject>();
            _pageNode = pageNode;
            _pageSize = pageSize;
        }

        public void AddText(string text)
        {
            CtText ctText = new CtText
            {
                Id = new Id(2002),
                Boundary = new Box(0, 0, 100, 100),
                FillColor = new Primitives.Pages.Description.Color.CtColor("0", "0", "0"),
                TextCode = new TextCode(text) { X = 0, Y = 0 },
                Font = new RefId(200)//todo font在哪里定义
            };
            Texts.Add(ctText);
        }

        public void AddPath(CtPath ctPath)
        {
            Paths.Add(ctPath);
        }

        public void AddImage(CtImage image)
        {
            Images.Add(image);
        }

        public PageObject CreatePageObject()
        {
            var layer = new Layer
            {
                Id = new Id(303),
                PageBlocks = new List<PageBlock>()
            };
            int count = Math.Max(Math.Max(Math.Max(Texts.Count, Paths.Count), Images.Count), CompositeObjects.Count);
            for (int i = 0; i < count; i++)
            {
                PageBlock pg = new PageBlock
                {
                    TextObject = Texts.ElementAtOrDefault(i),
                    PathObject = Paths.ElementAtOrDefault(i),
                    ImageObject = Images.ElementAtOrDefault(i),
                    CompositeObject = CompositeObjects.ElementAtOrDefault(i)
                };
                layer.PageBlocks.Add(pg);
            }
            return new PageObject
            {
                Area = new Primitives.Doc.PageArea
                {
                    Physical = new Box(_pageSize.X, _pageSize.Y, _pageSize.Width, _pageSize.Height)
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
