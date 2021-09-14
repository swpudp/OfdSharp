using System.Xml.Serialization;

namespace OfdSharp.Reader
{
    /// <summary>
    /// 文档正文
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [XmlRoot("OFD")]
    public class DocBody<T>
    {
        /// <summary>
        /// 文档信息
        /// </summary>
        [XmlElement(Namespace = "http://www.ofdspec.org/2016")]
        public T DocInfo { get; set; }
    }
}
