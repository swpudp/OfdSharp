using System.Collections.Generic;
using System.Xml;

namespace OfdSharp.Core.Basic.Ofd
{
    /// <summary>
    /// 主入口
    /// OFD.xml
    /// </summary>
    public class Ofd : OfdElement
    {
        /// <summary>
        /// 文件格式的版本号
        /// 固定值： 1.0
        /// </summary>
        public const string Version = "1.0";

        /// <summary>
        /// 文件格式子集类型，取值为“OFD”，表明此文件符合本标准。
        /// </summary>
        public const string DocType = "OFD";

        public Ofd(XmlDocument xmlDocument) : base(xmlDocument, "OFD")
        {
            Element.SetAttribute("Version", Version);
            Element.SetAttribute("DocType", DocType);
        }

        /// <summary>
        /// 获取所有文档入口
        /// </summary>
        public IList<DocBody> DocBodies { get; set; }
    }
}
