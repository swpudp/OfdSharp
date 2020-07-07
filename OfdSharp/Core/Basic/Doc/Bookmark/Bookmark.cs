using OfdSharp.Core.Action;
using System.Xml;

namespace OfdSharp.Core.Basic.Doc.Bookmark
{
    /// <summary>
    /// 本标准支持书签，可以将常用位置定义为书签，文档可以包含一组书签。
    /// </summary>
    public class Bookmark : OfdElement
    {
        public Bookmark(XmlDocument xmlDocument, string name, DestAction dest) : base(xmlDocument, "Bookmark")
        {
            Dest = dest;
            Element.SetAttribute("Name", name);
        }

        /// <summary>
        /// 目标区域
        /// </summary>
        public DestAction Dest { get; }

        /// <summary>
        /// 书签名称
        /// </summary>
        public string Name { get; }
    }
}
