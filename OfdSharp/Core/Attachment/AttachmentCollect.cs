using System.Collections.Generic;
using System.Xml;

namespace OfdSharp.Core.Attachment
{
    /// <summary>
    /// 附件列表
    /// 附件列表文件的入口点在 7.5 文档根节点中定义。
    /// 一个OFD文件可以定义多个附件，附件列表结构如图 91 所示。
    /// </summary>
    public class AttachmentCollect : OfdElement
    {
        public AttachmentCollect(XmlDocument xmlDocument) : base(xmlDocument, "Attachments")
        {
        }

        public IList<Attachment> Attachments { get; set; }
    }
}
