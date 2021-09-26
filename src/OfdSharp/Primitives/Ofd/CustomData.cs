using System.Xml.Serialization;

namespace OfdSharp.Primitives.Ofd
{
    /// <summary>
    /// 用户自定义元数据，可以指定一个名称及其对应的值
    /// </summary>
    public class CustomData
    {
        /// <summary>
        /// 元数据名称(Name)
        /// </summary>
        [XmlAttribute("Name")]
        public string Name { get; set; }

        /// <summary>
        /// 元数据值
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }
}
