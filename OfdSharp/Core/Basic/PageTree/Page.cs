using System.Xml;

namespace OfdSharp.Core.Basic.PageTree
{
    /// <summary>
    /// 页节点
    /// </summary>
    public class Page : OfdElement
    {
        public Page(XmlDocument xmlDocument) : base(xmlDocument, "Page")
        {
        }
    }
}
