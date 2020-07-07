using OfdSharp.Core.PageDescription.Color;
using System.Xml;

namespace OfdSharp.Core.Graph.PathObj
{
    /// <summary>
    /// 勾边颜色
    /// </summary>
    public class StrokeColor : CtColor
    {
        public StrokeColor(XmlDocument xmlDocument) : base(xmlDocument, "StrokeColor")
        {
        }
    }
}
