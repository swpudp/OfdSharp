using System.Xml;

namespace OfdSharp.Core.Image
{
    /// <summary>
    /// 边框颜色
    /// 有关边框颜色描述见 8.3.2 基本颜色
    /// 默认为黑色
    /// </summary>
    public class BorderColor : OfdElement
    {
        public BorderColor(XmlDocument xmlDocument) : base(xmlDocument, "BorderColor")
        {
        }
    }
}
