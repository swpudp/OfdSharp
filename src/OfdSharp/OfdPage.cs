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
        public List<CtText> Texts { get; }

        public List<CtPath> Paths { get; }

        public List<CtImage> Images { get; }

        /// <summary>
        /// todo 复合对象是啥？
        /// </summary>
        public List<CompositeObject> CompositeObjects { get; }

        public PageNode PageNode { get; }

        public OfdPage(PageNode pageNode)
        {
            Paths = new List<CtPath>();
            Texts = new List<CtText>();
            Images = new List<CtImage>();
            CompositeObjects = new List<CompositeObject>();
            PageNode = pageNode;
        }

        public void AddText(CtText text)
        {
            Texts.Add(text);
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
