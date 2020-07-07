using System.Collections.Generic;
using System.Xml;

namespace OfdSharp.Core.Basic.Res.Resources
{
    /// <summary>
    /// 包含了一组文档所有多媒体的描述
    /// </summary>
    public class MultiMediaCollect : OfdElement
    {
        public MultiMediaCollect(XmlDocument xmlDocument) : base(xmlDocument, "MultiMedias")
        {
        }

        /// <summary>
        /// 多媒体资源描述列表
        /// </summary>
        public IList<CtMultiMedia> MultiMedias { get; set; }
    }
}
