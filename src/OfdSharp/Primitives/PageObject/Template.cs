namespace OfdSharp.Primitives.PageObject
{
    /// <summary>
    /// 页面使用的模板页
    /// 模板页的内容和结构与普通页相同，定义在 CommonData
    /// 指定的 XML 文件中。一个页可以使用多个模板页。该节点
    /// 使用是通过 TemplateID 来引用具体模板，并通过 ZOrder
    /// 属性来控制模板在页面中的显示顺序。
    /// <remarks>在模板页的内容描述中该属性无效。</remarks>
    /// </summary>
    public class Template
    {
        /// <summary>
        /// 引用在文档公用数据（CommonData）中定义的模板页标识
        /// </summary>
        public string TemplateId { get; set; }

        /// <summary>
        /// ZOrder
        /// 控制模板在页面中的呈现顺序,其类型描述和呈现顺序与Layer中Type的描述和处理一致。
        /// 如果多个图层的此属性相同,则应根据其出现的顺序来显示,先出现者先绘制
        /// 默认值为Background
        /// </summary>
        public LayerType ZOrder { get; set; }
    }
}
