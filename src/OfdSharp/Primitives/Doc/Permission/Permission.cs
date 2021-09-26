namespace OfdSharp.Primitives.Doc.Permission
{
    /// <summary>
    /// 标准支持设置文档权限声明（Permission）节点，以达到文档防扩散等应用目的。
    /// 文档权限声明结构如 图9所示。
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// 是否允许编辑 默认true
        /// </summary>
        public bool Edit { get; set; } = true;

        /// <summary>
        /// 是否允许添加或修改标注 默认true
        /// </summary>
        public bool Annot { get; set; } = true;

        /// <summary>
        /// 是否允许导出 默认true
        /// </summary>
        public bool Export { get; set; } = true;

        /// <summary>
        /// 是否允许进行数字签名 默认true
        /// </summary>
        public bool Signature { get; set; } = true;

        /// <summary>
        /// 是否允许添加水印 默认true
        /// </summary>
        public bool Watermark { get; set; } = true;

        /// <summary>
        /// 是否允许截屏 默认true
        /// </summary>
        public bool PrintScreen { get; set; } = true;

        /// <summary>
        /// 打印权限，其具体的权限和份数设置由其属性Printable及Copies控制，若不设置Print节点，则默认为可以打印，并且打印次数不受限制
        /// </summary>
        public Print Print { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public ValidPeriod ValidPeriod { get; set; }
    }
}
