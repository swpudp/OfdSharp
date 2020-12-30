using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfdSharp.Reader;
using OfdSharp.Verify;
using System.IO;

namespace OfdSharpUnitTest.Verify
{
    /// <summary>
    /// OFD电子签名验证引擎测试
    /// </summary>
    [TestClass]
    public class OfdValidatorTests
    {
        /// <summary>
        /// OFD电子签名验证
        /// </summary>
        [TestMethod]
        public void ExeValidateTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "test.ofd");
            OfdReader reader = new OfdReader(filePath);
            VerifyResult verifyResult = OfdValidator.Validate(reader);
            Assert.IsNotNull(verifyResult);
            Assert.AreEqual(VerifyResult.Success, verifyResult);
        }

        /// <summary>
        /// OFD电子签名验证
        /// </summary>
        [TestMethod]
        public void ExeValidateInvalidFileTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "test1.ofd");
            OfdReader reader = new OfdReader(filePath);
            VerifyResult verifyResult = OfdValidator.Validate(reader);
            Assert.IsNotNull(verifyResult);
            Assert.AreEqual(VerifyResult.SealNotFound, verifyResult);
        }
    }
}