using OfdSharp.Core.Action;

namespace OfdSharp.Core.Basic.Doc.Bookmark
{
    /// <summary>
    /// 本标准支持书签，可以将常用位置定义为书签，文档可以包含一组书签。
    /// </summary>
    public class Bookmark
    {
        /// <summary>
        /// 目标区域
        /// </summary>
        public CtDest Dest { get; }

        /// <summary>
        /// 书签名称
        /// </summary>
        public string Name { get; }
    }
}
