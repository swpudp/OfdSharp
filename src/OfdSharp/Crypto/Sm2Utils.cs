using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Text;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Asn1.X509;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.X509.Extension;

namespace OfdSharp.Crypto
{
    /// <summary>
    /// SM2加密算法工具
    /// </summary>
    public static class Sm2Utils
    {
        /// <summary>
        /// 获取一条SM2曲线参数
        /// </summary>
        private static readonly X9ECParameters Sm2EcParameters = GMNamedCurves.GetByName("sm2p256v1");

        /// <summary>
        /// 默认Id
        /// </summary>
        private static readonly byte[] DefaultParamId = Strings.ToByteArray("1234567812345678");

        /// <summary>
        /// 生成SM2公私钥对
        /// </summary>
        /// <returns></returns>
        private static AsymmetricCipherKeyPair CreateKeyPairInternal()
        {
            //构造domain参数
            ECDomainParameters domainParameters = new ECDomainParameters(Sm2EcParameters.Curve, Sm2EcParameters.G, Sm2EcParameters.N);

            //1.创建密钥生成器
            ECKeyPairGenerator keyPairGenerator = new ECKeyPairGenerator();

            //2.初始化生成器,带上随机数
            keyPairGenerator.Init(new ECKeyGenerationParameters(domainParameters, SecureRandom.GetInstance("SHA1PRNG")));

            //3.生成密钥对
            AsymmetricCipherKeyPair asymmetricCipherKeyPair = keyPairGenerator.GenerateKeyPair();
            return asymmetricCipherKeyPair;
        }

        /// <summary>
        /// 生成公私钥对
        /// </summary>
        /// <param name="compressedPubKey">是否压缩公钥 默认压缩</param>
        /// <returns>Item1-公钥 Item2-私钥</returns>
        public static Tuple<string, string> CreateKeyPair(bool compressedPubKey = true)
        {
            AsymmetricCipherKeyPair asymmetricCipherKeyPair = CreateKeyPairInternal();

            //提取公钥点
            ECPoint ecPoint = ((ECPublicKeyParameters)asymmetricCipherKeyPair.Public).Q;

            //公钥前面的02或者03表示是压缩公钥,04表示未压缩公钥,04的时候,可以去掉前面的04
            string pubKey = Hex.ToHexString(ecPoint.GetEncoded(compressedPubKey));

            BigInteger privateKey = ((ECPrivateKeyParameters)asymmetricCipherKeyPair.Private).D;
            string priKey = Hex.ToHexString(privateKey.ToByteArray());

            return new Tuple<string, string>(pubKey, priKey);
        }

        /// <summary>
        /// SM2加密
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="data">明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(string publicKey, string data)
        {
            // 构造domain参数
            ECDomainParameters domainParameters = new ECDomainParameters(Sm2EcParameters.Curve, Sm2EcParameters.G, Sm2EcParameters.N);
            //提取公钥点
            ECPoint pukPoint = Sm2EcParameters.Curve.DecodePoint(Hex.Decode(publicKey));
            // 公钥前面的02或者03表示是压缩公钥，04表示未压缩公钥, 04的时候，可以去掉前面的04
            ECPublicKeyParameters publicKeyParameters = new ECPublicKeyParameters(pukPoint, domainParameters);

            SM2Engine sm2Engine = new SM2Engine();
            sm2Engine.Init(true, new ParametersWithRandom(publicKeyParameters, new SecureRandom()));

            byte[] input = Encoding.UTF8.GetBytes(data);
            byte[] arrayOfBytes = sm2Engine.ProcessBlock(input, 0, input.Length);

            return Hex.ToHexString(arrayOfBytes);
        }

