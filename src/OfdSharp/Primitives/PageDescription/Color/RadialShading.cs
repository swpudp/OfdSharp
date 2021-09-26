using System.Collections.Generic;

namespace OfdSharp.Primitives.PageDescription.Color
{
    /// <summary>
    /// 径向渐变：
    /// 径向渐变定义了两个离心率和倾斜角度均相同的椭圆，并在椭圆边缘连线区域内进行渐变绘制的方法。
    /// 具体算法是，先由起始点椭圆中心点开始绘制一个起始点颜色的空心矩形，
    /// 随后沿着中心点连线不断绘制离心率与倾角角度相同的空心椭圆，颜色由起始点颜色逐渐渐变为结束点颜色，椭圆大小由起始点椭圆主键变为结束点椭圆。
    /// 当轴向渐变某个方向设定为延伸时（Extend 不等于 0），渐变应沿轴在该方向的延长线延伸到超出裁剪区在该轴线的投影区域为止。
    /// 当 MapType 为 Direct 时，延伸区域的渲染颜色使用该方向轴点所在的段的颜色；否则，按照在轴线区域内的渲染规则进行渲染。
    /// </summary>
    public class RadialShading
    {
        /// <summary>
        /// 渐变绘制的方式
        /// </summary>
        public MapType MapType { get; set; }

        /// <summary>
        /// 轴线一个渐变区间的长度
        /// </summary>
        public double MapUnit { get; set; }

        /// <summary>
        /// 两个椭圆的离心率,即椭圆焦距与长轴的比值,取值范围是[0,1.0)默认值为0,在这种情况下椭圆退化为圆
        /// </summary>
        public double Eccentricity { get; set; }

        /// <summary>
        /// 两个椭圆的倾斜角度,椭圆长轴与x轴正向的夹角,单位为度默认值为0
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// 起始椭圆的的中心点
        /// </summary>
        public Position StartPoint { get; set; }

        /// <summary>
        /// 结束椭圆的的中心点
        /// </summary>
        public Position EndPoint { get; set; }

        /// <summary>
        /// 起始椭圆的长半轴
        /// </summary>
        public double StartRadius { get; set; }

        /// <summary>
        /// 结束椭圆的长半轴
        /// </summary>
        public double EndRadius { get; set; }

        /// <summary>
        /// 轴线延长线方向是否继续绘制
        /// </summary>
        public ExtendType Extend { get; set; }

        /// <summary>
        /// 颜色段
        /// </summary>
        public List<Segment> Segments { get; set; }
    }
}
