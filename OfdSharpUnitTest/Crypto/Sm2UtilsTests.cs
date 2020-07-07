using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfdSharp.Crypto;
using System;
using System.Linq;

namespace OfdSharpUnitTest.Crypto
{
    [TestClass]
    public class Sm2UtilsTests
    {
        [TestMethod]
        public void EncryptTest()
        {
            string publicKey = "03b2b5c034deec0efdb3d7c195d5c1bed560fe9b30bd49bb5a7d3dd04ab1987d55";
            string cipherText = Sm2Utils.Encrypt(publicKey, "test");
            Assert.IsNotNull(cipherText);
            Console.WriteLine(cipherText);
        }

        [TestMethod]
        public void DecryptTest()
        {
            string privateKey = "00ce393e0cf8d8b60196f8b7efc50122b70d341e5f26875317ae2a5add37e9d711";
            string cipherText = "04ed3ee3733ddde2c3c0bbf12316df287a6d53ce3000863efded725f54abf3b2fb1f65446104a385450e962b49624f82b477a17bffec8cf2fa4a35d4d87f77c6c290160b0b52b27aa89a1a6bdb14aeedd91a8de66a0b292d3b1ecd2129daa9d368c662363e";
            string plainText = Sm2Utils.Decrypt(privateKey, cipherText);
            Assert.IsNotNull(plainText);
            Assert.AreEqual("test", plainText);
        }

        [TestMethod]
        public void GenerateKeyPairTest()
        {
            Tuple<string, string> keyPair = Sm2Utils.CreateKeyPair();
            Console.WriteLine("公钥是：{0}", keyPair.Item1);
            Console.WriteLine("密钥是：{0}", keyPair.Item2);
        }

        /// <summary>
        /// 生成密钥对-经过压缩
        /// </summary>
        [TestMethod]
        public void Sm2TestUtilKeyTest()
        {
            Tuple<string, string> key = Sm2TestUtil.CreateKeyPair();
            Console.WriteLine("公钥是：{0}", key.Item1);
            Console.WriteLine("密钥是：{0}", key.Item2);
        }

        /// <summary>
        /// 生成密钥对-未经压缩
        /// </summary>
        [TestMethod]
        public void Sm2TestUtilKeyNoCompressTest()
        {
            Tuple<string, string> key = Sm2TestUtil.CreateKeyPair(false);
            Console.WriteLine("公钥是：{0}", key.Item1);
            Console.WriteLine("密钥是：{0}", key.Item2);
            Assert.AreEqual("04", key.Item1.Substring(0, 2));
        }

        [TestMethod]
        public void Sm2TestUtilEncryptTest()
        {
            string publicKey = "02a02cee0eabf72d073b00f3685e120058ce983ad482d85f659e30779ce26b6dcb";
            string plainText = string.Join(",", Enumerable.Repeat(Guid.NewGuid(), 10000));
            string cipher = Sm2TestUtil.Encrypt(publicKey, plainText);
            Console.WriteLine(cipher);

            string primaryKey = "008125e15f2961d94876c1e8bccf0b14de6c2e8eda390a50d00e4999d25cdcdee2";
            string content = Sm2TestUtil.Decrypt(primaryKey, cipher);
            Console.WriteLine(content);

            Assert.AreEqual(content, plainText);
        }


    }
}