using System.Xml;

namespace OfdSharp.Core.Version
{
    /// <summary>
    /// 文件列表文件描述
    /// </summary>
    public class File : OfdElement
    {
        public File(XmlDocument xmlDocument) : base(xmlDocument, "File")
        {
        }

        /// <summary>
        /// 文件列表文件标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 文件列表文件描述
        /// </summary>
        public string FullName { get; set; }
    }
}
