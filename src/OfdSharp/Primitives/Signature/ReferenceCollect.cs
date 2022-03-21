using System.Collections.Generic;
using OfdSharp.Primitives.Signatures;

namespace OfdSharp.Primitives.Signature
{
    /// <summary>
    /// 签名的范围
    /// </summary>
    public class ReferenceCollect
    {
        /// <summary>
        /// 摘要方法
        /// 视应用场景的不同使用不同的摘要方法。
        /// 用于各行业应用时，应使用符合行业安全规范的算法。
        /// </summary>
        public string CheckMethod { get; set; }

        /// <summary>
        /// 针对一个文件的摘要节点列表
        /// </summary>
        public List<Reference> Items { get; set; }
    }
}
