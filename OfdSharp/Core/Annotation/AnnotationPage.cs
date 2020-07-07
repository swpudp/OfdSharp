using System.Xml;

namespace OfdSharp.Core.Annotation
{
    public class AnnotationPage : OfdElement
    {
        public string PageId { get; }

        /// <summary>
        /// 指向包内的分页注释文件
        /// </summary>
        public string FileLoc { get; }

        public AnnotationPage(XmlDocument xmlDocument, string pageId, string fileLoc) : base(xmlDocument, "Page")
        {
            PageId = pageId;
            FileLoc = fileLoc;
        }
    }
}
