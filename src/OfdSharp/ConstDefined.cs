using System.Xml.Linq;

namespace OfdSharp
{
    /// <summary>
    /// 静态变量
    /// </summary>
    internal static class ConstDefined
    {
        /// <summary>
        /// 命名空间 URI,《GB/T_33190-2016》 7.1 命名空间
        /// </summary>
        public const string OfdNamespaceUri = "http://www.ofdspec.org/2016";

        /// <summary>
        /// 元素节点应使用命名空间标识符
        /// ————《GB/T 33190-2016》 7.1 命名空间
        /// </summary>
        public const string OfdPrefix = "ofd";

        /// <summary>
        /// 文档类型
        /// </summary>
        public const string OfdDocType = "OFD";

        /// <summary>
        /// 文档版本号
        /// </summary>
        public const string OfdVersion = "1.1";

        /// <summary>
        /// 日期格式化
        /// </summary>
        public const string DateFormatter = "yyyy-MM-dd";

        /// <summary>
        /// 类型时间日期格式化
        /// </summary>
        public const string DatetimeFormatter = "yyyy-MM-dd'T'hh:mm:ss";

        /// <summary>
        /// 入口文件
        /// </summary>
        public const string OfdRootFileName = "OFD.xml";

        /// <summary>
        /// 发票文件命名空间uri
        /// </summary>
        public const string InvoiceNamespaceUri = "http://www.edrm.org.cn/schema/e-invoice/2019";

        /// <summary>
        /// 命名空间,《GB/T_33190-2016》 7.1 命名空间
        /// </summary>
        public static readonly XNamespace OfdNamespace = "http://www.ofdspec.org/2016";

        /// <summary>
        /// Xmlns uri
        /// </summary>
        public const string OfdXmlns = "http://www.ofdspec.org/2016";

        /// <summary>
        /// 签章版本v1
        /// </summary>
        public const int SesV1 = 1;

        /// <summary>
        /// 签章版本v4
        /// </summary>
        public const int SesV4 = 4;
    }
}