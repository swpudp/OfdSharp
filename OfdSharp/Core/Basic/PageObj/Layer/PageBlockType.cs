using System.Xml;

namespace OfdSharp.Core.Basic.PageObj.Layer
{
    /// <summary>
    /// 用于表示页块类型的接口
    /// </summary>
    public class PageBlockType : OfdElement
    {
        public PageBlockType(XmlDocument xmlDocument, string name) : base(xmlDocument, name)
        {
        }
    }
}
