using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfdSharp;

namespace UnitTests
{
    [TestClass()]
    public class OfdDocumentTests
    {
        public OfdDocumentTests()
        {
            //Environment.SetEnvironmentVariable("Debug", "yes");
        }

        [TestMethod()]
        public void OfdDocumentTest()
        {
            OfdDocumentInfo documentInfo = new OfdDocumentInfo
            {
                Title = "测试001",
                Abstract = "开发测试生成ofd文档",
                Author = "丁品",
                DocUsage = OfdSharp.Primitives.Entry.DocUsage.Normal,
                Subject = "生成ofd示例文档",
                Creator = "unit test",
                CreatorVersion = "1.0",
                CustomDataList = Enumerable.Range(0, 10).Select((f, i) => new OfdSharp.Primitives.Entry.CustomData {Name = "custom-name-" + i, Value = "custom-value-" + i}).ToList(),
                Keywords = Enumerable.Range(0, 5).Select(f => Guid.NewGuid().ToString("N")).ToList(),
                Cover = null
            };
            OfdBuilder builder = new OfdBuilder();
            OfdDocument ofdDocument = builder.AddDocument(documentInfo);
            Assert.IsNotNull(ofdDocument);
        }

        [TestMethod()]
        public void AddPageTest()
        {
            OfdDocumentInfo documentInfo = new OfdDocumentInfo
            {
                Title = "测试001",
                //Abstract = "开发测试生成ofd文档",
                //Author = "丁品",
                //DocUsage = Primitives.Entry.DocUsage.Normal,
                //Subject = "生成ofd示例文档",
                // Creator = "unit test",
                //CreatorVersion = "1.0",
                //CustomDataList = Enumerable.Range(0, 10).Select((f, i) => new Primitives.Entry.CustomData { Name = "custom-name-" + i, Value = "custom-value-" + i }).ToList(),
                //Keywords = Enumerable.Range(0, 5).Select(f => Guid.NewGuid().ToString("N")).ToList(),
                Cover = null
            };
            OfdBuilder builder = new OfdBuilder();
            OfdDocument ofdDocument = builder.AddDocument(documentInfo);
            Assert.IsNotNull(ofdDocument);

            OfdPage ofdPage = ofdDocument.AddPage(PageSize.A4);

            Paragraph p1 = ofdPage.AddParagraph();
            Text t1 = p1.AddText("一切从写入简单文字开始");
            t1.FontSize = 10;
            t1.Color = OfdColor.Black;

            //t1.StrokeColor = OfdColor.Black;

            //ofdPage.AddText("一切从写入简单文字开始", 11);

            Paragraph p2 = ofdPage.AddParagraph();
            Text t2 = p2.AddText("这是第二行用黑体写的文字，ok？");
            t2.FontSize = 10.5f;
            t2.Font = OfdFont.Simhei;
            t2.Color = OfdColor.Black;

            byte[] content = builder.Save();
            Assert.IsNotNull(content);
            System.IO.File.WriteAllBytes(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "test-files", "simple-text.ofd"), content);
        }

        [TestMethod()]
        public void AddCustomTagTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddAttachmentTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddAnnotationInfoTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SignTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SaveTest()
        {
            Assert.Fail();
        }
    }
}