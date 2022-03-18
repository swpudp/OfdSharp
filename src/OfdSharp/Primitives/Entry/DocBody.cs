namespace OfdSharp.Primitives.Entry
{
    /// <summary>
    /// 文档正文
    /// </summary>
    public class DocBody
    {
        /// <summary>
        /// 文档信息
        /// </summary>
        public DocInfo DocInfo { get; set; }

        /// <summary>
        /// 文档入口文件
        /// </summary>
        public Location DocRoot { get; set; }

        /// <summary>
        /// 签名入口
        /// </summary>
        public Location Signatures { get; set; }
    }
}
