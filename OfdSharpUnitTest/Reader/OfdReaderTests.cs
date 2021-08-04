using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfdSharp.Core.Invoice;
using OfdSharp.Reader;
using System.IO;
using System.Linq;

namespace OfdSharpUnitTest.Reader
{
    [TestClass]
    public class OfdReaderTests
    {
        [TestMethod]
        public void OfdReaderFileTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "test.ofd");
            FileStream fileStream = File.OpenRead(filePath);
            OfdReader reader = new OfdReader(fileStream);
            Assert.IsNotNull(reader);
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
            FileStream fileStream = System.IO.File.OpenWrite(filePath);
            Assert.IsTrue(fileStream.Length > 0);
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

            reader.Close();
        }

        [TestMethod]
        public void GetSignaturesByStreamTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "test.ofd");
            FileStream stream = File.OpenRead(filePath);
            OfdReader reader = new OfdReader(stream);
            Assert.IsNotNull(reader);

            string signatures = reader.GetSignatures();
            Assert.IsNotNull(signatures);

            reader.Close();
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

        [TestMethod]
        public void GetInvoiceInfoTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "test.ofd");
            OfdReader reader = new OfdReader(filePath);
            Assert.IsNotNull(reader);
            InvoiceInfo invoiceInfo = reader.GetInvoiceInfo();
            Assert.IsNotNull(invoiceInfo);
            Console.WriteLine(DateTime.Parse(invoiceInfo.IssueDate));
            Assert.AreEqual(9709m, decimal.Parse(invoiceInfo.TaxExclusiveTotalAmount));
        }

        [TestMethod]
        public void ReadHeadTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "test.ofd");
            byte[] content = File.ReadAllBytes(filePath);
            Console.WriteLine(string.Join(string.Empty, content.Take(2)));
        }
    }
}