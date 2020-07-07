using System.Collections.Generic;
using System.Xml;

namespace OfdSharp.Core.Version
{
    /// <summary>
    /// 一个OFD文档可能有多个版本
    /// 版本序列
    /// </summary>
    public class VersionCollect : OfdElement
    {
        public VersionCollect(XmlDocument xmlDocument) : base(xmlDocument, "Versions")
        {
        }

        /// <summary>
        /// 版本描述入口列表
        /// </summary>
        public IList<Version> Versions { get; set; }
    }
}
