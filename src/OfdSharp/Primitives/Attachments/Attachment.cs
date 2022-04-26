using System;

namespace OfdSharp.Primitives.Attachments
{
    /// <summary>
    /// 附件
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// 附件标识
        /// </summary>
        public CtId Id { get; set; }

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
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModDate { get; set; }

        /// <summary>
        /// 附件大小，以KB为单位
        /// </summary>
        public double Size { get; set; }

        /// <summary>
        /// 附件是否可见，默认为true
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// 附件用途，默认为none
        /// </summary>
        public string Usage { get; set; } = "none";

        /// <summary>
        /// 附件内容在包内的路径
        /// </summary>
        public CtLocation FileLoc { get; set; }
    }
}
