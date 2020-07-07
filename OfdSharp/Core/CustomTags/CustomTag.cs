using System.Xml;

namespace OfdSharp.Core.CustomTags
{
    /// <summary>
    /// 自定义标引入口
    /// </summary>
    public class CustomTag : OfdElement
    {
        public CustomTag(XmlDocument xmlDocument) : base(xmlDocument, "CustomTag")
        {
        }

        /// <summary>
        /// 自定义标引内容节点使用的类型标识
        /// </summary>
        public string TypeId { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace { get; set; }

        /// <summary>
        /// 指向自定义标引内容节点适用的Schema文件
        /// </summary>
        public string SchemaLoc { get; set; }

        /// <summary>
        /// 指向自定义标引文件
        /// </summary>
        public string FileLoc { get; set; }
    }
}
