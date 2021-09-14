namespace OfdSharp.Core.Signature
{
    /// <summary>
    /// 数字签名或安全签章在类表中的注册信息，依次签名或签章对应一个节点
    /// </summary>
    public class SignatureInfo
    {
        /// <summary>
        /// 签名或签章的标识
        /// 推荐使用“sNNN”的编码方式，NNN从1开始
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 签名节点的类型
        /// </summary>
        public SignedType Type { get; set; }

        /// <summary>
        /// 指向包内的签名描述文件
        /// </summary>
        public string BaseLoc { get; set; }
    }
}
