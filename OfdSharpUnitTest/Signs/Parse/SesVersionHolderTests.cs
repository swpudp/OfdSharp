using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfdSharp.Ses.Parse;

namespace OfdSharpUnitTest.Signs.Parse
{
    [TestClass]
    public class SesVersionHolderTests
    {
        [TestMethod]
        public void SesVersionHolderTest()
        {
            byte[] sealBytes = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Files", "Doc_0", "Signs", "Sign_0", "Seal.esl"));
            SesVersionHolder holder = VersionParser.ParseSealVersion(sealBytes);
            Assert.AreEqual(SesVersion.V4, holder.Version);
        }
    }
}