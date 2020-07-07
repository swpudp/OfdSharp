using System.Collections.Generic;
using System.Xml;

namespace OfdSharp.Core.Signatures
{
    /// <summary>
    /// 签名的范围
    /// </summary>
    public class ReferenceCollect : OfdElement
    {
        public ReferenceCollect(XmlDocument xmlDocument) : base(xmlDocument, "References")
        {
        }

        /// <summary>
        /// 摘要方法
        /// 视应用场景的不同使用不同的摘要方法。
        /// 用于各行业应用时，应使用符合行业安全规范的算法。
        /// </summary>
        public CheckMethod CheckMethod { get; set; }

        /// <summary>
        /// 针对一个文件的摘要节点列表
        /// </summary>
        public IList<Reference> References { get; set; }
    }
}
