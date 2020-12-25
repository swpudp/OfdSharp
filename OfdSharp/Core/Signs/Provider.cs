using System.Xml.Serialization;

namespace OfdSharp.Core.Signs
{
    /// <summary>
    /// 创建签名时所用的签章组件提供者信息
    /// </summary>
    public class Provider
    {
        /// <summary>
        /// 创建签名时所用的签章组件提供者信息
        /// </summary>
        [XmlAttribute]
        public string ProviderName { get; set; }

        /// <summary>
        /// 创建签名时所使用的签章组件的版本
        /// </summary>
        [XmlAttribute]
        public string Version { get; set; }

        /// <summary>
        /// 创建签名时所使用的签章组件的制造商
        /// </summary>
        [XmlAttribute]
        public string Company { get; set; }
    }
}
