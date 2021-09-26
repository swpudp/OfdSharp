namespace OfdSharp.Primitives.Resources
{
    /// <summary>
    /// 多媒体
    /// </summary>
    public class CtMultiMedia
    {
        /// <summary>
        /// 多媒体类型。支持位图图像、视频、音频三种多媒体类型
        /// </summary>
        public MediaType Type { get; set; }

        /// <summary>
        /// 资源的格式。支持BMP、JPEG、PNG、TIFF及AVS等格式,其中TIFF格式不支持多页
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 指向OFD包内的多媒体文件的位置
        /// </summary>
        public Location MediaFile { get; set; }
    }
}
