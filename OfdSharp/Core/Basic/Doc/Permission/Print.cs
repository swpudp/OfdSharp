using System.Xml;

namespace OfdSharp.Core.Basic.Doc.Permission
{
    /// <summary>
    /// 打印权限
    /// 具体的权限和份数设置由其属性 Printable 及 Copics 控制。若不设置 Print节点，
    /// 则默认可以打印，并且打印份数不受限制
    /// </summary>
    public class Print : OfdElement
    {
        public Print(XmlDocument xmlDocument, bool printable, int copies) : base(xmlDocument, "Print")
        {
            Printable = printable;
            Copies = copies;

            Element.SetAttribute("Printable", printable.ToString());
            Element.SetAttribute("Copies", copies.ToString());
        }

        /// <summary>
        /// 是否允许被打印
        /// </summary>
        public bool Printable { get; }

        /// <summary>
        /// 设置 打印份数
        /// 在 Printable 为 true 时有效，若 Printable 为 true
        /// 并且不设置 Copies 则打印份数不受限，若 Copies 的值为负值时，
        /// 打印份数不受限，当 Copies 的值为 0 时，不允许打印，当 Copies的值
        /// 大于 0 时，则代表实际可打印的份数值。
        /// 默认值为 -1
        /// </summary>
        public int Copies { get; }
    }
}
