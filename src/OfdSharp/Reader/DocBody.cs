using OfdSharp.Primitives;
using OfdSharp.Primitives.Ofd;
using System.Xml.Serialization;

namespace OfdSharp.Reader
{
    /// <summary>
    /// 文档正文
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DocBody
    {
        /// <summary>
        /// 文档信息
        /// </summary>
        [XmlElement("DocInfo")]
        public DocInfo DocInfo { get; set; }

        /// <summary>
        /// 文档入口文件
        /// </summary>
        [XmlElement("DocRoot")]
        public Location DocRoot { get; set; }

        /// <summary>
        /// 签名入口
        /// </summary>
        [XmlElement("Signatures")]
        public Location Signatures { get; set; }
    }
}
