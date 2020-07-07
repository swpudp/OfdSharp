using System.Xml;

namespace OfdSharp.Core.Version
{
    /// <summary>
    /// 版本描述入口
    /// </summary>
    public class Version : OfdElement
    {
        public Version(XmlDocument xmlDocument) : base(xmlDocument, "Version")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 是否是默认版本
        /// </summary>
        public bool Current { get; set; }

        /// <summary>
        /// 指向包内的版本描述文件
        /// </summary>
        public string BaseLoc { get; set; }
    }
}