        /// <summary>
        /// SM2解密
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="cipherData">密文</param>
        /// <returns></returns>
        public static string Decrypt(string privateKey, string cipherData)
        {
            byte[] cipherDataByte = Hex.Decode(cipherData);

            //构造domain参数
            ECDomainParameters domainParameters = new ECDomainParameters(Sm2EcParameters.Curve, Sm2EcParameters.G, Sm2EcParameters.N);

            BigInteger privateKeyD = new BigInteger(privateKey, 16);
            ECPrivateKeyParameters privateKeyParameters = new ECPrivateKeyParameters(privateKeyD, domainParameters);

            SM2Engine sm2Engine = new SM2Engine();

            sm2Engine.Init(false, privateKeyParameters);

            byte[] arrayOfBytes = sm2Engine.ProcessBlock(cipherDataByte, 0, cipherDataByte.Length);
            return Encoding.UTF8.GetString(arrayOfBytes);
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="content">待签名内容</param>
        /// <returns>签名值</returns>
        public static string Sign(string privateKey, string content)
        {
            //待签名内容转为字节数组
            byte[] message = Encoding.UTF8.GetBytes(content);

            //构造domain参数
            ECDomainParameters domainParameters = new ECDomainParameters(Sm2EcParameters.Curve, Sm2EcParameters.G, Sm2EcParameters.N);

            BigInteger privateKeyD = new BigInteger(privateKey, 16);
            ECPrivateKeyParameters privateKeyParameters = new ECPrivateKeyParameters(privateKeyD, domainParameters);

            //创建签名实例
            SM2Signer sm2Signer = new SM2Signer();

            //初始化签名实例,带上ID,国密的要求,ID默认值:1234567812345678
            sm2Signer.Init(true, new ParametersWithID(new ParametersWithRandom(privateKeyParameters, SecureRandom.GetInstance("SHA1PRNG")), DefaultParamId));
            sm2Signer.BlockUpdate(message, 0, message.Length);
            byte[] signBytes = sm2Signer.GenerateSignature();

            return Hex.ToHexString(signBytes);
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="content">待签名内容</param>
        /// <param name="sign">签名值</param>
        /// <returns></returns>
        public static bool Verify(string publicKey, string content, string sign)
        {
            //待签名内容
            byte[] message = Encoding.UTF8.GetBytes(content);
            byte[] signData = Hex.Decode(sign);
            // 构造domain参数
            ECDomainParameters domainParameters = new ECDomainParameters(Sm2EcParameters.Curve, Sm2EcParameters.G, Sm2EcParameters.N);
            //提取公钥点
            ECPoint pukPoint = Sm2EcParameters.Curve.DecodePoint(Hex.Decode(publicKey));
            // 公钥前面的02或者03表示是压缩公钥，04表示未压缩公钥, 04的时候，可以去掉前面的04
            ECPublicKeyParameters publicKeyParameters = new ECPublicKeyParameters(pukPoint, domainParameters);
            //创建签名实例
            SM2Signer sm2Signer = new SM2Signer();
            //初始化签名实例,带上ID,国密的要求,ID默认值:1234567812345678
            ParametersWithID parametersWithId = new ParametersWithID(publicKeyParameters, DefaultParamId);
            sm2Signer.Init(false, parametersWithId);
            sm2Signer.BlockUpdate(message, 0, message.Length);
            //验证签名结果
            bool verify = sm2Signer.VerifySignature(signData);
            return verify;
        }


        public static X509Certificate MakeCert(string subjectName, string issuerName)
        {
            AsymmetricCipherKeyPair keypair = CreateKeyPairInternal();
            ISignatureFactory sigFact = new Asn1SignatureFactory("SM3WithSM2", keypair.Private);
            X509V3CertificateGenerator sm2CertGen = new X509V3CertificateGenerator();
            sm2CertGen.SetSerialNumber(new BigInteger(128, new Random()));//128位   
            sm2CertGen.SetIssuerDN(new X509Name("CN=" + issuerName));//签发者
            sm2CertGen.SetNotBefore(DateTime.UtcNow.AddDays(-1));//有效期起
            sm2CertGen.SetNotAfter(DateTime.UtcNow.AddYears(1));//有效期止
            sm2CertGen.SetSubjectDN(new X509Name("CN=" + subjectName));//使用者
            sm2CertGen.SetPublicKey(keypair.Public); //公钥

            sm2CertGen.AddExtension(X509Extensions.BasicConstraints, true, new BasicConstraints(true));
            sm2CertGen.AddExtension(X509Extensions.SubjectKeyIdentifier, false, new SubjectKeyIdentifierStructure(keypair.Public));
            sm2CertGen.AddExtension(X509Extensions.AuthorityKeyIdentifier, false, new AuthorityKeyIdentifierStructure(keypair.Public));
            sm2CertGen.AddExtension(X509Extensions.KeyUsage, true, new KeyUsage(6));

            X509Certificate sm2Cert = sm2CertGen.Generate(sigFact);
            sm2Cert.CheckValidity();
            sm2Cert.Verify(keypair.Public);
            return sm2Cert;
        }
    }
}