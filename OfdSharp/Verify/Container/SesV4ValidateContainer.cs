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
    public class SesV4ValidateContainer : SignedDataValidateContainer
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
                return VerifyResult.SealTampered;
            }
            // 预期的电子签章数据，签章值
            byte[] expSigVal = sesSignature.Signature.GetOctets();
            //获取电子签章证书
            ISigner sg = SignerUtilities.GetSigner(sesSignature.SignatureAlgId);
            byte[] certDer = sesSignature.Cert.GetOctets();
            //构造证书
            X509Certificate x509Certificate = new X509CertificateParser().ReadCertificate(certDer);
            //获取公钥
            AsymmetricKeyParameter p = x509Certificate.GetPublicKey();
            sg.Init(false, p);

            byte[] toSignBytes = toSign.GetDerEncoded();
            sg.BlockUpdate(toSignBytes, 0, toSignBytes.Length);

            return !sg.VerifySignature(expSigVal) ? VerifyResult.SignedTampered : VerifyResult.Success;
        }
    }
}
