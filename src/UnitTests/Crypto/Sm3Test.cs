using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenSsl.Crypto.Utility;
using Org.BouncyCastle.Utilities;

namespace UnitTests.Crypto
{
    /// <summary>
    /// sm3加密测试
    /// </summary>
    [TestClass]
    public class Sm3Test
    {
        /// <summary>
        /// 获取摘要测试
        /// </summary>
        [TestMethod]
        public void DigestTest()
        {
            string ofdXml = Path.Combine(Directory.GetCurrentDirectory(), "Files", "test", "OFD.xml");
            byte[] ofdXmlContent = File.ReadAllBytes(ofdXml);
            byte[] output = DigestUtils.Sm3(ofdXmlContent);
            byte[] expect = Convert.FromBase64String("/Ew+hIIgEQwmbW71cvPmIjkT9S7ABpRZTUPHtNBwhZg=");
            Assert.AreEqual(true, Arrays.AreEqual(output, expect));
        }
    }
}