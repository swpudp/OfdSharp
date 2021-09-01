using OfdSharp.Primitives;
using System.Xml;

namespace OfdSharp.Core.Basic.Doc
{
    /// <summary>
    /// 页面区域结构
    /// </summary>
    public class PageArea : OfdElement
    {
        public PageArea(XmlDocument xmlDocument, Position topLeft, double width, double height) : base(xmlDocument, "PageArea")
        {
            Box = new Box(topLeft, width, height);
        }

        /// <summary>
        /// 页面物理区域
        /// </summary>
        public Box Box { get; }

    }
}
