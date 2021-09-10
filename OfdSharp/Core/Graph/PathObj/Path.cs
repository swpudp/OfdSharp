using OfdSharp.Core.PageDescription;
using OfdSharp.Core.PageDescription.Color;

namespace OfdSharp.Core.Graph.PathObj
{
    /// <summary>
    /// 图形对象：图形对象具有一般图元的一切属性和行为特征。
    /// </summary>
    public class Path : GraphicUnit
    {
        /// <summary>
        /// 图形是否被勾边。默认true
        /// </summary>
        public bool Stroke { get; set; } = true;

        /// <summary>
        /// 图形是否被填充。默认值为false
        /// </summary>
        public bool Fill { get; set; } = false;

        /// <summary>
        /// 图形的填充规则,当Fill属性存在时出现
        /// </summary>
        public FillRule Rule { get; set; } = FillRule.NonZero;

        /// <summary>
        /// 填充颜色,默认为透明色
        /// </summary>
        public CtColor FillColor { get; set; }

        /// <summary>
        /// 勾边颜色，默认黑色
        /// </summary>
        public CtColor StrokeColor { get; set; }

        /// <summary>
        /// 图形轮廓数据,由一系列紧缩的操作符和操作数构成
        /// </summary>
        public AbbreviatedData AbbreviatedData { get; set; }
    }
}
