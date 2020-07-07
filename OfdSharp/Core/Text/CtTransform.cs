using System.Collections.Generic;
using System.Xml;

namespace OfdSharp.Core.Text
{
    /// <summary>
    /// 变换描述
    ///
    /// 当存在字形变换时，TextCode对象中使用字形变换节点（CGTransform）描述字符编码
    /// 和字形索引之间的关系
    /// </summary>
    public class CtTransform : OfdElement
    {
        public CtTransform(XmlDocument xmlDocument) : base(xmlDocument, "CGTransfrom")
        {
        }

        /// <summary>
        /// TextCode 中字符编码的起始位置
        /// </summary>
        public int CodePosition { get; set; }

        /// <summary>
        /// 变换关系中字符的数量
        /// 该数值应大于等于 1，否则属于错误描述
        /// </summary>
        public int CodeCount { get; set; }

        /// <summary>
        /// 变换关系中字形索引的个数
        /// 该数值应大于等于 1，否则属于错误描述
        /// </summary>
        public int GlyphCount { get; set; }

        /// <summary>
        /// 变换后的字形索引列表
        /// </summary>
        public int Glyphs { get; set; }

        /// <summary>
        /// 文字内容
        /// 也就是一段字符编码串
        /// 如果字符编码不在XML编码方式的字符范围之内，应采用“\”加四位
        /// 十六进制数的格式转义；文字内容中出现的空格也需要转义
        /// 若 TextCode 作为占位符使用时一律采用  ¤ （\u00A4）占位
        /// </summary>
        public IList<TextCode> TextCodes { get; set; }
    }
}
