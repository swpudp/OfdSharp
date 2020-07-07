using System.Collections.Generic;
using System.Xml;

namespace OfdSharp.Core.PageDescription.Color
{
    /// <summary>
    /// 高洛德渐变
    /// 高洛德渐变的基本原理是指定三个带有可选颜色的顶点，在其构成的三角形区域内
    /// 采用高洛德算法绘制渐变图形。
    /// </summary>
    public class CtGouraudShd : OfdElement
    {
        public CtGouraudShd(XmlDocument xmlDocument) : base(xmlDocument, "GouraudShd")
        {
        }

        /// <summary>
        /// 在渐变控制点所确定的部分是否填充
        /// </summary>
        public bool Extend { get; set; }

        /// <summary>
        /// 渐变控制点列表
        /// </summary>
        public IList<Point> Points { get; set; }

        /// <summary>
        /// 渐变范围外的填充颜色
        /// </summary>
        public CtColor BackColor { get; set; }
    }
}
