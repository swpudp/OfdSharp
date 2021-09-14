namespace OfdSharp.Primitives
{
    /// <summary>
    /// 包结构内文件的路径，.表示当前路径，..表示父路径
    /// 约定：
    /// 1."/" 表示根路径
    /// 2.未显式指定时代表当前路径
    /// 3.路径区分大小写
    /// </summary>
    public class Location
    {
        /// <summary>
        /// 路径
        /// </summary>
        public string Value { get; set; }
    }
}
