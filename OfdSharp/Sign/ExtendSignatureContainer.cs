using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto.Digests;
using System.IO;
using OfdSharp.Core.Signature;

namespace OfdSharp.Sign
{
    /// <summary>
    /// 扩展数字签名容器
    /// </summary>
    public abstract class ExtendSignatureContainer
    {
        /// <summary>
        /// 提供文件的摘要算法功能
        /// </summary>
        /// <returns></returns>
        public abstract GeneralDigest GetDigest();

        /// <summary>
        /// 签名方法OID
        /// </summary>
        /// <returns></returns>
        public abstract DerObjectIdentifier GetSignAlgOId();

        /// <summary>
        /// 对待签名数据签名
        /// </summary>
        /// <param name="inData">在操作过程中请勿对流进行关闭</param>
        /// <param name="propertyInfo">签章属性信息</param>
        /// <returns></returns>
        public abstract byte[] Sign(Stream inData, string propertyInfo);

        /// <summary>
        /// 获取电子印章二进制编码
        /// </summary>
        /// <returns></returns>
        public abstract byte[] GetSeal();

        /// <summary>
        /// 获取签名节点类型
        /// </summary>
        public abstract SignedType SignType { get; }
    }
}
