using System.IO;
using OfdSharp.Core.Signatures;
using OfdSharp.Extensions;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Security;

namespace OfdSharp.Sign.SignContainer
{
    /// <summary>
    /// 国密SM2withSM3数字签名实现容器
    /// </summary>
    public class DigitalSignContainer : ExtendSignatureContainer
    {
        /// <summary>
        /// 提供文件的摘要算法功能
        /// </summary>
        /// <returns></returns>
        public override GeneralDigest GetDigest()
        {
            return new SM3Digest();
        }

        /// <summary>
        /// 签名方法OID
        /// </summary>
        /// <returns></returns>
        public override DerObjectIdentifier GetSignAlgOId()
        {
            return GMObjectIdentifiers.sm2sign_with_sm3;
        }

        /// <summary>
        /// 对待签名数据签名
        /// </summary>
        /// <param name="inData">在操作过程中请勿对流进行关闭</param>
        /// <param name="propertyInfo">签章属性信息</param>
        /// <returns></returns>
        public override byte[] Sign(Stream inData, string propertyInfo)
        {
            ISigner signer = SignerUtilities.GetSigner(GMObjectIdentifiers.sm2sign_with_sm3);
            signer.Init(false, null);
            byte[] signBytes = inData.ToArray();
            signer.BlockUpdate(signBytes, 0, signBytes.Length);
            byte[] sign = signer.GenerateSignature();

            //Org.BouncyCastle.Asn1.Ocsp.Signature signatureFnc = Org.BouncyCastle.Asn1.Ocsp.Signature.GetInstance(GMObjectIdentifiers.sm2sign_with_sm3.ToString());
            //signatureFnc.initSign(prvKey);
            //signatureFnc.update(IOUtils.toByteArray(inData));
            //return signatureFnc.SignatureValue;
            //signatureFnc.GetSignatureOctets();
            //throw new NotImplementedException();

            return sign;
        }

        /// <summary>
        /// 获取电子印章二进制编码
        /// </summary>
        /// <returns></returns>
        public override byte[] GetSeal()
        {
            return null;
        }

        /// <summary>
        /// 获取签名节点类型
        /// </summary>
        public override SigType SignType => SigType.Sign;
    }
}
