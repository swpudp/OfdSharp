namespace OfdSharp.Primitives.Version
{
    /// <summary>
    /// 版本描述入口
    /// </summary>
    public class Version 
    {
        /// <summary>
        /// 版本标识
        /// </summary>
        public CtId Id { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 是否是默认版本，默认为false
        /// </summary>
        public bool Current { get; set; }

        /// <summary>
        /// 指向包内的版本描述文件
        /// </summary>
        public CtLocation BaseLoc { get; set; }
    }
}
