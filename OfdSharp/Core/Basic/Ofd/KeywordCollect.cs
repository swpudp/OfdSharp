using System.Collections.Generic;
using System.Xml;

namespace OfdSharp.Core.Basic.Ofd
{
    /// <summary>
    /// 关键词集合，每一个关键词用一个“Keyword”子节点来表达
    /// </summary>
    public class KeywordCollect : OfdElement
    {
        public KeywordCollect(XmlDocument xmlDocument) : base(xmlDocument, "Keywords")
        {
        }

        public IList<string> Keywords { get; set; }
    }
}
