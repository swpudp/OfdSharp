using System.Xml;

namespace OfdSharp.Core.PageDescription.DrawParam
{
    public class CtDrawParam : OfdElement
    {
        public CtDrawParam(XmlDocument xmlDocument) : base(xmlDocument, "DrawParam")
        {
        }

        /// <summary>
        /// 基础绘制参数，引用资源文件中的绘制参数的标识符
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 基础绘制参数，引用资源文件中的绘制参数的标识符
        /// </summary>
        public string Relative { get; set; }

        /// <summary>
        /// 线宽
        /// 非负浮点数，指定了绘制路径绘制时线的宽度。由于某些设备不能输出一个像素宽度的线，因此强制规定:
        /// 当线宽大于 0 时，无论多小都至少要绘制两个像素的宽度；
        /// 当线宽为 0 时，绘制一个像素的宽度。由于线宽为 0 定义与
        /// 设备相关，所以不推荐使用线宽为 0。
        /// </summary>
        public double LineWidth { get; set; }

        /// <summary>
        /// 线条连接样式
        /// </summary>
        public LineJoinType Join { get; set; }

        /// <summary>
        /// 线端点样式
        /// </summary>
        public LineCapType Cap { get; set; }
    }
}
