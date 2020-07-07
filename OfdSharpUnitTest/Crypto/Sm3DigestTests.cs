using Microsoft.VisualStudio.TestTools.UnitTesting;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;

namespace OfdSharpUnitTest.Crypto
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
    }
}