using System.Xml.Serialization;

namespace OfdSharp.Core.Signs
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
        public BaseLoc BaseLoc { get; set; }
    }

    public class BaseLoc
    {
        [XmlText]
        public string Value { get; set; }
    }
}
