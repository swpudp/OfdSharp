using System.Xml.Serialization;
using OfdSharp.Core.Signature;

namespace OfdSharp.Core.Signatures
{
    /// <summary>
    /// 针对一个文件的摘要节点
    /// </summary>
    public class Reference
    {
        /// <summary>
        /// 指向包内的文件，使用绝对路径
        /// </summary>
        [XmlAttribute]
        public string FileRef { get; set; }

        /// <summary>
        /// 对包内文件进行摘要计算值的杂凑值base64 编码
        /// </summary>
        [XmlElement]
        public CheckValue CheckValue { get; set; }
    }
}
