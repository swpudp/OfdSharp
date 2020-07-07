using System;
using System.Xml;

namespace OfdSharp.Core.Version
{
    /// <summary>
    /// 版本
    /// </summary>
    public class DocVersion : OfdElement
    {
        public DocVersion(XmlDocument xmlDocument) : base(xmlDocument, "DocVersion")
        {
        }

        /// <summary>
        /// 版本标识符
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 该文件适用的格式版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 版本名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// 该版本的入口文件
        /// </summary>
        public string DocRoot { get; set; }

        /// <summary>
        /// 版本包含的文件列表
        /// </summary>
        public FileCollect FileList { get; set; }
    }
}
