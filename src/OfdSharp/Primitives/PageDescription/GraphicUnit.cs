using OfdSharp.Primitives.Action;
using OfdSharp.Primitives.PageDescription.Clips;
using OfdSharp.Primitives.PageDescription.DrawParam;
using System.Collections.Generic;

namespace OfdSharp.Primitives.PageDescription
{
    /// <summary>
    /// 图元对象：版式文档中页面上呈现内容的最基本单元,所有页面显示内容,包括文字、图形、图像等,都属于图元对象,或是图元对象的组合。
    /// </summary>
    public class GraphicUnit
    {
        /// <summary>
        /// 外接矩形,采用当前空间坐标系(页面坐标或其他容器坐标),当图元绘制超出此矩形区域时进行裁剪
        /// </summary>
        public Box Boundary { get; set; }

        /// <summary>
        /// 图元对象的名字，默认值为空
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图元是否可见，默认true
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// 对象空间内的图元变换矩阵
        /// </summary>
        public Array TransformMatrixs { get; set; }

        /// <summary>
        /// 引用资源文件中的绘制参数标识
        /// </summary>
        public RefId DrawParam { get; set; }

        /// <summary>
        /// 绘制路径时使用的线宽。
        /// 如果图元对象有DrawParam属性,则用此值覆盖DrawParam中对应的值
        /// </summary>
        public double LineWidth { get; set; }

        /// <summary>
        /// 线端点样式。如果图元对象有DrawParam属性,则用此值覆盖DrawParam中对应的值
        /// </summary>
        public LineCapType Cap { get; set; }

        /// <summary>
        /// 线条连接样式。如果图元对象有DrawParam属性,则用此值覆盖DrawParam中对应的值
        /// </summary>
        public LineJoinType Join { get; set; }

        /// <summary>
        /// Join为Miter时,MiterSize的截断值。如果图元对象有DrawParam属性,则用此值覆盖DrawParam中对应的值
        /// </summary>
        public double MiterLimit { get; set; }

        /// <summary>
        /// 线条虚线样式开始的位置,默认值为0。如果图元对象有DrawParam属性,则用此值覆盖DrawParam中对应的值
        /// </summary>
        public double DashOffset { get; set; }

        /// <summary>
        /// 线条虚线的重复样式,数组中共含两个值,第一个值代表虚线线段的长度,第二个值代表虚线间隔的长度。
        /// </summary>
        public Array DashPattern { get; set; }

        /// <summary>
        /// 图元对象的透明度,取值区间为[0,255]，0表示全透明,255表示完全不透明，默认为0
        /// </summary>
        public int Alpha { get; set; }

        /// <summary>
        /// 图元动作
        /// </summary>
        public List<CtAction> Actions { get; set; }

        /// <summary>
        /// 图元对象的裁剪区域序列,采用对象空间坐标系
        /// </summary>
        public List<Clip> Clips { get; set; }
    }
}
