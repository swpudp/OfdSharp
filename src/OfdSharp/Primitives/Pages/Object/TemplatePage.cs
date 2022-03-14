namespace OfdSharp.Primitives.Pages.Object
{
    /// <summary>
    /// 页对象
    /// ————《GB/T 33190-2016》 图 14
    /// </summary>
    public class TemplatePage
    {
        /// <summary>
        /// 模板页的标识,不能与已有标识重复
        /// </summary>
        public Id Id { get; set; }

        /// <summary>
        /// 模板页名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ZOrder
        /// 控制模板在页面中的呈现顺序,其类型描述和呈现顺序与Layer中Type的描述和处理一致。
        /// 如果多个图层的此属性相同,则应根据其出现的顺序来显示,先出现者先绘制
        /// 默认值为Background
        /// </summary>
        public LayerType ZOrder { get; set; }

        /// <summary>
        /// 指向模板页内容描述文件
        /// </summary>
        public Location BaseLoc { get; set; }
    }
}
