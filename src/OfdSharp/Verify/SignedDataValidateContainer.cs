using OfdSharp.Core.Signature;

namespace OfdSharp.Verify
{
    /// <summary>
    /// 签名数据验证容器
    /// </summary>
    internal abstract class SignedDataValidateContainer
    {
        /// <summary>
        /// 签名数据验证
        /// 如果验证不通过请抛出异常
        /// </summary>
        /// <param name="type">电子签名类型</param>
        /// <param name="tbsContent">待签章内容</param>
        /// <param name="signedValue">电子签章数据或签名值（SignedValue.xml文件内容）</param>
        public abstract VerifyResult Validate(SignedType type, byte[] tbsContent, byte[] signedValue);

    }
}
