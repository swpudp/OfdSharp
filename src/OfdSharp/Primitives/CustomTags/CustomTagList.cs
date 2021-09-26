using System.Collections.Generic;

namespace OfdSharp.Primitives.CustomTags
{
    /// <summary>
    /// 自定义标引集合，外部系统或用户可以添加自定义标记和信息，从而达到与其他系统、数据进行交互的目的并扩展应用。
    /// 一个文档可以带有多个自定义标引。
    /// 自定义标引列表的入口点在 7.5 文档根节点中定义。
    /// 标引索引文件，标引文件中通过ID引用于被引用标引对象发生“非接触式（分离式）”关联。标引内容可任意扩展，
    /// 但建议给出扩展内容的规范约束文件（schema）或命名空间。
    /// </summary>
    public class CustomTagList
    {
        /// <summary>
        /// 自定义标引入口列表
        /// </summary>
        public IList<CustomTag> CustomTags { get; set; }
    }
}
