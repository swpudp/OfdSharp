namespace OfdSharp.Primitives.Graph.Tight
{
    /// <summary>
    /// todo 二次贝塞尔曲线
    /// 二次贝塞尔曲线结构，
    /// 二次贝塞尔曲线公式
    /// <code>
    /// B(t) = (1 - t)^2 + 2t(1 - t)(P1) + t^2(P2),t ∈ [0,1]
    /// </code>
    /// </summary>
    public class QuadraticBezier
    {
        /// <summary>
        /// 二次贝塞尔曲线的控制点
        /// </summary>
        public CtPosition Point1 { get; set; }

        /// <summary>
        /// 二次贝塞尔曲线的结束点,下一路径的起始点
        /// </summary>
        public CtPosition Point2 { get; set; }
    }
}
