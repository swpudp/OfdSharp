using System.Xml;

namespace OfdSharp.Core.Basic.Res
{
    /// <summary>
    /// 多媒体
    /// </summary>
    public class CtMultiMedia : OfdElement
    {
        public CtMultiMedia(XmlDocument xmlDocument) : base(xmlDocument, "MultiMedia")
        {
        }

        public string Id { get; set; }

        /// <summary>
        /// 多媒体类型
        /// </summary>
        public MediaType Type { get; set; }

        /// <summary>
        /// 资源的格式
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 指向 OFD包内的多媒体文件位置
        /// </summary>
        public string MediaFile { get; set; }
    }
}
