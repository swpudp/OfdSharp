using System.Xml;

namespace OfdSharp.Core.Signatures
{
    /// <summary>
    /// 针对一个文件的摘要节点
    /// </summary>
    public class Reference : OfdElement
    {
        public Reference(XmlDocument xmlDocument) : base(xmlDocument, "Reference")
        {
        }

        /// <summary>
        /// 指向包内的文件，使用绝对路径
        /// </summary>
        public string FileRef { get; set; }

        /// <summary>
        /// 对包内文件进行摘要计算值的杂凑值
        /// 所得的二进制摘要值进行 base64 编码
        /// </summary>
        public byte[] CheckValue { get; set; }
    }
}
