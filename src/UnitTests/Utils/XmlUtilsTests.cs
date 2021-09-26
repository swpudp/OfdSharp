using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfdSharp.Primitives;
using OfdSharp.Primitives.Ofd;
using OfdSharp.Reader;
using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using OfdSharp.Extensions;

namespace OfdSharp.Utils.Tests
{
    [TestClass()]
    public class XmlUtilsTests
    {
        [TestMethod()]
        public void DeserializeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SerializeTest()
        {
            IdTest idTest = new IdTest { Id = Id.NewId(), Key = Guid.NewGuid(), Value = "test" };
            Console.WriteLine(idTest.Id);
            string content = XmlUtils.Serialize(idTest);
            Assert.IsNotNull(content);
        }

        /// <summary>
        /// 测试xml读取
        /// </summary>
        [TestMethod]
        public void ReadXmlTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "OFD.xml");
            OfdRoot ofdRoot = XmlUtils.Deserialize<OfdRoot>(filePath);
            Assert.IsNotNull(ofdRoot);
            Assert.IsNotNull(ofdRoot.DocBody);
            Assert.IsNotNull(ofdRoot.DocBody.DocInfo);
            Assert.AreEqual("90baf370c9dc11e980000b7700000a77", ofdRoot.DocBody.DocInfo.DocId);
        }

        [TestMethod]
        public void LinqReadXmlTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "OFD.xml");
            XDocument document = XDocument.Load(filePath);
            XNamespace ns = document.Root.Name.Namespace;
            OfdRoot ofdRoot = new OfdRoot
            {
                DocBody = new Reader.DocBody
                {
                    DocInfo = new DocInfo
                    {
                        DocId = document.Descendants(XName.Get("DocID", ns.NamespaceName)).First().Value,
                        CustomDatas = document.Descendants(XName.Get("CustomData", ns.NamespaceName)).Select(f => new CustomData { Name = f.AttributeValueOrDefault<string>("Name"), Value = f.Value }).ToList()
                    },
                    DocRoot = new Location { Value = document.Descendants(XName.Get("DocRoot", ns.NamespaceName)).First().Value },
                    Signatures = new Location { Value = document.Descendants(XName.Get("Signatures", ns.NamespaceName)).First().Value }
                }
            };
            Assert.IsNotNull(ofdRoot);
            Assert.IsNotNull(ofdRoot.DocBody);
            Assert.IsNotNull(ofdRoot.DocBody.DocInfo);
            Assert.AreEqual("90baf370c9dc11e980000b7700000a77", ofdRoot.DocBody.DocInfo.DocId);
        }
    }

    [XmlType(AnonymousType = false, IncludeInSchema = false, Namespace = "http://www.ofdspec.org/2016", TypeName = "id-test")]
    public class IdTest
    {
        [XmlElement("id")]
        public Id Id { get; set; }

        public Guid Key { get; set; }

        public string Value { get; set; }
    }
}