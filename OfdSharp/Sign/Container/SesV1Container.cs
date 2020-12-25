using System;
using System.IO;
using OfdSharp.Core.Signs;
using OfdSharp.Extensions;
using OfdSharp.Ses.V1;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace OfdSharp.Sign.Container
{
    /// <summary>
    /// 《GM/T 0031-2014 安全电子签章密码技术规范》 电子签章数据生成扩展容器
    /// 注意：该容器仅用于测试，电子签章请使用符合国家规范具有国家型号证书的设备进行！
    /// </summary>
    public class SesV1Container : ExtendSignatureContainer
    {
        /// <summary>
        /// 签名使用的私钥
        /// </summary>
        private readonly AsymmetricKeyParameter _privateKey;

        /// <summary>
        /// 电子印章
        /// </summary>
        private readonly SesSealInfo _seal;

        /// <summary>
        /// 签章使用的证书
        /// </summary>
        private readonly X509Certificate _certificate;

        public SesV1Container(AsymmetricKeyParameter privateKey, SesSealInfo seal, X509Certificate signCert)
        {
            _privateKey = privateKey;
            _seal = seal;
            _certificate = signCert;
        }

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
            //原文杂凑值计算
            GeneralDigest digest = GetDigest();
            byte[] block = inData.ToArray();
            digest.BlockUpdate(block, 0, block.Length);
            byte[] outBytes = new byte[32];
            digest.DoFinal(outBytes, 0);

            //计算杂凑值
            DerUtcTime signTime = new DerUtcTime(DateTime.Now);
            TbsSign tbsSign = new TbsSign
            {
                Version = new DerInteger(1),
                EsSeal = _seal,
                TimeInfo = new DerBitString(signTime),
                DataHash = new DerBitString(outBytes),
                PropertyInfo = new DerIA5String(propertyInfo),
                Cert = new DerOctetString(_certificate.GetEncoded()),
                SignatureAlgorithm = GetSignAlgOId()
            };
            ISigner signer = SignerUtilities.GetSigner(GMObjectIdentifiers.sm2sign_with_sm3);
            signer.Init(true, _privateKey);
            byte[] signBytes = tbsSign.GetDerEncoded();
            signer.BlockUpdate(signBytes, 0, signBytes.Length);
            byte[] sign = signer.GenerateSignature();
            SesSignature sesSignature = new SesSignature(tbsSign, new DerBitString(sign));
            return sesSignature.GetDerEncoded();
        }

        /// <summary>
        /// 获取电子印章二进制编码
        /// </summary>
        /// <returns></returns>
        public override byte[] GetSeal()
        {
            return _seal.GetDerEncoded();
        }

        /// <summary>
        /// 获取签名节点类型
        /// </summary>
        public override SigType SignType => SigType.Sign;
    }
}
