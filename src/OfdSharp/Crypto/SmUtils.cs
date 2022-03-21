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
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.X509.Extension;
using Org.BouncyCastle.Crypto.Digests;

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
        public static CipherKeyPair CreateKeyPair(bool compressedPubKey = true)
        {
            AsymmetricCipherKeyPair asymmetricCipherKeyPair = CreateKeyPairInternal();

            //提取公钥点
            ECPoint ecPoint = ((ECPublicKeyParameters)asymmetricCipherKeyPair.Public).Q;

            //公钥前面的02或者03表示是压缩公钥,04表示未压缩公钥,04的时候,可以去掉前面的04
            string pubKey = Hex.ToHexString(ecPoint.GetEncoded(compressedPubKey));

            BigInteger privateKey = ((ECPrivateKeyParameters)asymmetricCipherKeyPair.Private).D;
            string priKey = Hex.ToHexString(privateKey.ToByteArray());

            return new CipherKeyPair(pubKey, priKey);
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
        public static byte[] Sign(string privateKey, string content)
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

            return signBytes;
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="content">待签名内容</param>
        /// <returns>签名值</returns>
        public static byte[] Sign(string privateKey, byte[] content)
        {
            //构造domain参数
            ECDomainParameters domainParameters = new ECDomainParameters(Sm2EcParameters.Curve, Sm2EcParameters.G, Sm2EcParameters.N);

            BigInteger privateKeyD = new BigInteger(privateKey, 16);
            ECPrivateKeyParameters privateKeyParameters = new ECPrivateKeyParameters(privateKeyD, domainParameters);

            //创建签名实例
            SM2Signer sm2Signer = new SM2Signer();

            //初始化签名实例,带上ID,国密的要求,ID默认值:1234567812345678
            sm2Signer.Init(true, new ParametersWithID(new ParametersWithRandom(privateKeyParameters, SecureRandom.GetInstance("SHA1PRNG")), DefaultParamId));
            sm2Signer.BlockUpdate(content, 0, content.Length);
            byte[] signBytes = sm2Signer.GenerateSignature();

            return signBytes;
        }

        /// <summary>
        /// SM3计算摘要
        /// </summary>
        /// <param name="data">待计算字符内容</param>
        /// <returns>摘要字符</returns>
        public static byte[] Digest(byte[] data)
        {
            SM3Digest digest = new SM3Digest();
            byte[] cipherBytes = digest.ComputeHashBytes(data);
            return cipherBytes;
        }

        /// <summary>
        /// SM3计算摘要
        /// </summary>
        /// <param name="data">待计算字符内容</param>
        /// <param name="encoding">编码</param>
        /// <returns>摘要字符</returns>
        public static byte[] Digest(string data, Encoding encoding)
        {
            SM3Digest digest = new SM3Digest();
            byte[] cipherBytes = digest.ComputeHashBytes(data, encoding);
            return cipherBytes;
        }

        /// <summary>
        /// 计算Hash字节
        /// </summary>
        /// <param name="digest"></param>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        private static byte[] ComputeHashBytes(this IDigest digest, string data, Encoding encoding)
        {
            var hashBytes = new byte[digest.GetDigestSize()];
            var bs = encoding.GetBytes(data);
            digest.BlockUpdate(bs, 0, bs.Length);
            digest.DoFinal(hashBytes, 0);
            return hashBytes;
        }

        /// <summary>
        /// 计算Hash字节
        /// </summary>
        /// <param name="digest"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] ComputeHashBytes(this IDigest digest, byte[] data)
        {
            var hashBytes = new byte[digest.GetDigestSize()];
            digest.BlockUpdate(data, 0, data.Length);
            digest.DoFinal(hashBytes, 0);
            return hashBytes;
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

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="alg">公钥</param>
        /// <param name="cert">公钥</param>
        /// <param name="content">待签名内容</param>
        /// <param name="sign">签名值</param>
        /// <returns></returns>
        public static bool Verify(string alg, X509Certificate cert, byte[] content, byte[] sign)
        {
            ISigner signer = SignerUtilities.GetSigner(alg);
            AsymmetricKeyParameter p = cert.GetPublicKey();
            signer.Init(false, p);
            signer.BlockUpdate(content, 0, content.Length);
            //验证签名结果
            bool verify = signer.VerifySignature(sign);
            return verify;
        }

        public static X509Certificate MakeCert(string subjectName, string issuerName)
        {
            AsymmetricCipherKeyPair keypair = CreateKeyPairInternal();
            return MakeCert(keypair.Private, keypair.Public, subjectName, issuerName);
        }

        public static X509Certificate MakeCert(string publicKey, string privateKey, string subjectName, string issuerName)
        {
            ECDomainParameters domainParameters = new ECDomainParameters(Sm2EcParameters.Curve, Sm2EcParameters.G, Sm2EcParameters.N);
            AsymmetricKeyParameter privateParameter = new ECPrivateKeyParameters(new BigInteger(1, Hex.Decode(privateKey)), domainParameters);
            AsymmetricKeyParameter publicParameter = new ECPublicKeyParameters(domainParameters.Curve.DecodePoint(Hex.Decode(publicKey)), domainParameters);
            return MakeCert(privateParameter, publicParameter, subjectName, issuerName);
        }

        private static X509Certificate MakeCert(AsymmetricKeyParameter privateParameter, AsymmetricKeyParameter publicParameter, string subjectName, string issuerName)
        {
            ISignatureFactory sigFact = new Asn1SignatureFactory(GMObjectIdentifiers.sm2sign_with_sm3.Id, privateParameter);
            X509V3CertificateGenerator sm2CertGen = new X509V3CertificateGenerator();
            sm2CertGen.SetSerialNumber(new BigInteger(128, new Random()));//128位   
            sm2CertGen.SetIssuerDN(new X509Name("CN=" + issuerName));//签发者
            sm2CertGen.SetNotBefore(DateTime.UtcNow.AddDays(-1));//有效期起
            sm2CertGen.SetNotAfter(DateTime.UtcNow.AddYears(1));//有效期止
            sm2CertGen.SetSubjectDN(new X509Name("CN=" + subjectName));//使用者
            sm2CertGen.SetPublicKey(publicParameter); //公钥

            sm2CertGen.AddExtension(X509Extensions.BasicConstraints, true, new BasicConstraints(true));
            sm2CertGen.AddExtension(X509Extensions.SubjectKeyIdentifier, false, new SubjectKeyIdentifierStructure(publicParameter));
            sm2CertGen.AddExtension(X509Extensions.AuthorityKeyIdentifier, false, new AuthorityKeyIdentifierStructure(publicParameter));
            sm2CertGen.AddExtension(X509Extensions.KeyUsage, true, new KeyUsage(6));

            X509Certificate sm2Cert = sm2CertGen.Generate(sigFact);
            sm2Cert.CheckValidity();
            sm2Cert.Verify(publicParameter);
            return sm2Cert;
        }
    }
}