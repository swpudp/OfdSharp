namespace OfdSharp.Core.PageDescription.Color
{
    /// <summary>
    /// 渐变控制点，至少出现三个
    /// </summary>
    public class ShdPoint 
    {
        /// <summary>
        /// 控制点水平位置
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// 控制点垂直位置
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// 三角单元切换的方向标志
        /// </summary>
        public EdgeFlag EdgeFlag { get; set; }

        /// <summary>
        /// 控制点对应的颜色
        /// </summary>
        public CtColor Color { get; set; }
    }
}
