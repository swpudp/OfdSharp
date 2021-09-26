using Microsoft.VisualStudio.TestTools.UnitTesting;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Security.Cryptography;
using System.Text;

namespace UnitTests.Crypto
{
    [TestClass]
    public class Sm3DigestTests
    {
        [TestMethod]
        public void GetDigestSizeTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Sm3DigestNoContractorTest()
        {
            SM3Digest sm3Digest = new SM3Digest();
            SM2Engine sm2 = new SM2Engine(sm3Digest);

        }

        [TestMethod]
        public void Sm3DigestContractorTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ResetTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void DoFinalTest()
        {
            Assert.Fail();
        }

        /// <summary>
        /// sha256 System和BC库加密对比
        /// </summary>
        [TestMethod]
        public void Sha256Test()
        {
            string content = Guid.NewGuid().ToString();
            string hashBc = Sha256(content);
            string hashSys = Sha256WithSystem(content);
            Assert.AreEqual(hashBc, hashSys);
        }

        /// <summary>
        /// md5 System和BC库加密对比
        /// </summary>
        [TestMethod]
        public void Md5Test()
        {
            string content = Guid.NewGuid().ToString();
            string hashBc = Md5(content);
            string hashSys = Md5WithSystem(content);
            Assert.AreEqual(hashBc, hashSys);
        }

        [TestMethod]
        public void ECBTest()
        {
            string secret = "ZWNyOC00MjAhLWFmNjEtMzAhYTYxZDEhMWV2MC42NjP2MjA0NDY3NDU5MjgwLjk4";
            string data = "{\"IsSuccessful\":true,\"ResultSign\":0,\"MessageKey\":\"操作成功\"}";

            //string sha256 = Sha256(secret);
            string sha256Key = Sha256(secret);
            Console.WriteLine("sha256Key is " + sha256Key);

            //string sm3 = Sm3(sha256);
            string sm3Key = Sm3(sha256Key);
            Console.WriteLine("sm3Key is " + sm3Key);

            //string md5 = Md5(sm3);
            string md5Key = Md5(sm3Key);
            Console.WriteLine("md5Key is " + md5Key);

            string ecb = Encrypt_ECB(md5Key, data);
            Console.WriteLine("ecb is " + ecb);
        }

        /// <summary>
        /// 加密测试
        /// </summary>
        [TestMethod]
        public void EncryptTest()
        {
            string secret = "ZWNyOC00MjAhLWFmNjEtMzAhYTYxZDEhMWV2MC42NjP2MjA0NDY3NDU5MjgwLjk4";
            string data = "{\"IsSuccessful\":true,\"ResultSign\":0,\"MessageKey\":\"操作成功\"}";
            string sign = Encrypt(secret, data);
            string expect = "QUY5NDIxMkQ3NzQ3Qjc4NjJEMTM4MTg1NjBGQkEyMzk4OTUwNENGRUI1NDlGNkY4RUJCQjdFMTdFMUU0MkMxRUVDMjk3MjNCOTI0RDA4MTFFMUUwOUNFRTJGNTI3RjNCMUIwMDI5NEQ3MTU4RjY2QTU3RTBCRjMzMUI2MkQ1NEM3MkI4QkVDNkU0MDUwRDU3Qzc0RUVERDhDNERDN0RBRg==";
            Assert.AreEqual(expect, sign);
        }


        /// <summary>
        /// 加密测试
        /// </summary>
        [TestMethod]
        public void DecryptTest()
        {
            string secret = "ZWNyOC00MjAhLWFmNjEtMzAhYTYxZDEhMWV2MC42NjP2MjA0NDY3NDU5MjgwLjk4";
            string plainText = "{\"IsSuccessful\":true,\"ResultSign\":0,\"MessageKey\":\"操作成功\"}";
            string encrypted = "QUY5NDIxMkQ3NzQ3Qjc4NjJEMTM4MTg1NjBGQkEyMzk4OTUwNENGRUI1NDlGNkY4RUJCQjdFMTdFMUU0MkMxRUVDMjk3MjNCOTI0RDA4MTFFMUUwOUNFRTJGNTI3RjNCMUIwMDI5NEQ3MTU4RjY2QTU3RTBCRjMzMUI2MkQ1NEM3MkI4QkVDNkU0MDUwRDU3Qzc0RUVERDhDNERDN0RBRg==";
            string decrypted = Decrypt(secret, encrypted);
            Assert.AreEqual(plainText, decrypted);
        }

