using OfdSharp.Primitives.Signatures;
using OfdSharp.Primitives.Signature;
using OfdSharp.Reader;
using OfdSharp.Ses.Parse;
using OfdSharp.Ses.V4;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using System;
using System.Linq;
using OfdSharp.Crypto;

namespace OfdSharp.Verify
{
    /// <summary>
    /// OFD电子签名验证引擎
    /// </summary>
    public static class OfdValidator
    {
        /// <summary>
        /// 执行OFD电子签名验证
        ///校验过程：
        /// 1. 检查文件完整性
        /// 获取SignedInfo节点
        /// 1）从此节点获取References，References由多个Reference组成
        /// 2）从此节点获取CheckMethod，签名方法: IDigest digest = DigestUtilities.GetDigest(GMObjectIdentifiers.sm3);//1.2.156.10197.1.401
        /// 3）循环每个Reference，FileRef节点指名了签名文件路径，CheckValue节点指名了签名值base64字符串
        /// 4）读取FileRef指向的文件的内容
        /// 5）使用3）步获取签名方法对文件内容签名，并与CheckValue获取的值比较
        /// 2.签章验证
        /// 1）获取SignedValue节点，获取签章值文件路径：/Doc_0/Signs/Sign_0/SignedValue.dat
        /// 2）从SignedInfo节点下获取Seal节点，获取印章数据文件路径：/Doc_0/Signs/Sign_0/Seal.esl
        /// 3）读取印章数据文件并检查印章匹配
        /// 4）获取SignatureMethod节点，获取签名算法名称：1.2.156.10197.1.501，获取签名算法：IDigest digest = DigestUtilities.GetDigest(GMObjectIdentifiers.sm2sign_with_sm3);//1.2.156.10197.1.401
        /// 5）使用签章数据检验签名信息是否匹配
        /// </summary>
        public static VerifyResult Validate(OfdReader reader)
        {
            //1、读取OFD.xml文件的ofd:Signatures节点
            //2、读取Doc_0\Signs\Signatures.xml文件的ofd:Signature节点，从BaseLoc特性获取签名文件路径，如/Doc_0/Signs/Sign_0/Signature.xml
            //3、读取/Doc_0/Signs/Sign_0/Signature.xml文件
            DigestInfo signature = reader.GetDigestInfo();
            //获取签名算法
            IDigest digest = DigestUtilities.GetDigest(signature.SignedInfo.ReferenceCollect.CheckMethod);
            //验证文件完整性
            VerifyResult result = CheckFileIntegrity(reader, signature, digest);
            if (result != VerifyResult.Success)
            {
                return result;
            }

            //电子印章与电子签章数据的匹配性检查
            result = CheckSealMatch(reader, signature);

            //检查电子签章数据
            return result == VerifyResult.Success ? CheckSignedValue(reader, signature) : result;
        }

        /// <summary>
        /// 验证文件完整性
        /// </summary>
        private static VerifyResult CheckFileIntegrity(OfdReader reader, DigestInfo signature, IDigest digest)
        {
            foreach (Reference referenceItem in signature.SignedInfo.ReferenceCollect.Items)
            {
                VerifyResult verifyResult = CheckFileIntegrity(reader, digest, referenceItem);
                if (verifyResult != VerifyResult.Success)
                {
                    return verifyResult;
                }
            }

            return VerifyResult.Success;
        }

        /// <summary>
        /// 验证文件完整性
        /// </summary>
        private static VerifyResult CheckFileIntegrity(OfdReader reader, IDigest digest, Reference item)
        {
            byte[] contentBytes = reader.ReadContent(item.FileRef);
            digest.BlockUpdate(contentBytes, 0, contentBytes.Length);
            byte[] output = new byte[32];
            digest.DoFinal(output, 0);
            byte[] checkBytes = Convert.FromBase64String(item.CheckValue.Value);
            return Arrays.AreEqual(output, checkBytes) ? VerifyResult.Success : VerifyResult.FileTampered;
        }

        /// <summary>
        /// 检查印章匹配[可选-存在Seal.esl则验证，不存在不验证]
        /// 验证文件：Doc_0\Signs\Sign_0\Seal.esl、Doc_0\Signs\Sign_0\SignedValue.dat
        /// </summary>
        private static VerifyResult CheckSealMatch(OfdReader reader, DigestInfo signature)
        {
            if (signature.SignedInfo.Seal == null)
            {
                return VerifyResult.Success;
            }

            byte[] sesSignatureBin = reader.ReadContent(signature.SignedValue);
            SesVersionHolder holder = VersionParser.ParseSignatureVersion(sesSignatureBin);
            if (holder.Version == SesVersion.V4)
            {
                SesSignature v4Signature = SesSignature.GetInstance(holder.Sequence);
                SeSeal seal = v4Signature.TbsSign.EsSeal;
                byte[] expect = seal.GetDerEncoded();

                byte[] sealBytes = reader.ReadContent(signature.SignedInfo.Seal.BaseLoc.Value);
                if (!Arrays.AreEqual(expect, sealBytes))
                {
                    return VerifyResult.SealNotMatch;
                }
            }
            return VerifyResult.Success;
        }


        /// <summary>
        /// 检查电子签章数据
        /// 验证文件：Doc_0\Signs\Sign_0\Signature.xml、Doc_0\Signs\Sign_0\SignedValue.dat
        /// </summary>
        private static VerifyResult CheckSignedValue(OfdReader reader, DigestInfo signature)
        {
            string signatureFilePath = reader.GetSignatureInfo().Signatures.First().BaseLoc;
            byte[] tbsContent = reader.ReadContent(signatureFilePath);
            byte[] signedValue = reader.ReadContent(signature.SignedValue);
            return Validate(tbsContent, signedValue);
        }

        /// <summary>
        /// 签名数据验证，技术标准：《GB/T 38540-2020 信息安全技术 安全电子签章密码技术规范》 电子印章数据验证
        /// </summary>
        /// <param name="tbsContent">待签章内容</param>
        /// <param name="signedValue">电子签章数据或签名值（SignedValue.xml文件内容）</param>
        private static VerifyResult Validate(byte[] tbsContent, byte[] signedValue)
        {
            //计算原文摘要
            byte[] output = Sm2Utils.Digest(tbsContent);

            SesSignature sesSignature = SesSignature.GetInstance(signedValue);
            TbsSign toSign = sesSignature.TbsSign;

            byte[] exceptHash = toSign.DataHash.GetOctets();
            if (!Arrays.AreEqual(output, exceptHash))
            {
                return VerifyResult.SignedNotMatch;
            }
            //加载证书
            byte[] certDer = sesSignature.Cert.GetOctets();
            Org.BouncyCastle.X509.X509CertificateParser parser = new Org.BouncyCastle.X509.X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate cert = parser.ReadCertificate(certDer);
            //判断证书是否过期
            if (!cert.IsValid(DateTime.Now))
            {
                return VerifyResult.SealOutdated;
            }
            //预期的电子签章数据，签章值
            byte[] expect = sesSignature.Signature.GetOctets();
            //验证签名
            byte[] signed = toSign.GetDerEncoded();
            bool result = Sm2Utils.Verify(sesSignature.SignatureAlgId.Id, cert, signed, expect);
            //验证签名
            return result ? VerifyResult.Success : VerifyResult.SealTampered;
        }
    }
}