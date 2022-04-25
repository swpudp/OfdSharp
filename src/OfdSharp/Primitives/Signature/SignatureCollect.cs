using System.Collections.Generic;

namespace OfdSharp.Primitives.Signature
{
    /// <summary>
    /// 签名列表根节点，签名列表文件的入口点在7.4主入口中定义。
    /// 签名列表文件中可以包含多个签名（例如联合发文等情况），见图 85。
    /// 当允许下次继续添加签名时，该文件不会被包含到本次签名的保护文件列表（References）中。
    /// </summary>
    public class SignatureCollect
    {
        /// <summary>
        /// 安全标识的最大值。
        /// 作用与文档入口文件 Document.xml 中的 MaxID相同，为了避免在签名时影响文档入口文件，采用了与ST_ID不一样的ID编码方式，
        /// 推荐使用“sNNN”的编码方式，NNN从1开始
        /// </summary>
        public string MaxSignId { get; set; }

        /// <summary>
        /// 数字签名或安全签章在类表中的注册信息
        /// </summary>
        public List<SignatureInfo> Signatures { get; set; }
    }
}