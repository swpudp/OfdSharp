using System.ComponentModel;

namespace OfdSharp.Verify
{
    /// <summary>
    /// 验证结果
    /// </summary>
    [Description("验证结果")]
    public enum VerifyResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success,

        /// <summary>
        /// 文件数据被篡改
        /// </summary>
        [Description("文件数据被篡改")]
        FileTampered,

        /// <summary>
        /// 印章数据被篡改
        /// </summary>
        [Description("印章数据被篡改")]
        SealTampered,

        /// <summary>
        /// 印章不匹配
        /// </summary>
        [Description("印章不匹配")]
        SealNotMatch,

        /// <summary>
        /// 印章不存在
        /// </summary>
        [Description("印章不存在")]
        SealNotFound,

        /// <summary>
        /// 印章已过期
        /// </summary>
        [Description("印章已过期")]
        SealOutdated,

        /// <summary>
        /// 签章数据被篡改
        /// </summary>
        [Description("签章数据被篡改")]
        SignedTampered,

        /// <summary>
        /// 签章数据不匹配
        /// </summary>
        [Description("签章数据不匹配")]
        SignedNotMatch
    }
}
