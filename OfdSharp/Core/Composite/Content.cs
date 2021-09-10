using System.Xml;

namespace OfdSharp.Core.CompositeObj
{
    /// <summary>
    /// 内容的矢量描述
    /// </summary>
    public class Content : OfdElement
    {
        public Content(XmlDocument xmlDocument) : base(xmlDocument, "Content")
        {
        }
    }
}
