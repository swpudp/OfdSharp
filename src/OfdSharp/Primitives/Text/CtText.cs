using System.Collections.Generic;
using OfdSharp.Primitives.Pages.Description;
using OfdSharp.Primitives.Pages.Description.Color;

namespace OfdSharp.Primitives.Text
{
    /// <summary>
    /// 文字对象
    /// 11.2 文字对象 图 59 表 45
    /// </summary>
    public class CtText : GraphicUnit
    {
        /// <summary>
        /// 标识
        /// </summary>
        public Id Id { get; set; }

        /// <summary>
        /// 引用资源文件中定义的字形标识
        /// </summary>
        public RefId Font { get; set; }

        /// <summary>
        /// 字号，单位为毫米，常用字体单位是pt（磅），转换：1pt=0.3527mm
        /// </summary>
        public double Size { get; set; }

        /// <summary>
        /// 是否勾边，默认值为 false
        /// </summary>
        public bool Stroke { get; set; }

        /// <summary>
        /// 是否填充，默认值为 true
        /// </summary>
        public bool Fill { get; set; }

        /// <summary>
        /// 字形在水平方向的缩放比，默认值为 1.0
        /// </summary>
        public double HorizontalScale { get; set; }

        /// <summary>
        /// 阅读方向，指定了文字排列的方向，描述见 11.3 文字定位，默认值为0
        /// </summary>
        public Direction ReadDirection { get; set; }

        /// <summary>
        /// 字符方向，指定了文字放置的方向，描述见 11.3 文字定位，默认值为0
        /// </summary>
        public Direction CharDirection { get; set; }

        /// <summary>
        /// 文字对象的粗细值，默认值为 400
        /// </summary>
        public Weight Weight { get; set; }

        /// <summary>
        /// 是否是斜体样式
        /// </summary>
        public bool Italic { get; set; }

        /// <summary>
        /// 填充颜色，默认黑色
        /// </summary>
        public CtColor FillColor { get; set; }

        /// <summary>
        /// 勾边颜色，默认透明色
        /// </summary>
        public CtColor StrokeColor { get; set; }

        /// <summary>
        /// 指定字符编码到字符索引之间的变换关系序列
        /// </summary>
        public List<CtTransform> Transforms { get; set; }

        /// <summary>
        /// 文字内容,也就是一段字符编码串，如果字符编码不在XML编码方式的字符范围之内,应采用“\”加四位十六进制数的格式转义;文字内容中出现的空格也需要转义，
        /// 若TextCode作为占位符使用时,一律采用“¤”(u00A4)占位
        /// </summary>
        public TextCode TextCode { get; set; }
    }
}