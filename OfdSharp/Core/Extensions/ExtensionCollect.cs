using System.Collections.Generic;
using System.Xml;

namespace OfdSharp.Core.Extensions
{
    /// <summary>
    /// 扩展信息集合
    /// </summary>
    public class ExtensionCollect : OfdElement
    {
        public ExtensionCollect(XmlDocument xmlDocument) : base(xmlDocument, "Extensions")
        {
        }

        /// <summary>
        /// 扩展信息节点
        /// </summary>
        public IList<Extension> Extensions { get; set; }
    }
}
