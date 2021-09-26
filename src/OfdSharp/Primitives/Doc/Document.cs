using OfdSharp.Primitives.Action;
using OfdSharp.Primitives.Doc.View;
using OfdSharp.Primitives.Outlines;
using OfdSharp.Primitives.PageTree;
using System.Collections.Generic;

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
        public IList<Page> Pages { get; set; }

        /// <summary>
        /// 大纲
        /// </summary>
        public IList<OutlineElem> Outlines { get; set; }

        /// <summary>
        /// 文档的权限声明
        /// </summary>
        public IList<Permission.Permission> Permissions { get; set; }

        /// <summary>
        /// 文档关联的动作序列,当存在多个Action对象时,所有动作依次执行，事件类型应为DocumentOpen
        /// </summary>
        public IList<CtAction> Actions { get; set; }

        /// <summary>
        /// 文档的视图首选项
        /// </summary>
        public ViewPreferences ViewPreferences { get; set; }

        /// <summary>
        /// 文档的书签集,包含一组书签
        /// </summary>
        public IList<Bookmark.Bookmark> Bookmarks { get; set; }

        /// <summary>
        /// 指向附件列表文件。
        /// </summary>
        public IList<Location> Attachments { get; set; }

        /// <summary>
        /// 指向注释列表文件
        /// </summary>
        public IList<Location> Annotations { get; set; }

        /// <summary>
        /// 指向自定义标引列表文件
        /// </summary>
        public IList<Location> CustomTags { get; set; }

        /// <summary>
        /// 指向扩展列表文件
        /// </summary>
        public IList<Location> Extensions { get; set; }
    }
}
