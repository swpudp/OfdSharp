using OfdSharp.Core.Basic.Doc.Bookmark;

namespace OfdSharp.Core.Action
{
    /// <summary>
    /// 跳转动作
    /// </summary>
    public class Goto
    {
        /// <summary>
        /// 跳转的目标区域
        /// </summary>
        public CtDest Dest { get; set; }

        /// <summary>
        /// 跳转的目标书签
        /// </summary>
        public Bookmark Bookmark { get; set; }
    }
}
