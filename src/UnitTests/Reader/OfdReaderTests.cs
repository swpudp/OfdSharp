using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfdSharp.Primitives.Invoice;
using OfdSharp.Reader;
using System.IO;
using System.Linq;
using OfdSharp.Primitives.Doc;
using OfdSharp.Primitives;
using System.Collections.Generic;
using OfdSharp.Primitives.PageObject;
using OfdSharp.Primitives.Resources;
using OfdSharp.Primitives.PageDescription.ColorSpace;

namespace UnitTests.Reader
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
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "test.ofd");
            OfdReader reader = new OfdReader(filePath);
            string doc = reader.GetDoc(0);
            Assert.AreEqual("Doc_0", doc);
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

        /// <summary>
        /// 测试读取OFD.xml
        /// </summary>
        [TestMethod]
        public void GetOfdRootTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "test.ofd");
            OfdReader reader = new OfdReader(filePath);
            Assert.IsNotNull(reader);
            OfdRoot ofdRoot = reader.GetOfdRoot();
            Assert.IsNotNull(ofdRoot);
            Assert.IsNotNull(ofdRoot.DocBody);
            Assert.IsNotNull(ofdRoot.DocBody.DocInfo);
            Assert.AreEqual("90baf370c9dc11e980000b7700000a77", ofdRoot.DocBody.DocInfo.DocId);
        }

        /// <summary>
        /// 测试读取Document.xml
        /// </summary>
        [TestMethod]
        public void GetDocumentTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "test.ofd");
            OfdReader reader = new OfdReader(filePath);
            Assert.IsNotNull(reader);
            Document document = reader.GetDocument();
            Assert.IsNotNull(document);
            Assert.IsNotNull(document.CommonData);
            Assert.AreEqual(new Id("2000"), document.CommonData.MaxUnitId);
            Assert.AreEqual(Box.Parse("0 0 210 297"), document.CommonData.PageArea.Physical);
            Assert.AreEqual("DocumentRes.xml", document.CommonData.PublicRes.Value);
            Assert.IsNotNull(document.CommonData.TemplatePages);
            Assert.IsTrue(document.CommonData.TemplatePages.All(f => new List<Id> { new Id("187"), new Id("100") }.Contains(f.Id)));
            Assert.IsTrue(document.CommonData.TemplatePages.All(f => new List<string> { "TPLS/TPL_0/Content.xml", "TPLS/TPL_1/Content.xml" }.Contains(f.BaseLoc.Value)));
            Assert.IsTrue(document.CommonData.TemplatePages.All(f => new List<LayerType> { LayerType.Background }.Contains(f.ZOrder)));
            Assert.IsTrue(document.Pages.All(f => new List<Id> { new Id("1"), new Id("331"), new Id("547"), new Id("763"), new Id("979") }.Contains(f.Id)));
            Assert.IsTrue(document.Pages.All(f => new List<string> { "Pages/Page_0/Content.xml", "Pages/Page_1/Content.xml", "Pages/Page_2/Content.xml", "Pages/Page_3/Content.xml", "Pages/Page_4/Content.xml" }.Contains(f.BaseLoc.Value)));
            Assert.AreEqual("Annotations.xml", document.Annotations.First().Value);
            Assert.AreEqual("Attachs/Attachments.xml", document.Attachments.First().Value);
            Assert.AreEqual("Tags/CustomTags.xml", document.CustomTags.First().Value);
        }

        /// <summary>
        /// 测试读取DocumentRes.xml
        /// </summary>
        [TestMethod]
        public void GetDocumentResourceTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "test1.ofd");
            OfdReader reader = new OfdReader(filePath);
            Assert.IsNotNull(reader);
            DocumentResource documentRes = reader.GetDocumentResource();
            Assert.IsNotNull(documentRes);
            Assert.IsNotNull(documentRes.BaseLoc);
            Assert.IsNotNull(documentRes.ColorSpaces);
            Assert.IsNotNull(documentRes.Fonts);
            Assert.IsNotNull(documentRes.MultiMedias);
            Assert.AreEqual("Res", documentRes.BaseLoc.Value);

            Assert.IsTrue(documentRes.DrawParams.All(f => new List<Id> { new Id("4") }.Contains(f.Id)));
            Assert.IsTrue(documentRes.DrawParams.All(f => new List<OfdSharp.Primitives.Array> { OfdSharp.Primitives.Array.Parse("156 82 35") }.Contains(f.FillColor.Value)));
            Assert.IsTrue(documentRes.DrawParams.All(f => new List<OfdSharp.Primitives.Array> { OfdSharp.Primitives.Array.Parse("156 82 35") }.Contains(f.StrokeColor.Value)));

            Assert.IsTrue(documentRes.MultiMedias.All(f => new List<Id> { new Id("78") }.Contains(f.Id)));
            Assert.IsTrue(documentRes.MultiMedias.All(f => new List<MediaType> { MediaType.Image }.Contains(f.Type)));
            Assert.IsTrue(documentRes.MultiMedias.All(f => new List<string> { "image_78.jb2" }.Contains(f.MediaFile.Value)));
        }

        /// <summary>
        /// 测试读取PublicRes.xml
        /// </summary>
        [TestMethod]
        public void GetPublicResourceTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "test.ofd");
            OfdReader reader = new OfdReader(filePath);
            Assert.IsNotNull(reader);
            DocumentResource documentRes = reader.GetPublicResource();
            Assert.IsNotNull(documentRes);
            Assert.IsNotNull(documentRes.BaseLoc);
            Assert.IsNotNull(documentRes.ColorSpaces);
            Assert.IsNotNull(documentRes.Fonts);
            Assert.IsNotNull(documentRes.MultiMedias);
            Assert.AreEqual("Res", documentRes.BaseLoc.Value);

            Assert.IsTrue(documentRes.ColorSpaces.All(f => new List<Id> { new Id("190") }.Contains(f.Id)));
            Assert.IsTrue(documentRes.ColorSpaces.All(f => new List<ColorSpaceType> { ColorSpaceType.RGB }.Contains(f.Type)));
            Assert.IsTrue(documentRes.ColorSpaces.All(f => new List<BitsPerComponent> { BitsPerComponent.Bit8 }.Contains(f.BitsPerComponent)));

            Assert.IsTrue(documentRes.Fonts.All(f => new List<Id> { new Id("28"), new Id("85"), new Id("88") }.Contains(f.Id)));
            Assert.IsTrue(documentRes.Fonts.All(f => new List<string> { "楷体", "宋体", "Courier New" }.Contains(f.FontName)));
            Assert.IsTrue(documentRes.Fonts.All(f => new List<string> { "楷体", "宋体", "Courier New" }.Contains(f.FamilyName)));

            Assert.IsTrue(documentRes.MultiMedias.All(f => new List<Id> { new Id("311") }.Contains(f.Id)));
            Assert.IsTrue(documentRes.MultiMedias.All(f => new List<MediaType> { MediaType.Image }.Contains(f.Type)));
            Assert.IsTrue(documentRes.MultiMedias.All(f => new List<string> { "image_0.png" }.Contains(f.MediaFile.Value)));
        }
    }
}