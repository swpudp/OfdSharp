using System;
using System.Xml;

namespace OfdSharp.Core.Attachment
{
    /// <summary>
    /// 附件
    /// </summary>
    public class Attachment : OfdElement
    {
        public Attachment(XmlDocument xmlDocument) : base(xmlDocument, "Attachment")
        {
        }

        /// <summary>
        /// 附件标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 附件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 附件格式
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModDate { get; set; }

        /// <summary>
        /// 附件大小
        /// </summary>
        public double Size { get; set; }

        /// <summary>
        /// 附件是否可见
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// 附件用途
        /// </summary>
        public string Usage { get; set; }

        /// <summary>
        /// 附件内容在包内的路径
        /// </summary>
        public string FileLoc { get; set; }
    }
}
