using System.Xml;

namespace OfdSharp.Core.Signatures
{
    /// <summary>
    /// 创建签名时所用的签章组件提供者信息
    /// </summary>
    public class Provider : OfdElement
    {
        public Provider(XmlDocument xmlDocument) : base(xmlDocument, "Provider")
        {
        }

        /// <summary>
        /// 创建签名时所用的签章组件提供者信息
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// 创建签名时所使用的签章组件的版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 创建签名时所使用的签章组件的制造商
        /// </summary>
        public string Company { get; set; }
    }
}
