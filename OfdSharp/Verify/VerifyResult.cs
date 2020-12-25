namespace OfdSharp.Verify
{
    /// <summary>
    /// 验证结果
    /// </summary>
    public enum VerifyResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success,

        /// <summary>
        /// 文件数据被篡改
        /// </summary>
        FileTampered,

        /// <summary>
        /// 印章数据被篡改
        /// </summary>
        SealTampered,

        /// <summary>
        /// 签章数据被篡改
        /// </summary>
        SignedTampered,

        /// <summary>
        /// 印章不匹配
        /// </summary>
        SealNotMatch,

        /// <summary>
        /// 电子签章不匹配
        /// </summary>
        SignedNotMatch
    }
}
