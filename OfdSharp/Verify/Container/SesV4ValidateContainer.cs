using OfdSharp.Core.Signs;
using OfdSharp.Ses.V4;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using System;

namespace OfdSharp.Verify.Container
{
    /// <summary>
    /// 《GB/T 38540-2020 信息安全技术 安全电子签章密码技术规范》 电子印章数据验证
    /// 注意：仅用于测试，电子签章验证请使用符合国家规范的流程进行！
    /// </summary>
    internal class SesV4ValidateContainer : SignedDataValidateContainer
    {
        /// <summary>
        /// 签名数据验证
        /// </summary>
        /// <param name="type">电子签名类型</param>
        /// <param name="tbsContent">待签章内容</param>
        /// <param name="signedValue">电子签章数据或签名值（SignedValue.xml文件内容）</param>
        public override VerifyResult Validate(SigType type, byte[] tbsContent, byte[] signedValue)
        {
            if (type == SigType.Sign)
            {
                throw new ArgumentOutOfRangeException(nameof(type), "签名类型(type)必须是 Seal，不支持电子印章验证");
            }
            //计算原文摘要
            SM3Digest md = new SM3Digest();
            md.BlockUpdate(tbsContent, 0, tbsContent.Length);
            byte[] output = new byte[32];
            md.DoFinal(output, 0);

            SesSignature sesSignature = SesSignature.GetInstance(signedValue);
            TbsSign toSign = sesSignature.TbsSign;

            byte[] exceptHash = toSign.DataHash.GetOctets();
            if (!Arrays.AreEqual(output, exceptHash))
            {
                return VerifyResult.SignedNotMatch;
            }
            //加载证书
            byte[] certDer = sesSignature.Cert.GetOctets();
            X509CertificateParser parser = new X509CertificateParser();
            X509Certificate cert = parser.ReadCertificate(certDer);
            //判断证书是否过期
            if (!cert.IsValid(DateTime.Now))
            {
                return VerifyResult.SealOutdated;
            }
            //获取签名验证对象
            ISigner signer = SignerUtilities.GetSigner(sesSignature.SignatureAlgId);
            AsymmetricKeyParameter p = cert.GetPublicKey();
            signer.Init(false, p);
            byte[] buf = toSign.GetDerEncoded();
            signer.BlockUpdate(buf, 0, buf.Length);

            //预期的电子签章数据，签章值
            byte[] expect = sesSignature.Signature.GetOctets();

            //验证签名
            bool result = signer.VerifySignature(expect);
            return result ? VerifyResult.Success : VerifyResult.SealTampered;
        }
    }
}
