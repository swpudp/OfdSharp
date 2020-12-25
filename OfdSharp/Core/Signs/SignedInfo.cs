using System;
using System.Xml.Serialization;

namespace OfdSharp.Core.Signs
{
    /// <summary>
    /// 签名要保护的原文及本次签名相关的信息
    /// </summary>
    public class SignedInfo
    {
        /// <summary>
        /// 创建签名时所用的签章组件提供者信息
        /// </summary>
        [XmlElement]
        public Provider Provider { get; set; }

        /// <summary>
        /// 签名方法
        /// 记录安全模块返回的签名算法代码，以便验证时使用
        /// </summary>
        [XmlElement]
        public string SignatureMethod { get; set; }

        /// <summary>
        /// 签名时间
        /// 记录安全模块返回的签名时间，以便验证时使用
        /// <example>20200305112131Z</example>
        /// </summary>
        [XmlElement]
        public string SignatureDateTime { get; set; }

        /// <summary>
        /// 包内文件计算所得的摘要记录列表
        /// 一个受本次签名保护的包内文件对应一个 Reference节点
        /// </summary>
        [XmlElement("References")]
        public ReferenceCollect ReferenceCollect { get; set; }

        /// <summary>
        /// 签名关联的外观（用OFD中的注解表示）序列
        /// </summary>
        [XmlElement("StampAnnot")]
        public StampAnnot StampAnnot { get; set; }

        /// <summary>
        /// 电子印章信息
        /// </summary>
        [XmlElement]
        public Seal Seal { get; set; }
    }
}
