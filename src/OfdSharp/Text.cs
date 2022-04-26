namespace OfdSharp
{
    public class Text
    {
        internal Text(string content)
        {
            Content = content;
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// 下划线
        /// </summary>
        public bool Underline { get; set; }

        /// <summary>
        /// 加粗
        /// </summary>
        public bool Bold { get; set; }

        /// <summary>
        /// 斜体
        /// </summary>
        public bool Italic { get; set; }

        /// <summary>
        /// 字间距
        /// </summary>
        public float LetterSpacing { get; set; }

        /// <summary>
        /// 字体
        /// </summary>
        public OfdFont Font { get; set; }

        /// <summary>
        /// 字体大小，单位pt
        /// </summary>
        public float FontSize { get; set; }

        /// <summary>
        /// 是否填充
        /// </summary>
        public bool Fill { get; set; } = true;

        /// <summary>
        /// 字体颜色，默认黑色
        /// </summary>
        public OfdColor Color { get; set; } = OfdColor.Black;

        /// <summary>
        /// 是否描边
        /// </summary>
        public bool Stroke { get; set; }

        /// <summary>
        /// 描边颜色
        /// </summary>
        public OfdColor StrokeColor { get; set; } = OfdColor.Black;
    }
}