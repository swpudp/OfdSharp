using System.Xml.Serialization;

namespace OfdSharp.Primitives.Signature
{
    public class CheckValue
    {
        [XmlText]
        public string Value { get; set; }
    }
}
