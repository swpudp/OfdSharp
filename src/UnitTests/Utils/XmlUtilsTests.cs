using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfdSharp.Primitives;
using System;
using System.Xml.Serialization;

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