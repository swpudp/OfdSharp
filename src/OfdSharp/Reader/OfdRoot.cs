using OfdSharp.Primitives;
using System.Xml.Serialization;

namespace OfdSharp.Reader
{
    /// <summary>
    /// xml序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [XmlRoot(Namespace = "http://www.ofdspec.org/2016", ElementName = "OFD")]
    public class OfdRoot
    {
        /// <summary>
        /// 文档正文
        /// </summary>
        [XmlElement("DocBody")]
        public DocBody DocBody { get; set; }
    }
}
