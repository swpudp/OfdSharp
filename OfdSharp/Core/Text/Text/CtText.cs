using OfdSharp.Core.PageDescription.Color;
using System.Collections.Generic;
using System.Xml;

namespace OfdSharp.Core.Text.Text
{
    /// <summary>
    /// 文字对象
    /// 11.2 文字对象 图 59 表 45
    /// </summary>
    public class CtText : OfdElement
    {
        public CtText(XmlDocument xmlDocument) : base(xmlDocument, "TextObject")
        {
        }

        /// <summary>
        /// 文字对象标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 引用资源文件中定义的字形标识
        /// </summary>
        public string Font { get; set; }

        /// <summary>
        /// 字号，单位为毫米
        /// </summary>
        public double Size { get; set; }

        /// <summary>
        /// 是否勾边
        /// 默认值为 false
        /// </summary>
        public bool Stroke { get; set; }

        /// <summary>
        /// 是否填充
        /// 默认值为 true
        /// </summary>
        public bool Fill { get; set; }

        /// <summary>
        /// 字形在水平方向的缩放比
        /// 默认值为 1.0
        /// </summary>
        public double HScale { get; set; }

        /// <summary>
        /// 阅读方向
        /// 指定了文字排列的方向，描述见 11.3 文字定位
        /// </summary>
        public Direction ReadDirection { get; set; }

        /// <summary>
        /// 字符方向
        /// 指定了文字放置的方向，描述见 11.3 文字定位
        /// </summary>
        public Direction CharDirection { get; set; }

        /// <summary>
        /// 文字对象的粗细值
        /// 默认值为 400
        /// </summary>
        public Weight Weight { get; set; }

        /// <summary>
        /// 是否是斜体样式
        /// </summary>
        public bool Italic { get; set; }

        /// <summary>
        /// 填充颜色
        /// </summary>
        public CtColor FillColor { get; set; }

        /// <summary>
        /// 勾边颜色
        /// </summary>
        public CtColor StrokeColor { get; set; }

        /// <summary>
        /// 指定字符编码到字符索引之间的变换关系序列
        /// </summary>
        public IList<CtTransform> Transforms { get; set; }


    }
}
