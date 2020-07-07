using OfdSharp.Core.Version;
using System.Xml;

namespace OfdSharp.Core.Basic.Ofd
{
    /// <summary>
    /// 文件对象入口，可以存在多个，以便在一个文档中包含多个版式文档
    /// </summary>
    public class DocBody : OfdElement
    {
        /// <summary>
        /// 文档根节点文档名称
        /// </summary>
        public static string DocRoot = "DocRoot";

        public DocBody(XmlDocument xmlDocument) : base(xmlDocument, "DocBody")
        {
        }

        /// <summary>
        /// 文档元数据信息描述
        /// </summary>
        public CtDocInfo DocInfo { get; set; }

        /// <summary>
        /// 包含多个版本描述序列
        /// </summary>
        public VersionCollect Versions { get; set; }

        /// <summary>
        /// 指向该文档中签名和签章结构的路径 （见18章）
        /// </summary>
        public string Signatures { get; set; }
    }
}
