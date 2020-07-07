using System.Xml;

namespace OfdSharp.Core.Signatures
{
    /// <summary>
    /// 电子印章信息
    /// </summary>
    public class Seal : OfdElement
    {
        public Seal(XmlDocument xmlDocument) : base(xmlDocument, "Seal")
        {
        }

        /// <summary>
        /// 指向包内的安全电子印章文件路径
        /// </summary>
        public string BaseLoc { get; set; }
    }
}
