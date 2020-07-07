using System.Xml;

namespace OfdSharp.Core.Basic.PageObj.Layer.Block
{
    /// <summary>
    /// 页块结构
    /// 可以嵌套
    /// </summary>
    public class PageBlock : PageBlockType
    {
        public PageBlock(XmlDocument xmlDocument) : base(xmlDocument, "PageBlock")
        {
        }
    }
}
