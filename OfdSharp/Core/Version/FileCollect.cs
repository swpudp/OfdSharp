using System.Collections.Generic;
using System.Xml;

namespace OfdSharp.Core.Version
{
    /// <summary>
    /// 版本包含的文件列表
    /// </summary>
    public class FileCollect : OfdElement
    {
        public FileCollect(XmlDocument xmlDocument) : base(xmlDocument, "FileList")
        {
        }

        /// <summary>
        /// 文件列表文件描述
        /// </summary>
        public IList<File> Files { get; set; }
    }
}
