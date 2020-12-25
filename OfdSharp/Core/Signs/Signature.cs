using System.Xml.Serialization;

namespace OfdSharp.Core.Signs
{
    /// <summary>
    /// 数字签名或安全签章在类表中的注册信息，依次签名或签章对应一个节点
    /// </summary>
    [XmlRoot(Namespace = "http://www.ofdspec.org/2016")]
    public class Signature
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