        /// <summary>
        /// 签名测试
        /// </summary>
        [TestMethod]
        public void SignTest()
        {
            string privateKey = "56b73482e86a2d922e75d5b23118ea11ad932728bd2a7b6da0233506412af1e7";
            string plainText = "{\"IsSuccessful\":true,\"ResultSign\":0,\"MessageKey\":\"操作成功\"}";
            string sign = Sign(privateKey, plainText);
            string expectSign = "MzA0NDAyMjA2YTAxYjQyYThjZjEwNzQwZGRlOTEwODE2NmE3NzIzOGNjMjg2N2E5Mjk1ZDg0MDM0N2E2ZmZjODQ0NTlhYTBiMDIyMDY3NGY4ZmRhZGQxNWY5OGRjNTg5M2Y1M2NiMTU5NzgxZGEyZGEzZTBjNzMwY2VjYjM3MGE5YjY3OThkYjc1MDY=";
            Assert.AreEqual(expectSign, sign);
        }

        /// <summary>
        /// 验证签名测试
        /// </summary>
        [TestMethod]
        public void VerifySignTest()
        {
            string publicKey = "04fbe7ee23e6d0f823c4bc0bbbbda63a83dd7d9e2c5c451ba00452727d1374eb9cab78fc52e721d3e3679723fc98dec887c03082e697f21d067d8510def3de0e1c";
            string plainText = "{\"transNo\":\"04972108050170144432655765\",\"statusCode\":\"0002\",\"statusMsg\":\"系统名称sysName 不能为空\"}";
            string expectSign = "MzA0NTAyMjEwMDlhMGI1MDQyNmFiNmM2ZWJkNDE5YTU3MzEwYTJiZDliYzFmOWRmNDYyYjdmZjg2MTlkMGZkMjcyYTQ0ODAxZGQwMjIwNTNhYmQ3N2ZmZjFiZDI1N2YyOWIwNjVlMWIwYzQwNDI2YzM5N2QxNDc5Y2FlNGMxNjAwOTg3ZTJlNDJmMDUyYw==";
            bool result = VerifySign(plainText, publicKey, expectSign);
            Assert.IsTrue(result);
        }

        /// <summary>
        /// 报文体加密
        /// </summary>
        /// <param name="secret">X-SPDB-Client-Secret</param>
        /// <param name="data">请求报文</param>
        /// <returns>加密报文</returns>
        public static string Encrypt(string secret, string data)
        {
            string sha256Key = Sha256(secret);
            string sm3Key = Sm3(sha256Key);
            string md5Key = Md5(sm3Key);
            return Encrypt_ECB(md5Key, data);
        }

        /// <summary>
        /// 报文体加密
        /// </summary>
        /// <param name="secret">X-SPDB-Client-Secret</param>
        /// <param name="encrypted">请求报文</param>
        /// <returns>加密报文</returns>
        public static string Decrypt(string secret, string encrypted)
        {
            string sha256Key = Sha256(secret);
            string sm3Key = Sm3(sha256Key);
            string md5Key = Md5(sm3Key);
            return Decrypt_ECB(md5Key, encrypted);
        }

        private static string Sha256(string textPlain)
        {
            Sha256Digest sha256Digest = new Sha256Digest();
            var hashBytes = new byte[sha256Digest.GetDigestSize()];
            var bs = Encoding.UTF8.GetBytes(textPlain);
            sha256Digest.BlockUpdate(bs, 0, bs.Length);
            sha256Digest.DoFinal(hashBytes, 0);
            return Encoding.UTF8.GetString(Hex.Encode(hashBytes)).ToLower();
        }

        /// <summary>
        /// sm3加密处理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string Sm3(string data)
        {
            byte[] msg = Encoding.UTF8.GetBytes(data);
            byte[] md = new byte[32];
            SM3Digest sm3 = new SM3Digest();
            sm3.BlockUpdate(msg, 0, msg.Length);
            sm3.DoFinal(md, 0);
            return Encoding.UTF8.GetString(Hex.Encode(md)).ToLower();
        }

