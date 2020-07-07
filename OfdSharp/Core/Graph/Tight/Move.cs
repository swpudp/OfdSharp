using OfdSharp.Core.BaseType;
using System.Xml;

namespace OfdSharp.Core.Graph.Tight
{
    /// <summary>
    /// 移动节点
    /// 用于表示到新的绘制点指令
    /// </summary>
    public class Move : OfdElement
    {
        public Move(XmlDocument xmlDocument) : base(xmlDocument, "Move")
        {
        }

        /// <summary>
        /// 移动后新的当前绘制点
        /// </summary>
        public StPosition Point1 { get; set; }
    }
}
