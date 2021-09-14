using System.Xml.Serialization;

namespace OfdSharp.Reader
{
    /// <summary>
    /// xml序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [XmlRoot("OFD")]
    internal class OfdRoot<T>
    {
        [XmlElement(Namespace = "http://www.ofdspec.org/2016")]
        public DocInfo<T> DocInfo { get; set; }
    }

    /// <summary>
    /// doc信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class DocInfo<T>
    {
        /// <summary>
        /// xml内容
        /// </summary>
        [XmlElement(Namespace = "http://www.ofdspec.org/2016")]
        public DocBody<T> DocBody { get; set; }
    }
}
