using System.Xml;

namespace OfdSharp.Core.Graph.PathObj
{
    /// <summary>
    /// 填充颜色
    /// </summary>
    public class FillColor : OfdElement
    {
        public FillColor(XmlDocument xmlDocument) : base(xmlDocument, "FillColor")
        {
        }
    }
}
