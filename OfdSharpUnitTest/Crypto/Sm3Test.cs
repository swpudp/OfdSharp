using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Utilities;

namespace OfdSharpUnitTest.Crypto
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
            SM3Digest sm3Digest = new SM3Digest();

            string ofdXml = Path.Combine(Directory.GetCurrentDirectory(), "Files", "OFD.xml");
            byte[] ofdXmlContent = File.ReadAllBytes(ofdXml);

            sm3Digest.BlockUpdate(ofdXmlContent, 0, ofdXmlContent.Length);
            byte[] output = new byte[32];
            sm3Digest.DoFinal(output, 0);

            byte[] expect = Convert.FromBase64String("/Ew+hIIgEQwmbW71cvPmIjkT9S7ABpRZTUPHtNBwhZg=");
            Assert.AreEqual(true, Arrays.AreEqual(output, expect));
        }
    }
}
