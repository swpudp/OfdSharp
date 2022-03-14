using System.Collections.Generic;
using OfdSharp.Primitives.Action;
using OfdSharp.Primitives.Doc;

namespace OfdSharp.Primitives.Pages.Object
{
    /// <summary>
    /// 页对象
    /// 页对象支持模板页描述，每一页经常要重复显示的内容可统一在模板页中描述，
    /// 文档可以包含多个模板页。通过使用模板页可以使重复显示的内容不必出现在
    /// 描述每一页的页面描述内容中，而只需通过 Template 节点进行应用。
    /// </summary>
    public class PageObject
    {
        /// <summary>
        /// 页面区域的大小和位置，仅对该页面有效。
        /// </summary>
        public PageArea Area { get; set; }

        /// <summary>
        /// 页面使用的模板页
        /// </summary>
        public Template Template { get; set; }

        /// <summary>
        /// 指向该页使用的资源文件
        /// </summary>
        public Location PageRes { get; set; }

        /// <summary>
        /// 页面内容描述，该节点不存在时，标识空白页
        /// </summary>
        public Content Content { get; set; }

        /// <summary>
        /// 与页面关联的动作序列
        /// 当存在多个 Action对象时，所有动作依次执行。
        /// 动作列表的动作与页面关联，事件类型为 PO（页面打开，见表 52 事件类型）
        /// </summary>
        public List<CtAction> Actions { get; set; }
    }
}
