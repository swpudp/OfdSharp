using OfdSharp.Primitives.Pages.Description.Color;

namespace OfdSharp.Primitives.Pages.Description.DrawParam
{
    /// <summary>
    /// 绘制参数是一组用于控制绘制渲染效果的修饰参数的集合。绘制参数可以被不同的图元对象所共享。
    /// 图元对象通过绘制参数的标识引用绘制参数。图元对象在引用绘制参数的同时,还可以定义自己的绘制属性,图元自有的绘制属性将覆盖其引用的绘制参数中的同名属性。
    /// 绘制参数可通过引用基础绘制参数的方式形成嵌套,对单个绘制参数而言,它继承了其基础绘制参数中的所有属性,并且可以重定义其基础绘制参数中的属性。
    /// </summary>
    public class CtDrawParam
    {
        /// <summary>
        /// 标识
        /// </summary>
        public Id Id { get; set; }

        /// <summary>
        /// 基础绘制参数，引用资源文件中的绘制参数的标识符
        /// </summary>
        public RefId Relative { get; set; }

        /// <summary>
        /// 线条连接样式
        /// </summary>
        public LineJoinType Join { get; set; }

        /// <summary>
        /// 线宽
        /// 非负浮点数，指定了绘制路径绘制时线的宽度。由于某些设备不能输出一个像素宽度的线，因此强制规定:
        /// 当线宽大于 0 时，无论多小都至少要绘制两个像素的宽度；
        /// 当线宽为 0 时，绘制一个像素的宽度。由于线宽为 0 定义与
        /// 设备相关，所以不推荐使用线宽为 0。
        /// </summary>
        public double LineWidth { get; set; }

        /// <summary>
        /// 线条虚线样式开始的位置,默认值为0。当DashPattern不出现时,该参数无效
        /// </summary>
        public double DashOffsetxs { get; set; }

        /// <summary>
        /// 线条虚线的重复样式,数组中共含两个值,第一个值代表虚线线段的长度,第二个值代表虚线间隔的长度。
        /// 默认值为空。
        /// 线条虚线样式的控制效果见表23
        /// </summary>
        public Array DashPattern { get; set; }

        /// <summary>
        /// 线端点样式,枚举值,指定了一条线的端点样式
        /// </summary>
        public LineCapType Cap { get; set; }

        /// <summary>
        /// Join为Miter时小角度结合点长度的截断值,默认值为3.528。
        /// 当Join不等于Miter时该参数无效
        /// </summary>
        public double MiterLimitxs { get; set; }

        /// <summary>
        /// 填充颜色,用以填充路径形成的区域以及文字轮廓内的区域,默认值为透明色。
        /// </summary>
        public CtColor FillColor { get; set; }

        /// <summary>
        /// 勾边颜色,指定路径绘制的颜色以及文字轮廓的颜色,默认值为黑色。
        /// </summary>
        public CtColor StrokeColor { get; set; }
    }
}
