using System.Collections.Generic;

namespace OfdSharp.Primitives.Pages.Object
{
    /// <summary>
    /// 图层
    /// </summary>
    public class Layer
    {
        /// <summary>
        /// 层类型描述，默认Body
        /// </summary>
        public LayerType Type { get; set; } = LayerType.Body;

        /// <summary>
        /// 图层的绘制参数,引用资源文件中定义的绘制参数标识
        /// </summary>
        public CtRefId DrawParam { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        public CtId Id { get; set; }

        /// <summary>
        /// 页块结构
        /// </summary>
        public List<PageBlock> PageBlocks { get; set; }
    }
}
