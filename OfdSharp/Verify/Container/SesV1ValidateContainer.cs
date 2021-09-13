using OfdSharp.Core.Signature;
using OfdSharp.Ses.V1;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using System;

namespace OfdSharp.Verify.Container
{
    /// <summary>
    /// 《《GM/T 0031-2014 安全电子签章密码技术规范》 电子印章数据验证
    /// 注意：仅用于测试，电子签章验证请使用符合国家规范的流程进行！
    /// </summary>
    internal class SesV1ValidateContainer : SignedDataValidateContainer
    {
        public override VerifyResult Validate(SignedType type, byte[] tbsContent, byte[] signedValue)
        {
            if (type == SignedType.Sign)
            {
                throw new ArgumentOutOfRangeException(nameof(type), "签名类型(type)必须是 Seal，不支持电子印章验证");
            }

            // 计算原文摘要
            GeneralDigest md = new SM3Digest();
            md.BlockUpdate(tbsContent, 0, tbsContent.Length);
            byte[] expect = new byte[32];
            md.DoFinal(expect, 0);

            SesSignature sesSignature = SesSignature.GetInstance(signedValue);
            TbsSign toSign = sesSignature.ToSign;
            byte[] expectDataHash = toSign.DataHash.GetOctets();

            // 比较原文摘要
            if (!Arrays.AreEqual(expect, expectDataHash))
            {
                return VerifyResult.SignedTampered;
            }

            // 预期的电子签章数据，签章值
            byte[] expSigVal = sesSignature.Signature.GetOctets();
            ISigner sg = SignerUtilities.GetSigner(toSign.SignatureAlgorithm);
            byte[] certDer = toSign.Cert.GetOctets();

            // 构造证书对象
            X509Certificate x509Certificate = new X509CertificateParser().ReadCertificate(certDer);
            AsymmetricKeyParameter p = x509Certificate.GetPublicKey();
            sg.Init(false, p);

            byte[] input = toSign.GetDerEncoded();
            sg.BlockUpdate(input, 0, input.Length);

            if (!sg.VerifySignature(expSigVal))
            {
                return VerifyResult.SignedTampered;
            }
            return VerifyResult.Success;
        }
    }
}
