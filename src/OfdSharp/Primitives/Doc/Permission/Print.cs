namespace OfdSharp.Primitives.Doc.Permission
{
    /// <summary>
    /// 打印权限
    /// 具体的权限和份数设置由其属性 Printable 及 Copies 控制。若不设置 Print节点，
    /// 则默认可以打印，并且打印份数不受限制
    /// </summary>
    public class Print
    {
        /// <summary>
        /// 文档是否允许被打印 默认true
        /// </summary>
        public bool Printable { get; set; } = true;

        /// <summary>
        /// 打印份数，在Printable为true时有效，
        /// 若Printable为true并且不设置Copies则打印份数不受限，
        /// 若Copies设置负值时，打印份数不受限，
        /// 当Copies为0时，不允许打印，
        /// 当Copies大于0时，则代表实际可打印的份数值
        /// 默认null
        /// </summary>
        public int? Copies { get; set; }
    }
}
