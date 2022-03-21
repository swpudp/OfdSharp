using System;
using OfdSharp.Crypto;
using OfdSharp.Ses.V1;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.X509;

namespace OfdSharp.Sign
{
    /// <summary>
    /// 《GM/T 0031-2014 安全电子签章密码技术规范》 电子签章数据生成扩展容器
    /// </summary>
    public class SesV1Signer
    {
        /// <summary>
        /// 签名使用的私钥
        /// </summary>
        private readonly string _privateKey;

        /// <summary>
        /// 电子印章
        /// </summary>
        private readonly SesSealInfo _seal;

        /// <summary>
        /// 签章使用的证书
        /// </summary>
        private readonly X509Certificate _certificate;

        public SesV1Signer(string privateKey, SesSealInfo seal, X509Certificate signCert)
        {
            _privateKey = privateKey;
            _seal = seal;
            _certificate = signCert;
        }

        /// <summary>
        /// 对待签名数据签名
        /// </summary>
        /// <param name="input">在操作过程中请勿对流进行关闭</param>
        /// <param name="propertyInfo">签章属性信息</param>
        /// <returns></returns>
        public byte[] Sign(byte[] input, string propertyInfo)
        {
            //原文杂凑值计算
            byte[] outBytes = Sm2Utils.Digest(input);
            DerUtcTime signTime = new DerUtcTime(DateTime.Now);
            TbsSign tbsSign = new TbsSign
            {
                Version = new DerInteger(ConstDefined.SesV1),
                EsSeal = _seal,
                TimeInfo = new DerBitString(signTime),
                DataHash = new DerBitString(outBytes),
                PropertyInfo = new DerIA5String(propertyInfo),
                Cert = new DerOctetString(_certificate.GetEncoded()),
                SignatureAlgorithm = GMObjectIdentifiers.sm2sign_with_sm3
            };
            byte[] signBytes = tbsSign.GetDerEncoded();
            byte[] sign = Sm2Utils.Sign(_privateKey, signBytes);
            SesSignature sesSignature = new SesSignature(tbsSign, new DerBitString(sign));
            return sesSignature.GetDerEncoded();
        }
    }
}