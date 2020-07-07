using System.Collections.Generic;
using System.Xml;

namespace OfdSharp.Core.Basic.Doc.Bookmark
{
    /// <summary>
    /// 文档的书签集，包含一组书签
    /// </summary>
    public class BookmarkCollect:OfdElement
    {
        public BookmarkCollect(XmlDocument xmlDocument) : base(xmlDocument, "Bookmarks")
        {
        }

        public IList<Bookmark> Bookmarks { get; set; }
    }
}
