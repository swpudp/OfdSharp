using System.Xml.Serialization;

namespace OfdSharp.Core.Signature
{
    /// <summary>
    /// 文件签名摘要信息
    /// </summary>
    public class DigestInfo
    {
        /// <summary>
        /// 签名信息
        /// </summary>
        [XmlElement]
        public SignedInfo SignedInfo { get; set; }

        /// <summary>
        /// 签名值
        /// </summary>
        [XmlElement]
        public string SignedValue { get; set; }
    }
}
