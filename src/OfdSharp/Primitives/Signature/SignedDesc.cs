namespace OfdSharp.Primitives.Signature
{
    /// <summary>
    /// 签名描述文件的根节点，OFD的数字签名通过对描述文件的保护间接实现对OFD原文的保护。
    /// 签名结构中的签名信息（SignedInfo）是这一过程中的关键点，
    /// 其中记录了当次数字签名保护的所有文件的二进制摘要信息，同时
    /// 将安全算法提供者、签名算法、签名时间、和所应用的安全印章等
    /// 信息也包含在此节点内。签名描述文件同时包含了签名值将要存放的
    /// 包内位置，一旦对该文件实施签名保护，则其对应的包内文件原文
    /// 以及本次签名对应的附加信息都将不可改动，从而实现依次数字签名
    /// 对整个原文内容的保护。签名描述文件的主要结构描述见图 86。
    ///
    /// 文件摘要文件根节点为 Signature，其子节点 SignedInfo 对应元素说明见表 67。
    /// </summary>
    public class SignedDesc 
    {
        /// <summary>
        /// 签名要保护的原文及本次签名的相关信息
        /// </summary>
        public SignedInfo SignedInfo { get; set; }

        /// <summary>
        /// 指向安全签名提供者所返还的针对签名描述文件计算所得的签名值文件
        /// </summary>
        public string SignedValue { get; set; }
    }
}
