using OfdSharp.Core.Basic.PageObj.Layer;
using System.Xml;

namespace OfdSharp.Core.Basic.PageObj
{
    /// <summary>
    /// 页面使用的模板页
    /// 模板页的内容和结构与普通页相同，定义在 CommonData
    /// 指定的 XML 文件中。一个页可以使用多个模板页。该节点
    /// 使用是通过 TemplateID 来引用具体模板，并通过 ZOrder
    /// 属性来控制模板在页面中的显示顺序。
    /// <remarks>在模板页的内容描述中该属性无效。</remarks>
    /// </summary>
    public class Template : OfdElement
    {
        public Template(XmlDocument xmlDocument) : base(xmlDocument, "Template")
        {
        }

        public string TemplateId { get; set; }

        public LayerType ZOrder { get; set; }
    }
}
