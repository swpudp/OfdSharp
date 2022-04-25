using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfdSharp.Extensions;
using OfdSharp.Primitives;
using OfdSharp.Primitives.Entry;
using OfdSharp.Primitives.Fonts;
using OfdSharp.Primitives.Pages.Description.ColorSpace;
using OfdSharp.Primitives.Resources;
using OfdSharp.Utils;

namespace UnitTests.Utils
{
    [TestClass]
    public class XmlUtilsTests
    {
        [TestMethod]
        public void DeserializeTest()
        {
            Assert.Fail();
        }

        [TestMethod]
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
            Assert.IsNotNull(ofdRoot.DocBodyList);
            Assert.IsNotNull(ofdRoot.DocBodyList[0].DocInfo);
            Assert.AreEqual("90baf370c9dc11e980000b7700000a77", ofdRoot.DocBodyList[0].DocInfo.DocId);
        }

        [TestMethod]
        public void LinqReadXmlTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Doc_0", "DocumentRes.xml");
            XDocument xDocument = XDocument.Load(filePath);
            var documentResource = new DocumentResource
            {
                ColorSpaces = xDocument.GetDescendants("ColorSpace").Select(f => new CtColorSpace
                {
                    Type = f.AttributeValueOrDefault("Type").ParseEnum<ColorSpaceType>(),
                    BitsPerComponent = f.AttributeValueOrDefault("BitsPerComponent").ParseEnum<BitsPerComponent>(),
                    Id = new Id(f.AttributeValueOrDefault("ID"))
                }).ToList(),
                Fonts = xDocument.GetDescendants("Font").Select(f => new CtFont
                {
                    Id = new Id(f.AttributeValueOrDefault("ID")),
                    FontName = f.AttributeValueOrDefault("FontName"),
                    FamilyName = f.AttributeValueOrDefault("FamilyName")
                }).ToList(),
                MultiMedias = xDocument.GetDescendants("MultiMedia").Select(f => new CtMultiMedia
                {
                    Type = f.AttributeValueOrDefault("Type").ParseEnum<MediaType>(),
                    Id = new Id(f.AttributeValueOrDefault("ID")),
                    MediaFile = new Location(f.ElementValue("MediaFile"))
                }).ToList()
            };
            Assert.IsNotNull(documentResource);
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