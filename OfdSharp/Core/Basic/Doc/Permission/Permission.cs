using System.Xml;

namespace OfdSharp.Core.Basic.Doc.Permission
{
    /// <summary>
    /// 标准支持设置文档权限声明（Permission）节点，以达到文档防扩散等应用目的。
    /// 文档权限声明结构如 图 9 所示。
    /// </summary>
    public class Permission:OfdElement
    {
        public Permission(XmlDocument xmlDocument) : base(xmlDocument, "Permission")
        {
        }

        /// <summary>
        /// 获取 是否允许编辑
        /// </summary>
        public bool Edit { get; set; }

        /// <summary>
        /// 设置 是否允许添加或修改标注
        /// </summary>
        public bool Annot { get; set; }

        /// <summary>
        /// 是否允许导出
        /// </summary>
        public bool Export { get; set; } = true;

        /// <summary>
        /// 是否允许进行数字签名
        /// </summary>
        public bool Signature { get; set; } = true;

        /// <summary>
        /// 是否允许添加水印
        /// </summary>
        public bool Watermark { get; set; } = true;

        /// <summary>
        /// 是否允许截屏
        /// </summary>
        public bool PrintScreen { get; set; } = true;

        /// <summary>
        /// 打印权限
        /// </summary>
        public Print Print { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public ValidPeriod ValidPeriod { get; set; }
    }
}
