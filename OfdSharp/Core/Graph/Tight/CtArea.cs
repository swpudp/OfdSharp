using OfdSharp.Core.BaseType;
using System.Xml;

namespace OfdSharp.Core.Graph.Tight
{
    /// <summary>
    /// 区域由一系列的分路径（Area）组成，每个路径都是闭合的
    /// </summary>
    public class CtArea : OfdElement
    {
        public CtArea(XmlDocument xmlDocument) : base(xmlDocument, "Area")
        {
        }

        /// <summary>
        /// 定义字图形的起始点坐标
        /// </summary>
        public StPosition Start { get; set; }
    }
}
