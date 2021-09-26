namespace OfdSharp.Primitives.Text
{
    /// <summary>
    /// 变换描述
    ///
    /// 当存在字形变换时，TextCode对象中使用字形变换节点（CGTransform）描述字符编码
    /// 和字形索引之间的关系
    /// </summary>
    public class CtTransform
    {
        /// <summary>
        /// TextCode 中字符编码的起始位置
        /// </summary>
        public int CodePosition { get; set; }

        /// <summary>
        /// 变换关系中字符的数量
        /// 该数值应大于等于 1，否则属于错误描述，默认1
        /// </summary>
        public int CodeCount { get; set; } = 1;

        /// <summary>
        /// 变换关系中字形索引的个数
        /// 该数值应大于等于 1，否则属于错误描述，默认1
        /// </summary>
        public int GlyphCount { get; set; } = 1;

        /// <summary>
        /// 变换后的字形索引列表
        /// </summary>
        public Array Glyphs { get; set; }
    }
}
