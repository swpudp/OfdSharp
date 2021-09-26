using OfdSharp.Primitives.PageDescription.Color;

namespace OfdSharp.Primitives.Image
{
    /// <summary>
    /// 图像边框
    /// 10 表 43
    /// </summary>
    public class Border 
    {
        /// <summary>
        /// 边框线宽
        /// 如果为 0 则表示边框不进行绘制
        /// 默认值为 0.353 mm
        /// </summary>
        public double LineWidth { get; set; }

        /// <summary>
        /// 边框水平角半径
        /// </summary>
        public double HorizontalCornerRadius { get; set; }

        /// <summary>
        /// 边框垂直角半径
        /// </summary>
        public double VerticalCornerRadius { get; set; }

        /// <summary>
        /// 边框虚线重复样式开始的位置
        /// 边框的起点位置为左上角，绕行方向为顺时针
        /// 默认值为 0
        /// </summary>
        public double DashOffset { get; set; }

        /// <summary>
        /// 边框虚线重复样式
        /// 边框的起点位置为左上角，绕行方向为顺时针
        /// </summary>
        public Array DashPattern { get; set; }

        /// <summary>
        /// 边框颜色
        /// 有关边框颜色描述见 8.3.2 基本颜色
        /// 默认为黑色
        /// </summary>
        public CtColor BorderColor { get; set; }
    }
}
