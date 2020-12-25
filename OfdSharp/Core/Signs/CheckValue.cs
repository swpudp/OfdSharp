using System.Xml.Serialization;

namespace OfdSharp.Core.Signs
{
    public class CheckValue
    {
        [XmlText]
        public string Value { get; set; }
    }
}
