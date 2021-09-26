namespace OfdSharp.Primitives.Annotations
{
    /// <summary>
    /// 注释所在页
    /// </summary>
    public class RefPage
    {
        /// <summary>
        /// 引用注释所在页面的标识
        /// </summary>
        public RefId PageId { get; }

        /// <summary>
        /// 指向包内的分页注释文件
        /// </summary>
        public Location FileLoc { get; }
    }
}
