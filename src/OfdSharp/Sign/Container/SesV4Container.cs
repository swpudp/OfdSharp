using System;
using System.IO;
using OfdSharp.Core.Signature;
using OfdSharp.Extensions;
using OfdSharp.Ses;
using OfdSharp.Ses.V4;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace OfdSharp.Sign.Container
{
    /// <summary>
    /// 《GB/T 38540-2020 信息安全技术 安全电子签章密码技术规范》 电子签章数据生成扩展容器
    /// 注意：该容器仅用于测试，电子签章请使用符合国家规范具有国家型号证书的设备进行！
    /// </summary>
    public class SesV4Container : ExtendSignatureContainer
    {
        /// <summary>
        /// 签名使用的私钥
        /// </summary>
        private readonly AsymmetricKeyParameter _privateKey;

        /// <summary>
        /// 电子印章
        /// </summary>
        private readonly SeSeal _seal;

        /// <summary>
        /// 签章使用的证书
        /// </summary>
        private readonly X509Certificate _certificate;

        public SesV4Container(AsymmetricKeyParameter privateKey, SeSeal seal, X509Certificate signCert)
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
            GeneralDigest md = GetDigest();
            byte[] input = inData.ToArray();
            md.BlockUpdate(input, 0, input.Length);
            byte[] output = new byte[32];
            md.DoFinal(output, 0);

            TbsSign tbsSign = new TbsSign
            {
                Version = SesHeader.V4,
                EsSeal = _seal,
                TimeInfo = new DerGeneralizedTime(DateTime.Now),
                DataHash = new DerBitString(output),
                PropertyInfo = new DerIA5String(propertyInfo)
            };
            ISigner signer = SignerUtilities.GetSigner(GMObjectIdentifiers.sm2sign_with_sm3);
            signer.Init(true, _privateKey);
            byte[] toSign = tbsSign.GetDerEncoded();
            signer.BlockUpdate(toSign, 0, toSign.Length);
            byte[] signed = signer.GenerateSignature();

            SesSignature sesSignature = new SesSignature(tbsSign, new DerOctetString(_certificate.GetEncoded()), GMObjectIdentifiers.sm2sign_with_sm3, new DerBitString(signed));
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
        public override SignedType SignType => SignedType.Seal;

    }
}
