using System.Xml;

namespace OfdSharp.Core.PageDescription.Color
{
    /// <summary>
    /// 颜色族
    /// 用于标识属于颜色的一种，颜色可以是基本颜色、底纹和渐变
    /// </summary>
    public class ColorClusterType : OfdElement
    {
        public ColorClusterType(XmlDocument xmlDocument, string name) : base(xmlDocument, name)
        {
        }
    }
}
