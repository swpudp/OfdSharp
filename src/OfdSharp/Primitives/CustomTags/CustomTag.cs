namespace OfdSharp.Primitives.CustomTags
{
    /// <summary>
    /// 自定义标引入口
    /// </summary>
    public class CustomTag
    {
        /// <summary>
        /// 自定义标引内容节点使用的类型标识
        /// </summary>
        public string TypeId { get; set; }

        /// <summary>
        /// 指向自定义标引内容节点适用的Schema文件
        /// </summary>
        public CtLocation SchemaLoc { get; set; }

        /// <summary>
        /// 指向自定义标引文件
        /// </summary>
        public CtLocation FileLoc { get; set; }
    }
}
