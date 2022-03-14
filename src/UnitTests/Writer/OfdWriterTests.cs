using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfdSharp.Writer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfdSharp.Writer.Tests
{
    [TestClass()]
    public class OfdWriterTests
    {
        [TestMethod()]
        public void WriteOfdRootTest()
        {
            OfdWriter writer = new OfdWriter();

            writer.WriteOfdRoot();
            writer.WriteDocument();
            writer.WriteDocumentRes();
            writer.WritePublicRes();
            writer.WritePages();

            byte[] content = writer.Flush();
            string fileName = Path.Combine(Directory.GetCurrentDirectory(), "test-root.ofd");
            File.WriteAllBytes(fileName, content);
            Assert.IsTrue(File.Exists(fileName));
        }
    }
}