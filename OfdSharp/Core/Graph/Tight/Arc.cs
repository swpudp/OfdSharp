using OfdSharp.Core.BaseType;
using System.Xml;

namespace OfdSharp.Core.Graph.Tight
{
    /// <summary>
    /// 圆弧
    /// </summary>
    public class Arc : OfdElement
    {
        public Arc(XmlDocument xmlDocument, string name) : base(xmlDocument, name)
        {
        }

        public Arc(XmlDocument xmlDocument) : base(xmlDocument, "Arc")
        {
        }

        /// <summary>
        /// 弧线方向是否顺时针
        /// true 表示由圆弧起始点到结束点是顺时针，false 表示由圆弧起始点到结束点是逆时针
        /// 对于经过坐标系上指定两点，给定旋转角度和长短轴长度的椭圆，满足条件的可能有 2 个，
        /// 对应的圆弧有 4 条，通过 LargeArc 属性可以排除 2 条，次属性从剩余的 2 条圆弧中确定一条
        /// </summary>
        public bool SweepDirection { get; set; }

        /// <summary>
        /// 是否是大圆弧
        /// true 表示此线型对应的位角度大于 180°的弧，false 表示对应度数小于 180°的弧
        /// 对于一个给定长、短轴的椭圆以及起始点和结束点，有一大一小两条圆弧，
        /// 如果所描述线型恰好为 180°的弧，此属性的值不被参考，可由 SweepDirection 属性确定圆弧形状
        /// </summary>
        public bool LargeArc { get; set; }

        /// <summary>
        /// 按 EllipseSize 绘制的椭圆在当前坐标系下旋转的角度，
        /// 正值为顺时针，负值为逆时针
        /// [异常处理] 如果角度大于 360°，则以 360°取模
        /// </summary>
        public double RotationAngle { get; set; }

        /// <summary>
        /// 长短轴
        /// 形如[200 100]的数组，2个浮点数值一次对应椭圆的长、短轴长度，较大的一个为长轴
        /// [异常处理]如果数组长度超过 2，则只取前两个数值
        /// [异常处理]如果数组长度为 1，则认为这是一个园，该数值为圆的半径
        /// [异常处理]如果数组前两个数值中有一个为 0，或者数组为空，则圆弧退化为一条从当前点到EndPoint的线段
        /// </summary>
        public StArray EllipseSize { get; set; }

        /// <summary>
        /// 圆弧结束点，下一个路径起点
        /// 不能与当前的绘制点为同一位置
        /// </summary>
        public StPosition EndPoint { get; set; }
    }
}
