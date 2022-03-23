﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfdSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfdSharp.Tests
{
    [TestClass()]
    public class OfdDocumentTests
    {
        [TestMethod()]
        public void OfdDocumentTest()
        {
            OfdDocumentInfo documentInfo = new OfdDocumentInfo
            {
                Title = "测试001",
                Abstract = "开发测试生成ofd文档",
                Author = "丁品",
                DocUsage = Primitives.Entry.DocUsage.Normal,
                Subject = "生成ofd示例文档",
                Creator = "unit test",
                CreatorVersion = "1.0",
                CustomDataList = Enumerable.Range(0, 10).Select((f, i) => new Primitives.Entry.CustomData { Name = "custom-name-" + i, Value = "custom-value-" + i }).ToList(),
                Keywords = Enumerable.Range(0, 5).Select(f => Guid.NewGuid().ToString("N")).ToList(),
                Cover = null
            };
            OfdDocument ofdDocument = new OfdDocument(documentInfo);
            Assert.IsNotNull(ofdDocument);
        }

        [TestMethod()]
        public void AddPageTest()
        {
            OfdDocumentInfo documentInfo = new OfdDocumentInfo
            {
                Title = "测试001",
                Abstract = "开发测试生成ofd文档",
                Author = "丁品",
                DocUsage = Primitives.Entry.DocUsage.Normal,
                Subject = "生成ofd示例文档",
                Creator = "unit test",
                CreatorVersion = "1.0",
                CustomDataList = Enumerable.Range(0, 10).Select((f, i) => new Primitives.Entry.CustomData { Name = "custom-name-" + i, Value = "custom-value-" + i }).ToList(),
                Keywords = Enumerable.Range(0, 5).Select(f => Guid.NewGuid().ToString("N")).ToList(),
                Cover = null
            };
            OfdDocument ofdDocument = new OfdDocument(documentInfo);
            Assert.IsNotNull(ofdDocument);

            OfdPage ofdPage = ofdDocument.AddPage(PageSize.A4);
            ofdPage.AddText("一切从写入简单文字开始");

            byte[] content = ofdDocument.Save();
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