        /// <summary>
        /// md5加密处理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string Md5(string data)
        {
            MD5Digest sha256Digest = new MD5Digest();
            var hashBytes = new byte[sha256Digest.GetDigestSize()];
            var bs = Encoding.UTF8.GetBytes(data);
            sha256Digest.BlockUpdate(bs, 0, bs.Length);
            sha256Digest.DoFinal(hashBytes, 0);
            return Encoding.UTF8.GetString(Hex.Encode(hashBytes)).ToLower();
        }

        /// <summary>
        /// md5加密处理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string Md5WithSystem(string data)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(data);
            byte[] targetData = md5.ComputeHash(fromData);
            return BitConverter.ToString(targetData).Replace("-", "").ToLower();
        }

        private static string Sha256WithSystem(string textPlain)
        {
            byte[] bytValue = Encoding.UTF8.GetBytes(textPlain);
            try
            {
                SHA256 sha256 = new SHA256CryptoServiceProvider();
                byte[] retVal = sha256.ComputeHash(bytValue);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetSHA256HashFromString() fail,error:" + ex.Message);
            }
        }

        /// <summary>
        /// 加密ECB模式
        /// </summary>
        /// <param name="secretKey">密钥</param>
        /// <param name="plainText">明文</param>
        /// <returns>返回密文</returns>
        public static string Encrypt_ECB(string secretKey, string plainText)
        {
            //将加密报文转化为字节数组utf8
            byte[] msg = Encoding.UTF8.GetBytes(plainText);
            //将16进制密钥转化为字节数组
            byte[] keyBytes = Hex.Decode(secretKey);
            KeyParameter key = ParameterUtilities.CreateKeyParameter("SM4", keyBytes);
            IBufferedCipher cipher = CipherUtilities.GetCipher("SM4/ECB/PKCS5Padding");
            cipher.Init(true, key);
            byte[] buf = new byte[cipher.GetOutputSize(msg.Length)];
            cipher.DoFinal(msg, 0, msg.Length, buf, 0);
            //将加密后的字节数组转化为16进制字符串
            string hexEncry = Hex.ToHexString(buf).ToUpper();
            //将16进制字符串转化为base64字符串
            byte[] hexBytes = Encoding.UTF8.GetBytes(hexEncry);
            return Convert.ToBase64String(hexBytes);
        }

        /// <summary>
        /// 解密ECB模式
        /// </summary>
        /// <param name="secretKey">密钥</param>
        /// <param name="plainText">明文</param>
        /// <returns>返回密文</returns>
        public static string Decrypt_ECB(string secret, string encrypted)
        {
            //将加密报文转化为字节数组utf8
            byte[] encryptedBytes = Convert.FromBase64String(encrypted);
            byte[] cipherBytes = Hex.Decode(Encoding.UTF8.GetString(encryptedBytes));
            Console.WriteLine("cipherBytes is " + Convert.ToBase64String(cipherBytes));
            //将16进制密钥转化为字节数组
            byte[] keyBytes = Hex.Decode(secret);
            KeyParameter key = ParameterUtilities.CreateKeyParameter("SM4", keyBytes);
            IBufferedCipher cipher = CipherUtilities.GetCipher("SM4/ECB/PKCS5Padding");
            cipher.Init(false, key);

            //byte[] buf = cipher.ProcessBytes(cipherBytes);

            //todo 解密结果不全
            //byte[] buf = new byte[cipher.GetUpdateOutputSize(cipherBytes.Length)];
            //cipher.DoFinal(cipherBytes, 0, cipherBytes.Length, buf, 0);

            byte[] buf = cipher.DoFinal(cipherBytes);
            return Encoding.UTF8.GetString(buf);
        }


        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="content">签名内容</param>
        /// <returns></returns>
        public static string Sign(string privateKey, string content)
        {
            string sign = Sm2Sign(privateKey, content);
            Console.WriteLine("sign is " + sign);
            byte[] hexBytes = Encoding.UTF8.GetBytes(sign);
            return Convert.ToBase64String(hexBytes);
        }

