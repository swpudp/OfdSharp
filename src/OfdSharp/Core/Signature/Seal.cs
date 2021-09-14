using OfdSharp.Primitives;
using System.Xml.Serialization;

namespace OfdSharp.Core.Signature
{
    /// <summary>
    /// 电子印章信息
    /// </summary>
    public class Seal
    {
        /// <summary>
        /// 指向包内的安全电子印章文件路径
        /// </summary>
        [XmlElement]
        public Location BaseLoc { get; set; }
    }
}
