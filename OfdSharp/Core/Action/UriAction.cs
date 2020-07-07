using System.Xml;

namespace OfdSharp.Core.Action
{
    public class UriAction : BaseAction
    {
        public UriAction(XmlDocument xmlDocument, string uri, string baseUri) : base(xmlDocument, "URI")
        {
            Base = baseUri;
            Uri = uri;

            Element.SetAttribute("URI", uri);
            Element.SetAttribute("Base", baseUri);
        }

        /// <summary>
        /// 设置 目标URI的位置
        /// </summary>
        public string Uri { get; }

        /// <summary>
        /// 设置 Base URI，用于相对地址
        /// </summary>
        public string Base { get; }


    }
}
