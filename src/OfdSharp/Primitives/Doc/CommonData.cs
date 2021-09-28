using OfdSharp.Primitives.PageObject;
using System.Collections.Generic;

namespace OfdSharp.Primitives.Doc
{
    /// <summary>
    /// 文档公共数据
    /// </summary>
    public class CommonData
    {
        /// <summary>
        /// 当前文档中所有对象使用标识的最大值，初始值为 0。
        /// 主要用于文档编辑，在向文档增加一个新对象时，需要分配一个新的标识符，新标识符取值宜为 MaxUnitID + 1，同时需要修改此 MaxUnitID值。
        /// </summary>
        public Id MaxUnitId { get; set; }

        /// <summary>
        /// 该文档页面区域的默认大小和位置
        /// </summary>
        public PageArea PageArea { get; set; }

        /// <summary>
        /// 公共资源序列，每个节点指向OFD包内的资源描述文档
        /// 字型和颜色空间等宜在文档资源文件中描述
        /// </summary>
        public Location PublicRes { get; set; }

        /// <summary>
        /// 文件资源序列，每个节点指向OFD包内的一个资源描述文件，绘制参数，多媒体，矢量图像等宜在文档资源文件中描述
        /// </summary>
        public Location DocumentRes { get; set; }

        /// <summary>
        /// todo 模板页序列
        /// 模板页序列，为一系列模板页的集合，模板页内容和结构和普通页相同
        /// </summary>
        public List<TemplatePage> TemplatePages { get; set; }

        /// <summary>
        /// 引用在资源文件中定义的颜色空间标识。如果此项不存在，采用RGB作为默认颜色空间
        /// </summary>
        public RefId ColorSpace { get; set; }

    }
}