        public static string[] ecc_param = {
                "FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00000000FFFFFFFFFFFFFFFF",
                "FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00000000FFFFFFFFFFFFFFFC",
                "28E9FA9E9D9F5E344D5A9E4BCF6509A7F39789F515AB8F92DDBCBD414D940E93",
                "FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFF7203DF6B21C6052B53BBF40939D54123",
                "32C4AE2C1F1981195F9904466A39C9948FE30BBFF2660BE1715A4589334C74C7",
                "BC3736A2F4F6779C59BDCEE36B692153D0A9877CC62A474002DF32E52139F0A0" };

        private static ECDomainParameters CreateParamsF2m()
        {
            var ecc_p = new BigInteger(ecc_param[0], 16);
            var ecc_a = new BigInteger(ecc_param[1], 16);
            var ecc_b = new BigInteger(ecc_param[2], 16);
            var ecc_n = new BigInteger(ecc_param[3], 16);
            var ecc_gx = new BigInteger(ecc_param[4], 16);
            var ecc_gy = new BigInteger(ecc_param[5], 16);
            var ecc_curve = new FpCurve(ecc_p, ecc_a, ecc_b, ecc_n, BigInteger.One);
            var ecc_bc_spec = new ECDomainParameters(ecc_curve, ecc_curve.CreatePoint(ecc_gx, ecc_gy), ecc_n);
            return ecc_bc_spec;
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="privateKey">公钥</param>
        /// <param name="content">待签名内容</param>
        /// <param name="sign">签名值</param>
        /// <returns></returns>
        private static string Sm2Sign(string privateKey, string content)
        {
            //待签名内容
            byte[] message = Encoding.UTF8.GetBytes(content);
            // 构造domain参数
            var domainParameters = CreateParamsF2m();
            byte[] privateKeyBytes = Hex.Decode(privateKey);
            var privateKeyParameters = new ECPrivateKeyParameters(new BigInteger(1, privateKeyBytes), domainParameters);
            //创建签名实例
            SM2Signer sm2Signer = new SM2Signer();
            sm2Signer.Init(true, privateKeyParameters);
            sm2Signer.BlockUpdate(message, 0, message.Length);
            byte[] result = sm2Signer.GenerateSignature();
            return Hex.ToHexString(result);
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="pubKey">公钥</param>
        /// <param name="content">待签名内容</param>
        /// <param name="sign">签名值</param>
        /// <returns></returns>
        private static bool VerifySign(string content, string publicKey, string sign)
        {
            byte[] base64Sign = Convert.FromBase64String(sign);
            string hexSign = Encoding.UTF8.GetString(base64Sign);
            byte[] signHex = Hex.Decode(hexSign);
            // 构造domain参数
            var domainParameters = CreateParamsF2m();
            byte[] publicKeyBytes = Hex.Decode(publicKey);
            var publicKeyParameters = new ECPublicKeyParameters(domainParameters.Curve.DecodePoint(publicKeyBytes), domainParameters);
            string sha1Data = Sha1(content);
            byte[] contentBytes = Encoding.UTF8.GetBytes(sha1Data);
            //创建签名实例
            SM2Signer sm2Signer = new SM2Signer();
            sm2Signer.Init(false, publicKeyParameters);
            sm2Signer.BlockUpdate(contentBytes, 0, contentBytes.Length);
            return sm2Signer.VerifySignature(signHex);
        }


        /// <summary>
        /// sha1加密处理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string Sha1(string data)
        {
            Sha1Digest sha256Digest = new Sha1Digest();
            var hashBytes = new byte[sha256Digest.GetDigestSize()];
            var bs = Encoding.UTF8.GetBytes(data);
            sha256Digest.BlockUpdate(bs, 0, bs.Length);
            sha256Digest.DoFinal(hashBytes, 0);
            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// sha1加密处理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string Sha1WithSystem(string data)
        {
            var strRes = Encoding.UTF8.GetBytes(data);
            HashAlgorithm iSha = new SHA1CryptoServiceProvider();
            byte[] res = iSha.ComputeHash(strRes);
            return Convert.ToBase64String(res);
        }

        [TestMethod]
        public void Sha1Test()
        {
            string content = Guid.NewGuid().ToString();
            string hashBc = Sha1(content);
            string hashSys = Sha1WithSystem(content);
            Assert.AreEqual(hashBc, hashSys);
        }
    }
}