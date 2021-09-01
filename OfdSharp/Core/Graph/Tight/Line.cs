using OfdSharp.Primitives;
using System.Xml;

namespace OfdSharp.Core.Graph.Tight
{
    /// <summary>
    /// 线段
    /// </summary>
    public class Line : OfdElement
    {
        public Line(XmlDocument xmlDocument, string name) : base(xmlDocument, name)
        {
        }

        /// <summary>
        /// 线段的结束点
        /// </summary>
        public Position Point1 { get; set; }
    }
}
