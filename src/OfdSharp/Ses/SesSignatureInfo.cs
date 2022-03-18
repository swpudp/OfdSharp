using System;
using System.Collections.Generic;
using System.Text;

namespace OfdSharp.Ses
{
    /// <summary>
    /// 签名参数
    /// </summary>
    public class SesSignatureInfo
    {
        /// <summary>
        /// 厂商
        /// </summary>
        public string manufacturer { get; set; }

        /// <summary>
        /// /Doc_0/Signs/Sign_0/Signature.xml
        /// </summary>
        public string PropertyInfo { get; set; }

        /// <summary>
        /// /Doc_0/Signs/Sign_0/Signature.xml杂凑值
        /// </summary>
        public byte[] dataHash { get; set; }

        /// <summary>
        /// 印章图片
        /// </summary>
        public byte[] sealPicture { get; set; }

        /// <summary>
        /// 印章名称
        /// </summary>
        public string sealName { get; set; }

        /// <summary>
        /// 制章人证书
        /// </summary>
        public byte[] sealCert { get; set; }

        /// <summary>
        /// 电子印章签名值
        /// </summary>
        public byte[] sealSign { get; set; }

        /// <summary>
        /// 签章者证书
        /// </summary>
        public byte[] signerCert { get; set; }

        /// <summary>
        /// SignedValue.xml签名值
        /// </summary>
        public byte[] signature { get; set; }

        /// <summary>
        /// 印章标识
        /// </summary>
        public string esId { get; set; }
    }
}
