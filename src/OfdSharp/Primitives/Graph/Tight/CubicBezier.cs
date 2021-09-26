namespace OfdSharp.Primitives.Graph.Tight
{
    /// <summary>
    /// todo 三次贝塞尔曲线
    /// 三次贝塞尔曲线，
    /// 三次贝塞尔曲线公式
    /// <code>
    /// B(t) = (1-t)^3(P0) + 3t(1-t)^2(P1) + 3t^2(1-t)(P2) + t^3(P3) t∈[0,1]
    /// </code>
    /// </summary>
    public class CubicBezier
    {
        /// <summary>
        /// 三次贝塞尔曲线的第一个控制点
        /// </summary>
        public Position Point1 { get; set; }

        /// <summary>
        /// 三次贝塞尔曲线的第二个控制点
        /// </summary>
        public Position Point2 { get; set; }

        /// <summary>
        /// 三次贝塞尔曲线的结束点，下一路径的起始点
        /// </summary>
        public Position Point3 { get; set; }
    }
}
