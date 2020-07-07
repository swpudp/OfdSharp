using System.Xml;

namespace OfdSharp.Core.Action
{
    /// <summary>
    /// 跳转的目的书签
    /// 表 53 跳转动作属性
    /// </summary>
    public class BookmarkAction : BaseAction
    {
        /// <summary>
        /// 目标书签的名称，引用文档书签中的名称
        /// </summary>
        public string Name { get; }

        public BookmarkAction(XmlDocument xmlDocument, string name) : base(xmlDocument, "Bookmark")
        {
            Name = name;
            Element.SetAttribute("Name", name);
        }
    }
}
