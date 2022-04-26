using OfdSharp.Primitives.Fonts;
using System.Collections.Generic;
using OfdSharp.Primitives.Composite;
using OfdSharp.Primitives.Pages.Description.ColorSpace;
using OfdSharp.Primitives.Pages.Description.DrawParam;

namespace OfdSharp.Primitives.Resources
{
    /// <summary>
    /// 资源文件
    /// </summary>
    public class DocumentResource
    {
        /// <summary>
        /// 定义此资源文件的通用数据存储路径,BaseLoc属性的意义在于明确资源文件存储的位置,
        /// 比如R1.xml中可以指定BaseLoc为“./Res”,表明该资源文件中所有数据文件的默认存储位置在当前路径的Res目录下
        /// </summary>
        public CtLocation BaseLoc { get; set; }

        /// <summary>
        /// 包含了一组颜色空间的描述
        /// </summary>
        public List<CtColorSpace> ColorSpaces { get; set; }

        /// <summary>
        /// 绘制参数描述序列
        /// </summary>
        public List<CtDrawParam> DrawParams { get; set; }

        /// <summary>
        /// 包含了一组文档所用字型的描述
        /// </summary>
        public List<CtFont> Fonts { get; set; }

        /// <summary>
        /// 多媒体资源描述列表
        /// </summary>
        public List<CtMultiMedia> MultiMedias { get; set; }

        /// <summary>
        /// 包含了一组矢量图像(被复合图元对象所引用)的描述
        /// </summary>
        public List<VectorGraph> CompositeGraphicUnits { get; set; }
    }
}
