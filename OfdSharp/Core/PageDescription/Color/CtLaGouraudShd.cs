using System.Xml;

namespace OfdSharp.Core.PageDescription.Color
{
    /// <summary>
    /// 网格高洛德渐变
    /// 网格高洛德渐变是高洛德渐变的一种特殊形式，
    /// 允许定义 4 个以上的控制点，按照每行固定的网格数（VerticesPerRow）
    /// 形成若干行列，相邻的 4 个控制点定义一个网格单元，在
    /// 一个网格单元内 EdgeFlag 固定为 1，网格单元及多个单元组成的网格区域的规则如图42所示。
    /// </summary>
    public class CtLaGouraudShd : OfdElement
    {
        public CtLaGouraudShd(XmlDocument xmlDocument) : base(xmlDocument, "LaGouraudShd")
        {
        }

        /// <summary>
        /// 渐变区域内每行的网格数
        /// </summary>
        public int VerticesPerRow { get; set; }

        /// <summary>
        /// 渐变控制点
        /// </summary>
        public Point Point { get; set; }
    }
}
