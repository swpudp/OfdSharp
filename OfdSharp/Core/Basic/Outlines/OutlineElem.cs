using OfdSharp.Core.Action;
using System.Xml;

namespace OfdSharp.Core.Basic.Outlines
{
    /// <summary>
    /// 大纲节点标题
    /// </summary>
    public class OutlineElem : OfdElement
    {
        public OutlineElem(XmlDocument xmlDocument, string title) : base(xmlDocument, "OutlineElem")
        {
            Title = title;
        }

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
        public ActionCollect Actions { get; set; }
    }
}
