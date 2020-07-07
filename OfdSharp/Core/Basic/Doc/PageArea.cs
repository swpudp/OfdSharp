using OfdSharp.Core.BaseType;
using System.Xml;

namespace OfdSharp.Core.Basic.Doc
{
    /// <summary>
    /// 页面区域结构
    /// </summary>
    public class PageArea : OfdElement
    {
        public PageArea(XmlDocument xmlDocument, double topLeftX, double topLeftY, double width, double height) : base(xmlDocument, "PageArea")
        {
            Box = new StBox(topLeftX, topLeftY, width, height);
        }

        /// <summary>
        /// 页面物理区域
        /// </summary>
        public StBox Box { get; }

    }
}
