using OfdSharp.Primitives.Action;
using System.Collections.Generic;

namespace OfdSharp.Primitives.Outlines
{
    /// <summary>
    /// 大纲节点标题
    /// </summary>
    public class OutlineElem
    {
        /// <summary>
        /// 大纲节点标题
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// 该节点下所有叶节点的数目参考值
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 在有子节点存在时有效，如果为 true，
        /// 表示该大纲在初始状态下展开子节点；
        /// 如果为 false，则表示不展开
        /// </summary>
        public bool Expanded { get; set; }

        /// <summary>
        /// 当此大纲节点被激活时执行的动作序列
        /// </summary>
        public IList<CtAction> Actions { get; set; }

        /// <summary>
        /// OutlineElem：该节点的子大纲节点。层层嵌套,形成树状结构
        /// </summary>
        public OutlineElem Next { get; set; }
    }
}
