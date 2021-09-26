using System.Xml.Serialization;

namespace OfdSharp.Primitives.Signature
{
    /// <summary>
    /// 签名的外观
    /// 一个数字签名可以跟一个或多个外观描述关联，也可以不关联任何外观，
    /// </summary>
    public class StampAnnot
    {
        /// <summary>
        /// 签章注释的标识
        /// </summary>
        [XmlAttribute("ID")]
        public Id Id { get; set; }

        /// <summary>
        /// 引用外观注释所在的页面的标识符
        /// </summary>
        [XmlAttribute]
        public RefId PageRef { get; set; }

        /// <summary>
        /// 签章注释的外观边框位置
        /// </summary>
        [XmlAttribute]
        public Box Boundary { get; set; }

        /// <summary>
        /// 签章注释的外观裁剪设置
        /// </summary>
        [XmlAttribute]
        public Box Clip { get; set; }
    }
}
