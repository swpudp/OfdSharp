using OfdSharp.Primitives.Action;
using OfdSharp.Primitives.Doc.View;
using OfdSharp.Primitives.Outlines;
using System.Collections.Generic;
using OfdSharp.Primitives.Pages.Tree;

namespace OfdSharp.Primitives.Doc
{
    /// <summary>
    /// 文档根节点
    /// </summary>
    public class Document
    {
        /// <summary>
        /// 文档公共数据,定义了页面区域、公共资源等数据
        /// </summary>
        public CommonData CommonData { get; set; }

        /// <summary>
        /// 页树
        /// </summary>
        public List<PageNode> Pages { get; set; }

        /// <summary>
        /// 大纲
        /// </summary>
        public List<OutlineElem> Outlines { get; set; }

        /// <summary>
        /// 文档的权限声明
        /// </summary>
        public List<Permission.Permission> Permissions { get; set; }

        /// <summary>
        /// 文档关联的动作序列,当存在多个Action对象时,所有动作依次执行，事件类型应为DocumentOpen
        /// </summary>
        public List<CtAction> Actions { get; set; }

        /// <summary>
        /// 文档的视图首选项
        /// </summary>
        public ViewPreferences ViewPreferences { get; set; }

        /// <summary>
        /// 文档的书签集,包含一组书签
        /// </summary>
        public List<Bookmark.Bookmark> Bookmarks { get; set; }

        /// <summary>
        /// 指向附件列表文件。
        /// </summary>
        public List<Location> Attachments { get; set; }

        /// <summary>
        /// 指向注释列表文件
        /// </summary>
        public List<Location> Annotations { get; set; }

        /// <summary>
        /// 指向自定义标引列表文件
        /// </summary>
        public List<Location> CustomTags { get; set; }

        /// <summary>
        /// 指向扩展列表文件
        /// </summary>
        public List<Location> Extensions { get; set; }
    }
}
