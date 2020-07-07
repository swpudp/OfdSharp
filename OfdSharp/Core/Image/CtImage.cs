using System.Xml;

namespace OfdSharp.Core.Image
{
    /// <summary>
    /// 图像
    /// 10 图像 图 57 表 43
    /// </summary>
    public class CtImage : OfdElement
    {
        public CtImage(XmlDocument xmlDocument) : base(xmlDocument, "Image")
        {
        }

        /// <summary>
        /// 引用资源文件的定义多媒体的标识
        /// </summary>
        public string ResourceId { get; set; }

        /// <summary>
        /// 可替换图像
        /// 引用资源文件中定义的多媒体的标识，由于某些情况
        /// 如高分辨率输出进行图像替换
        /// </summary>
        public string Substitution { get; set; }

        /// <summary>
        /// 图像蒙版
        /// 引用资源文件中定义的多媒体的标识，用作蒙板的图像应是与 ResourceID 指向的图像相同大小的二值图
        /// </summary>
        public string ImageMask { get; set; }

        /// <summary>
        /// 图像边框
        /// </summary>
        public Border Border { get; set; }

        /// <summary>
        /// 构造图片对象
        /// </summary>
        public string ImageObject { get; set; }
    }
}
