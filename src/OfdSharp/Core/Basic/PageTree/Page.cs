using OfdSharp.Primitives;

namespace OfdSharp.Core.Basic.PageTree
{
    /// <summary>
    /// 页节点
    /// </summary>
    public class Page
    {
        /// <summary>
        /// 申明该页的标识，不能与已有标识重复
        /// </summary>
        public Id Id { get; set; }

        /// <summary>
        /// 指向页对象描述文件
        /// </summary>
        public Location BaseLoc { get; set; }
    }
}
