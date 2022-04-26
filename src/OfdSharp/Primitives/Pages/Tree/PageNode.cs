namespace OfdSharp.Primitives.Pages.Tree
{
    /// <summary>
    /// 页节点
    /// </summary>
    public class PageNode
    {
        /// <summary>
        /// 申明该页的标识，不能与已有标识重复
        /// </summary>
        public CtId Id { get; set; }

        /// <summary>
        /// 指向页对象描述文件
        /// </summary>
        public CtLocation BaseLoc { get; set; }
    }
}
