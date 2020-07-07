namespace OfdSharp.Reader
{
    /// <summary>
    /// ofd文件概要信息
    /// </summary>
    public class DocSummary
    {
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; } = "1.0";

        /// <summary>
        /// 文档类型
        /// </summary>
        public string DocType { get; set; }

        /// <summary>
        /// 文入口文件
        /// </summary>
        public string DocRoot { get; set; }

        /// <summary>
        /// 签名文件
        /// </summary>
        public string Signatures { get; set; }
    }
}
