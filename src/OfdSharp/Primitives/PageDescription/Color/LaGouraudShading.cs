using System.Collections.Generic;

namespace OfdSharp.Primitives.PageDescription.Color
{
    /// <summary>
    /// 网格高洛德渐变，网格高洛德渐变是高洛德渐变的一种特殊形式，
    /// 允许定义 4 个以上的控制点，按照每行固定的网格数（VerticesPerRow）
    /// 形成若干行列，相邻的 4 个控制点定义一个网格单元，在
    /// 一个网格单元内 EdgeFlag 固定为 1，网格单元及多个单元组成的网格区域的规则如图42所示。
    /// </summary>
    public class LaGouraudShading
    {
        /// <summary>
        /// 渐变区域内每行的网格数
        /// </summary>
        public int VerticesPerRow { get; set; }

        /// <summary>
        /// 在渐变控制点所确定范围之外的部分是否填充0为不填充,1表示填充默认值为0
        /// </summary>
        public int Extend { get; set; }

        /// <summary>
        /// 渐变控制点
        /// </summary>
        public List<ShadingPoint> Points { get; set; }

        /// <summary>
        /// 渐变范围外的填充颜色,应使用基本颜色
        /// </summary>
        public CtColor BackColor { get; set; }
    }
}
