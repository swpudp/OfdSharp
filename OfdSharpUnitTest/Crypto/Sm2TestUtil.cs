using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Text;

namespace OfdSharpUnitTest.Crypto
{
    public class Sm2TestUtil
    {

        /// <summary>
        /// 生成SM2公私钥对
        /// </summary>
        /// <returns></returns>
        private static AsymmetricCipherKeyPair CreateKeyPairInternal()
        {
            //获取一条SM2曲线参数
            X9ECParameters sm2EcParameters = GMNamedCurves.GetByName("sm2p256v1");

            //构造domain参数
            ECDomainParameters domainParameters = new ECDomainParameters(sm2EcParameters.Curve, sm2EcParameters.G, sm2EcParameters.N);

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
        /// <returns></returns>
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

        ///**
        // * 私钥签名
        // * @param privateKey    私钥
        // * @param content       待签名内容
        // * @return
        // */
        //public static String sign(String privateKey, String content)
        //{
        //    //待签名内容转为字节数组
        //    byte[] message = Hex.Decode(content);

        //    //获取一条SM2曲线参数
        //    X9ECParameters sm2ECParameters = GMNamedCurves.GetByName("sm2p256v1");
        //    //构造domain参数
        //    ECDomainParameters domainParameters = new ECDomainParameters(sm2ECParameters.Curve,sm2ECParameters.G, sm2ECParameters.N);

        //    BigInteger privateKeyD = new BigInteger(privateKey, 16);
        //    ECPrivateKeyParameters privateKeyParameters = new ECPrivateKeyParameters(privateKeyD, domainParameters);

        //    //创建签名实例
        //    SM2Signer sm2Signer = new SM2Signer();

        //    //初始化签名实例,带上ID,国密的要求,ID默认值:1234567812345678
        //    sm2Signer.Init(true, new ParametersWithID(new ParametersWithRandom(privateKeyParameters, SecureRandom.GetInstance("SHA1PRNG")), Strings.ToByteArray("1234567812345678")));

        //    //生成签名,签名分为两部分r和s,分别对应索引0和1的数组
        //    byte[] bigIntegers = sm2Signer.GenerateSignature();

        //    byte[] rBytes = modifyRSFixedBytes(bigIntegers[0].toByteArray());
        //    byte[] sBytes = modifyRSFixedBytes(bigIntegers[1].toByteArray());

        //    byte[] signBytes = ByteUtils.concatenate(rBytes, sBytes);
        //    String sign = Hex.toHexString(signBytes);

        //    return sign;
        //}

        ///**
        // * 将R或者S修正为固定字节数
        // * @param rs
        // * @return
        // */
        //private static byte[] modifyRSFixedBytes(byte[] rs)
        //{
        //    int length = rs.length;
        //    int fixedLength = 32;
        //    byte[] result = new byte[fixedLength];
        //    if (length < 32)
        //    {
        //        System.arraycopy(rs, 0, result, fixedLength - length, length);
        //    }
        //    else
        //    {
        //        System.arraycopy(rs, length - fixedLength, result, 0, fixedLength);
        //    }
        //    return result;
        //}

        /**
         * 验证签名
         * @param publicKey     公钥
         * @param content       待签名内容
         * @param sign          签名值
         * @return
         */
        public static bool verify(string publicKey, string content, string sign)
        {
            //待签名内容
            byte[] message = Hex.Decode(content);
            byte[] signData = Hex.Decode(sign);

            // 获取一条SM2曲线参数
            X9ECParameters sm2EcParameters = GMNamedCurves.GetByName("sm2p256v1");
            // 构造domain参数
            ECDomainParameters domainParameters = new ECDomainParameters(sm2EcParameters.Curve, sm2EcParameters.G, sm2EcParameters.N);
            //提取公钥点
            ECPoint pukPoint = sm2EcParameters.Curve.DecodePoint(Hex.Decode(publicKey));
            // 公钥前面的02或者03表示是压缩公钥，04表示未压缩公钥, 04的时候，可以去掉前面的04
            ECPublicKeyParameters publicKeyParameters = new ECPublicKeyParameters(pukPoint, domainParameters);

            //获取签名
            //BigInteger R = null;
            //BigInteger S = null;
            //byte[] rBy = new byte[33];
            //Array.Copy(signData, 0, rBy, 1, 32);
            //rBy[0] = 0x00;
            //byte[] sBy = new byte[33];
            //Array.Copy(signData, 32, sBy, 1, 32);
            //sBy[0] = 0x00;
            //R = new BigInteger(rBy);
            //S = new BigInteger(sBy);

            //创建签名实例
            SM2Signer sm2Signer = new SM2Signer();
            ParametersWithID parametersWithId = new ParametersWithID(publicKeyParameters, Strings.ToByteArray("1234567812345678"));
            sm2Signer.Init(false, parametersWithId);

            //验证签名结果
            bool verify = sm2Signer.VerifySignature(message);
            return verify;
        }

        /// <summary>
        /// SM2加密
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="data">明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(string publicKey, string data)
        {
            // 获取一条SM2曲线参数
            X9ECParameters sm2EcParameters = GMNamedCurves.GetByName("sm2p256v1");
            // 构造domain参数
            ECDomainParameters domainParameters = new ECDomainParameters(sm2EcParameters.Curve, sm2EcParameters.G, sm2EcParameters.N);
            //提取公钥点
            ECPoint pukPoint = sm2EcParameters.Curve.DecodePoint(Hex.Decode(publicKey));
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

            //获取一条SM2曲线参数
            X9ECParameters sm2EcParameters = GMNamedCurves.GetByName("sm2p256v1");
            //构造domain参数
            ECDomainParameters domainParameters = new ECDomainParameters(sm2EcParameters.Curve, sm2EcParameters.G, sm2EcParameters.N);

            BigInteger privateKeyD = new BigInteger(privateKey, 16);
            ECPrivateKeyParameters privateKeyParameters = new ECPrivateKeyParameters(privateKeyD, domainParameters);

            SM2Engine sm2Engine = new SM2Engine();
            sm2Engine.Init(false, privateKeyParameters);

            byte[] arrayOfBytes = sm2Engine.ProcessBlock(cipherDataByte, 0, cipherDataByte.Length);
            return Encoding.UTF8.GetString(arrayOfBytes);
        }

        ///**
        // * SM2加密算法
        // * @param publicKey     公钥
        // * @param data          明文数据
        // * @return
        // */
        //public static String encrypt(PublicKey publicKey, String data)
        //{

        //ECPublicKeyParameters ecPublicKeyParameters = null;
        //if (publicKey.EncodedParameters is  ECPublicKeyParameters   ) 
        //{
        //    ECPublicKeyParameters bcecPublicKey = (ECPublicKeyParameters)publicKey;
        //    bcecPublicKey.Parameters

        //    // ecParameterSpec = bcecPublicKey.getParameters();
        //    ECDomainParameters ecDomainParameters = new ECDomainParameters(ecParameterSpec.getCurve(),
        //            ecParameterSpec.getG(), ecParameterSpec.getN());
        //    ecPublicKeyParameters = new ECPublicKeyParameters(bcecPublicKey.getQ(), ecDomainParameters);
        //}

        //SM2Engine sm2Engine = new SM2Engine();
        //sm2Engine.init(true, new ParametersWithRandom(ecPublicKeyParameters, new SecureRandom()));

        //byte[] arrayOfBytes = null;
        //try
        //{
        //    byte[] in = data.getBytes("utf-8");
        //    arrayOfBytes = sm2Engine.processBlock(in, 0, in.length);
        //}
        //catch (Exception e)
        //{
        //    log.error("SM2加密时出现异常:", e);
        //}
        //return Hex.toHexString(arrayOfBytes);
        //}



        ///**
        // * SM2解密算法
        // * @param privateKey        私钥
        // * @param cipherData        密文数据
        // * @return
        // */
        //public static String decrypt(PrivateKey privateKey, String cipherData)
        //{
        //    byte[] cipherDataByte = Hex.decode(cipherData);

        //    BCECPrivateKey bcecPrivateKey = (BCECPrivateKey)privateKey;
        //    ECParameterSpec ecParameterSpec = bcecPrivateKey.getParameters();

        //    ECDomainParameters ecDomainParameters = new ECDomainParameters(ecParameterSpec.getCurve(),
        //            ecParameterSpec.getG(), ecParameterSpec.getN());

        //    ECPrivateKeyParameters ecPrivateKeyParameters = new ECPrivateKeyParameters(bcecPrivateKey.getD(),
        //            ecDomainParameters);

        //    SM2Engine sm2Engine = new SM2Engine();
        //    sm2Engine.init(false, ecPrivateKeyParameters);

        //    String result = null;
        //    try
        //    {
        //        byte[] arrayOfBytes = sm2Engine.processBlock(cipherDataByte, 0, cipherDataByte.length);
        //        return new String(arrayOfBytes, "utf-8");
        //    }
        //    catch (Exception e)
        //    {
        //        log.error("SM2解密时出现异常:", e);
        //    }
        //    return result;
        //}

        ///**
        // * 将未压缩公钥压缩成压缩公钥
        // * @param pubKey    未压缩公钥(16进制,不要带头部04)
        // * @return
        // */
        //public static String compressPubKey(String pubKey)
        //{
        //    pubKey = CustomStringUtils.append("04", pubKey);    //将未压缩公钥加上未压缩标识.
        //                                                        // 获取一条SM2曲线参数
        //    X9ECParameters sm2ECParameters = GMNamedCurves.getByName("sm2p256v1");
        //    // 构造domain参数
        //    ECDomainParameters domainParameters = new ECDomainParameters(sm2ECParameters.getCurve(),
        //            sm2ECParameters.getG(),
        //            sm2ECParameters.getN());
        //    //提取公钥点
        //    ECPoint pukPoint = sm2ECParameters.getCurve().decodePoint(CommonUtils.hexString2byte(pubKey));
        //    // 公钥前面的02或者03表示是压缩公钥，04表示未压缩公钥, 04的时候，可以去掉前面的04
        //    //        ECPublicKeyParameters publicKeyParameters = new ECPublicKeyParameters(pukPoint, domainParameters);

        //    String compressPubKey = Hex.toHexString(pukPoint.getEncoded(Boolean.TRUE));

        //    return compressPubKey;
        //}

        ///**
        // * 将压缩的公钥解压为非压缩公钥
        // * @param compressKey   压缩公钥
        // * @return
        // */
        //public static String unCompressPubKey(String compressKey)
        //{
        //    // 获取一条SM2曲线参数
        //    X9ECParameters sm2ECParameters = GMNamedCurves.getByName("sm2p256v1");
        //    // 构造domain参数
        //    ECDomainParameters domainParameters = new ECDomainParameters(sm2ECParameters.getCurve(),
        //            sm2ECParameters.getG(),
        //            sm2ECParameters.getN());
        //    //提取公钥点
        //    ECPoint pukPoint = sm2ECParameters.getCurve().decodePoint(CommonUtils.hexString2byte(compressKey));
        //    // 公钥前面的02或者03表示是压缩公钥，04表示未压缩公钥, 04的时候，可以去掉前面的04
        //    //        ECPublicKeyParameters publicKeyParameters = new ECPublicKeyParameters(pukPoint, domainParameters);

        //    String pubKey = Hex.toHexString(pukPoint.getEncoded(Boolean.FALSE));
        //    pubKey = pubKey.substring(2);       //去掉前面的04   (04的时候，可以去掉前面的04)

        //    return pubKey;
        //}

    }

}
