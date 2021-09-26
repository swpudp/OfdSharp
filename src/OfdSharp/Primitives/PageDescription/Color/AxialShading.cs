using System.Collections.Generic;

namespace OfdSharp.Primitives.PageDescription.Color
{
    /// <summary>
    /// 轴向渐变：
    /// 在轴向渐变中，颜色渐变沿着一条指定的轴线方向，轴线由起始点和结束点决定，
    /// 与这条轴线垂直的直线上的点颜色相同。
    /// 当轴向渐变某个方向设定为延伸时（Extend 不等于 0），渐变应沿轴在该方向的延长线
    /// 延伸到超出裁剪区在该轴线的投影区域为止。当 MapType 为 Direct 时，延伸区域的
    /// 渲染颜色使用该方向轴点所在的段的颜色；否则，按照在轴线区域内的渲染规则进行渲染。
    /// </summary>
    public class AxialShading
    {
        /// <summary>
        /// 渐变绘制方式
        /// </summary>
        public MapType MapType { get; set; }

        /// <summary>
        /// 轴线一个渐变区间的长度
        /// </summary>
        public double MapUnit { get; set; }

        /// <summary>
        /// 轴线一个渐变区间的长度,当MapType的值不等于Direct时出现默认值为轴线长度
        /// </summary>
        public ExtendType Extend { get; set; }

        /// <summary>
        /// 轴线起始点
        /// </summary>
        public Position StartPoint { get; set; }

        /// <summary>
        /// 轴线结束点
        /// </summary>
        public Position EndPoint { get; set; }

        /// <summary>
        /// 颜色段,至少出现两个
        /// </summary>
        public List<Segment> Segments { get; set; }
    }
}
