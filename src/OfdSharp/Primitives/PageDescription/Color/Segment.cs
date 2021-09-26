namespace OfdSharp.Primitives.PageDescription.Color
{
    /// <summary>
    /// 颜色段
    /// 至少出现两个
    /// </summary>
    public class Segment
    {
        /// <summary>
        /// 该段的颜色,应是基本颜色
        /// </summary>
        public CtColor Color { get; set; }

        /// <summary>
        /// 渐变段颜色位置参数， 用于确定 StartPoint 和 EndPoint 中的各颜色的位置值，
        /// 取值范围是 [0, 1.0]，各颜色的 Position 值应根据颜色出现
        /// 的顺序递增第一个 Segment 的 Position 属性默认值为 0，最后
        /// 一个 Segment 的 Position 属性默认值为 1.0，当不存在时，
        /// 在空缺的区间内平局分配。
        /// <example>
        /// Segment 个数等于 2 且不出现 Position 属性时，按照“0 1.0”处理；
        /// Segment 个数等于 3 且不出现 Position 属性时，按照“0 0.5 1.0”处理；
        /// Segment 个数等于 5 且不出现 Position 属性时，按照“0 0.25 0.5 0.75 1.0” 处理。
        /// </example>
        /// </summary>
        public double Position { get; set; }
    }
}
