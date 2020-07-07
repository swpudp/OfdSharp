using System;
using System.Collections.Generic;
using System.Text;
using OfdSharp.Core.Signatures;
using OfdSharp.Ses.V1;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Tls;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.X509;

namespace OfdSharp.Verify.Container
{
    /// <summary>
    /// 《《GM/T 0031-2014 安全电子签章密码技术规范》 电子印章数据验证
    /// 注意：仅用于测试，电子签章验证请使用符合国家规范的流程进行！
    /// </summary>
    public class SesV1ValidateContainer : SignedDataValidateContainer
    {
        public override void Validate(SigType type, string signAlgName, byte[] tbsContent, byte[] signedValue)
        {
            if (type == SigType.Sign)
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
                //throw new InvalidSignedValueException("Signature.xml 文件被篡改，电子签章失效。("+ toSign.getPropertyInfo().getString() + ")");
            }

            //sg.initVerify(signCert);
            //sg.update(toSign.getEncoded("DER"));
            //if (!sg.verify(expSigVal))
            //{
            //    throw new InvalidSignedValueException("电子签章数据签名值不匹配，电子签章数据失效。");
            //}

            // 预期的电子签章数据，签章值
            byte[] expSigVal = sesSignature.Signature.GetOctets();

            //Signature sg = Signature(toSign.getSignatureAlgorithm().getId(),new BouncyCastleProvider());
            ISigner sg = SignerUtilities.GetSigner(GMObjectIdentifiers.sm2encrypt_with_sm3);

            byte[] certDER = toSign.Cert.GetOctets();

            //new  X509V1CertificateGenerator().Generate()

            // 构造证书对象
            //Certificate signCert = new CertificateFactory().engineGenerateCertificate(new ByteArrayInputStream(certDER));
            //X509Certificate x509Certificate = new X509Certificate(new X509CertificateStructure(TbsCertificateStructure.GetInstance(certDER), null, new DerBitString(certDER)));
            X509Certificate x509Certificate = new X509CertificateParser().ReadCertificate(certDER);
            //x509Certificate.Verify();
            AsymmetricKeyParameter p = x509Certificate.GetPublicKey();
            sg.Init(false, p);

            //System.Security.Cryptography.X509Certificates.X509Certificate x509 = new System.Security.Cryptography.X509Certificates.X509Certificate(certDER);
            //sg.Init(false,new ECPublicKeyParameters());


            // 获取一条SM2曲线参数
            X9ECParameters sm2EcParameters = GMNamedCurves.GetByName("sm2p256v1");
            // 构造domain参数
            ECDomainParameters domainParameters = new ECDomainParameters(sm2EcParameters.Curve, sm2EcParameters.G, sm2EcParameters.N);
            //提取公钥点
            ECPoint pukPoint = sm2EcParameters.Curve.DecodePoint(certDER);
            // 公钥前面的02或者03表示是压缩公钥，04表示未压缩公钥, 04的时候，可以去掉前面的04
            ECPublicKeyParameters publicKeyParameters = new ECPublicKeyParameters(pukPoint, domainParameters);

            sg.Init(false, publicKeyParameters);


            byte[] input = toSign.GetDerEncoded();
            sg.BlockUpdate(input, 0, input.Length);

            bool pass = sg.VerifySignature(expSigVal);
            if (!pass)
            {
                throw new Exception();
            }
        }
    }
}
