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
            OfdValidator validator = new OfdValidator();
            OfdReader reader = new OfdReader(filePath);
            VerifyResult verifyResult = validator.Validate(reader);
            Assert.IsNotNull(verifyResult);
            Assert.AreEqual(VerifyResult.Success, verifyResult);
        }
    }
}