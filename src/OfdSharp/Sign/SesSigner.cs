using System;
using System.Collections.Generic;
using System.Text;
using OfdSharp.Ses;
using OfdSharp.Ses.V4;
using OpenSsl.Crypto.Utility;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.GM;

namespace OfdSharp.Sign
{
    /// <summary>
    /// 《GB/T 38540-2020 信息安全技术 安全电子签章密码技术规范》 电子签章数据生成扩展容器
    /// </summary>
    public class SesSigner
    {
        // <summary>
        // 签名使用的私钥
        // </summary>
        //private readonly string _privateKey;

        // <summary>
        // 电子印章
        // </summary>
        //private SeSeal _seal;

        /// <summary>
        /// 签章使用的证书
        /// </summary>
        //public readonly X509Certificate _certificate;
        private readonly SesSealConfig _sealConfig;

        /// <summary>
        /// 签名算法标识
        /// </summary>
        public static DerObjectIdentifier SignatureMethod => GMObjectIdentifiers.sm2sign_with_sm3;

        /// <summary>
        /// 摘要算法标识
        /// </summary>
        public static DerObjectIdentifier DigestMethod => GMObjectIdentifiers.sm3;

        public SesSigner(SesSealConfig sealConfig)
        {
            _sealConfig = sealConfig;
        }

        /// <summary>
        /// 对待签名数据签名
        /// </summary>
        /// <param name="input">在操作过程中请勿对流进行关闭</param>
        /// <param name="propertyInfo">签章属性信息</param>
        /// <returns></returns>
        public byte[] Sign(byte[] input, string propertyInfo)
        {
            SeSeal seal = CreateSeal();
            byte[] output = DigestUtils.Sm3(input);
            TbsSign tbsSign = new TbsSign
            {
                Version = new DerInteger(ConstDefined.SesV4),
                EsSeal = seal,
                TimeInfo = new DerGeneralizedTime(DateTime.Now),
                DataHash = new DerBitString(output),
                PropertyInfo = new DerIA5String(propertyInfo)
            };
            byte[] toSign = tbsSign.GetDerEncoded();
            byte[] signed = SignatureUtils.Sm2Sign(HexUtils.ToByteArray(_sealConfig.SignerPrivateKey), toSign);
            SesSignature sesSignature = new SesSignature
            {
                TbsSign = tbsSign,
                Cert = new DerOctetString(_sealConfig.SignerCert),
                SignatureAlgId = SignatureMethod,
                Signature = new DerBitString(signed)
            };
            return sesSignature.GetDerEncoded();
        }

        /// <summary>
        /// 对待签名数据签名
        /// </summary>
        /// <param name="input">在操作过程中请勿对流进行关闭</param>
        /// <param name="propertyInfo">签章属性信息</param>
        /// <returns></returns>
        public byte[] Sign(string input, string propertyInfo)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            return Sign(inputBytes, propertyInfo);
        }

        /// <summary>
        /// 创建印章
        /// </summary>
        /// <returns></returns>
        private SeSeal CreateSeal()
        {
            SesHeader sesHeader = new SesHeader(new DerInteger(ConstDefined.SesV4), new DerIA5String(_sealConfig.Manufacturer));
            SesPropertyInfo sesPropertyInfo = new SesPropertyInfo
            {
                Type = new DerInteger(3),
                Name = new DerUtf8String(_sealConfig.SealName),
                CertListType = SesPropertyInfo.CertType,
                CertList = new SesCertCollect(new CertInfoCollect(new List<Asn1OctetString> {new DerOctetString(_sealConfig.SignerCert)})),
                CreateDate = new DerGeneralizedTime(DateTime.Now),
                ValidStart = new DerGeneralizedTime(DateTime.Now),
                ValidEnd = new DerGeneralizedTime(DateTime.Now.AddYears(1))
            };
            SesPictureInfo sesPictureInfo = new SesPictureInfo
            {
                Type = new DerIA5String(_sealConfig.SealType),
                Data = new DerOctetString(_sealConfig.SealPicture),
                Width = new DerInteger(_sealConfig.SealWidth),
                Height = new DerInteger(_sealConfig.SealHeight)
            };
            SealInfo sealInfo = new SealInfo
            {
                Header = sesHeader,
                EsId = new DerIA5String(_sealConfig.EsId),
                Picture = sesPictureInfo,
                Property = sesPropertyInfo,
                ExtensionData = null
            };
            return new SeSeal
            {
                SealInfo = sealInfo,
                Cert = new DerOctetString(_sealConfig.SealCert),
                SignAlgId = GMObjectIdentifiers.sm2sign_with_sm3,
                SignedValue = new DerBitString(SignatureUtils.Sm2Sign(HexUtils.ToByteArray(_sealConfig.SealPrivateKey), sealInfo.GetEncoded()))
            };
        }
    }
}