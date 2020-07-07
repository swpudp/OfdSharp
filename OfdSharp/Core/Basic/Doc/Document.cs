using OfdSharp.Core.Action;
using OfdSharp.Core.Basic.Doc.Bookmark;
using OfdSharp.Core.Basic.Doc.View;
using OfdSharp.Core.Basic.Outlines;
using OfdSharp.Core.Basic.PageTree;
using System.Xml;
using OfdSharp.Core.Annotation;

namespace OfdSharp.Core.Basic.Doc
{
    public class Document : OfdElement
    {
        public Document(XmlDocument xmlDocument) : base(xmlDocument, "Document")
        {
        }

        /// <summary>
        /// 文档公共数据
        /// </summary>
        public CommonData CommonData { get; set; }

        /// <summary>
        /// 页树
        /// </summary>
        public Pages Pages { get; set; }

        public ActionCollect Actions { get; set; }

        public ViewPreferences ViewPreferences { get; set; }

        public BookmarkCollect Bookmarks { get; set; }

        public OutlineCollect Outlines { get; set; }

        public Annotations Annotations { get; set; }
    }
}
