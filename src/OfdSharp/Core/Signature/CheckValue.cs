using System.Xml.Serialization;

namespace OfdSharp.Core.Signature
{
    public class CheckValue
    {
        [XmlText]
        public string Value { get; set; }
    }
}
