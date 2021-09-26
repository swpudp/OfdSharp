using System.Collections.Generic;

namespace OfdSharp.Primitives.PageTree
{
    /// <summary>
    /// 页树
    /// </summary>
    public class Pages
    {
        /// <summary>
        /// 页节点。一个页树中可以包含一个或多个页节点,页顺序是根据页树进行前序遍历时叶节点的访问顺序
        /// </summary>
        public IList<Page> Page { get; set; }
    }
}
