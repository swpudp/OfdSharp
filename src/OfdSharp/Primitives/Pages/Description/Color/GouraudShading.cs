using System.Collections.Generic;

namespace OfdSharp.Primitives.Pages.Description.Color
{
    /// <summary>
    /// 高洛德渐变
    /// 高洛德渐变的基本原理是指定三个带有可选颜色的顶点，在其构成的三角形区域内
    /// 采用高洛德算法绘制渐变图形。
    /// </summary>
    public class GouraudShading
    {
        /// <summary>
        /// 在渐变控制点所确定范围之外的部分是否填充0为不填充,1表示填充默认值为0
        /// </summary>
        public int Extend { get; set; }

        /// <summary>
        /// 渐变控制点列表
        /// </summary>
        public List<ShadingPoint> Points { get; set; }

        /// <summary>
        /// 渐变范围外的填充颜色
        /// </summary>
        public CtColor BackColor { get; set; }
    }
}
