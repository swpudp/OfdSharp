namespace OfdSharp.Core.Extensions
{
    /// <summary>
    /// 扩展信息
    /// “Name Type Value” 的数值组，用于简单的扩展
    /// </summary>
    public class Property
    {
        /// <summary>
        /// 扩展属性名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 扩展属性值类型
        /// </summary>
        public string Type { get; set; }
    }
}
