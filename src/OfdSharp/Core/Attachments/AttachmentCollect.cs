using System.Collections.Generic;

namespace OfdSharp.Core.Attachments
{
    /// <summary>
    /// 附件列表，附件列表文件的入口点在 7.5 文档根节点中定义。
    /// 一个OFD文件可以定义多个附件，附件列表结构如图 91 所示。
    /// </summary>
    public class AttachmentCollect 
    {
        /// <summary>
        /// 附件列表根节点
        /// </summary>
        public IList<Attachment> Attachments { get; set; }
    }
}
