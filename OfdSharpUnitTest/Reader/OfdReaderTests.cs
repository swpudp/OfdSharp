using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfdSharp.Reader;

namespace OfdSharpUnitTest.Reader
{
    [TestClass]
    public class OfdReaderTests
    {
        [TestMethod]
        public void OfdReaderFileTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "test.ofd");
            OfdReader reader = new OfdReader(filePath);
            Assert.IsNotNull(reader);
            Assert.AreEqual(reader.FileInfo.FullName, filePath);
        }

        [TestMethod]
        public void OfdReaderFileThrowExceptionTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Seal.esl");
            Assert.ThrowsException<FileNotFoundException>(() => new OfdReader(filePath));
        }

        [TestMethod]
        public void OfdReaderFileInfoTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "test.ofd");
            FileInfo fileInfo = new FileInfo(filePath);
            OfdReader reader = new OfdReader(fileInfo);
            Assert.IsNotNull(reader);
            Assert.AreEqual(reader.FileInfo.FullName, fileInfo.FullName);
        }

        [TestMethod]
        public void OfdReaderFileInfoThrowExceptionTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Seal.esl");
            FileInfo fileInfo = new FileInfo(filePath);
            Assert.ThrowsException<FileNotFoundException>(() => new OfdReader(fileInfo));
        }

        [TestMethod]
        public void GetBodyTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "test.ofd");
            OfdReader reader = new OfdReader(filePath);
            Assert.IsNotNull(reader);

            string body = reader.GetBody();
            Assert.IsNotNull(body);
        }

        [TestMethod]
        public void GetSignaturesTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "test.ofd");
            OfdReader reader = new OfdReader(filePath);
            Assert.IsNotNull(reader);

            string signatures = reader.GetSignatures();
            Assert.IsNotNull(signatures);
        }

        [TestMethod]
        public void GetDocTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void DisposeTest()
        {
            Assert.Fail();
        }
    }
}