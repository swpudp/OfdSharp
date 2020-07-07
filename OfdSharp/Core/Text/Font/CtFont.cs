using System.Xml;

namespace OfdSharp.Core.Text.Font
{
    /// <summary>
    /// 字形
    ///
    /// 11.1 字形 图 58 表 44
    /// </summary>
    public class CtFont : OfdElement
    {
        public CtFont(XmlDocument xmlDocument) : base(xmlDocument, "Font")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 字形名
        /// </summary>
        public string FontName { get; set; }

        /// <summary>
        /// 字形族名
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// 字形适用的字符分类
        /// </summary>
        public Charset Charset { get; set; }

        /// <summary>
        /// 是否是斜体
        /// 用于匹配替代字形
        /// 默认值是 false
        /// </summary>
        public bool Italic { get; set; }

        /// <summary>
        /// 是否是粗字体
        /// 用于匹配替代字形
        /// 默认值是 false
        /// </summary>
        public bool Bold { get; set; }

        /// <summary>
        /// 是否是带衬线字形
        /// 用于匹配替代字形
        /// 默认值是 false
        /// </summary>
        public bool Serif { get; set; }

        /// <summary>
        /// 是否是等宽字形
        /// 用于匹配替代字形
        /// 默认值是 false
        /// </summary>
        public bool FixedWidth { get; set; }

        /// <summary>
        /// 指向内嵌字形文件
        /// 嵌入字形文件应使用 OpenType 格式
        /// </summary>
        public string FontFile { get; set; }
    }
}
