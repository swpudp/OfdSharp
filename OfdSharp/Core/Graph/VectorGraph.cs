using OfdSharp.Core.Basic.PageObject;
using OfdSharp.Primitives;

namespace OfdSharp.Core.Graph
{
    /// <summary>
    /// 矢量图像
    /// </summary>
    public class VectorGraph
    {
        /// <summary>
        /// 矢量图像的宽度,超出部分做裁剪处理
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// 矢量图像的高度,超出部分做裁剪处理
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// 缩略图,指向包内的图像文件
        /// </summary>
        public RefId Thumbnail { get; set; }

        /// <summary>
        /// 替换图像,用于高分辨率输出时将缩略图替换为此高分辨率的图像指向包内的图像文件
        /// </summary>
        public RefId Substitution { get; set; }

        /// <summary>
        /// 内容的矢量描述
        /// </summary>
        public PageBlock Content { get; set; }
    }
}
