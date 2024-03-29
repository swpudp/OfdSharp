﻿using System.Xml.Serialization;

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
        /// 文件格式的版本号
        /// 固定值： 1.0
        /// </summary>
        public string Version => "1.0";

        /// <summary>
        /// 文件格式子集类型，取值为“OFD”，表明此文件符合本标准。
        /// </summary>
        public string DocType => "OFD";

        /// <summary>
        /// 文档正文
        /// </summary>
        [XmlElement("DocBody")]
        public DocBody DocBody { get; set; }
    }
}
