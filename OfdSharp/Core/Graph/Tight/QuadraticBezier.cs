using OfdSharp.Primitives;
using System.Xml;

namespace OfdSharp.Core.Graph.Tight
{
    /// <summary>
    /// 二次贝塞尔曲线结构
    /// 二次贝塞尔曲线公式
    /// <code>
    /// B(t) = (1 - t)^2 + 2t(1 - t)(P1) + t^2(P2),t ∈ [0,1]
    /// </code>
    /// </summary>
    public class QuadraticBezier : OfdElement
    {
        public QuadraticBezier(XmlDocument xmlDocument) : base(xmlDocument, "QuadraticBezier")
        {
        }

        /// <summary>
        /// 二次贝塞尔曲线的控制点
        /// </summary>
        public Position Point1 { get; set; }

        /// <summary>
        /// 二次贝塞尔曲线的结束点
        /// </summary>
        public Position Point2 { get; set; }
    }
}
