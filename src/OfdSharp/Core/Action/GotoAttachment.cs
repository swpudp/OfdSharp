using OfdSharp.Primitives;

namespace OfdSharp.Core.Action
{
    /// <summary>
    /// 附件动作，表明打开当前文档内的一个附件
    /// </summary>
    public class GotoAttachment
    {
        /// <summary>
        /// 附件的标识
        /// </summary>
        public RefId AttachId { get; set; }

        /// <summary>
        /// 是否在新窗口中打开
        /// </summary>
        public bool NewWindow { get; set; }
    }
}
