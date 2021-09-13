using OfdSharp.Core.CompositeObj;
using OfdSharp.Core.PageDescription.ColorSpace;
using OfdSharp.Core.PageDescription.DrawParam;
using OfdSharp.Core.Text.Font;
using OfdSharp.Primitives;
using System.Collections.Generic;

namespace OfdSharp.Core.Basic.Resources
{
    /// <summary>
    /// 资源文件
    /// </summary>
    public class Resource
    {
        /// <summary>
        /// 定义此资源文件的通用数据存储路径,BaseLoc属性的意义在于明确资源文件存储的位置,
        /// 比如R1.xml中可以指定BaseLoc为“./Res”,表明该资源文件中所有数据文件的默认存储位置在当前路径的Res目录下
        /// </summary>
        public Location BaseLoc { get; set; }

        /// <summary>
        /// 包含了一组颜色空间的描述
        /// </summary>
        public IList<CtColorSpace> ColorSpaces { get; set; }

        /// <summary>
        /// 绘制参数描述序列
        /// </summary>
        public IList<CtDrawParam> DrawParams { get; set; }

        /// <summary>
        /// 包含了一组文档所用字型的描述
        /// </summary>
        public IList<CtFont> Fonts { get; set; }

        /// <summary>
        /// 多媒体资源描述列表
        /// </summary>
        public IList<CtMultiMedia> MultiMedias { get; set; }

        /// <summary>
        /// 包含了一组矢量图像(被复合图元对象所引用)的描述
        /// </summary>
        public IList<VectorGraph> CompositeGraphicUnits { get; set; }
    }
}
