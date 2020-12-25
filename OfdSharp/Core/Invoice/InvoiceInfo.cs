using System.Collections.Generic;
using System.Xml.Serialization;

namespace OfdSharp.Core.Invoice
{
    /// <summary>
    /// 发票信息
    /// </summary>
    //[XmlRoot("eInvoice")]
    [System.Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "eInvoice", IsNullable = false)]
    public class InvoiceInfo
    {
        /// <summary>
        /// 文档Id
        /// </summary>
        [XmlElement("DocID", Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string DocId { get; set; }

        /// <summary>
        /// 地区代码
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string AreaCode { get; set; }

        /// <summary>
        /// 开票类型0-正数发票 1-负数发票
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string TypeCode { get; set; }

        /// <summary>
        /// 发票特殊标识区域2
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string InvoiceSIA2 { get; set; }

        /// <summary>
        /// 发票特殊标识区域1
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string InvoiceSIA1 { get; set; }

        /// <summary>
        /// 发票代码
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string InvoiceCode { get; set; }

        /// <summary>
        /// 发票号码
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string InvoiceNo { get; set; }

        /// <summary>
        /// 开票日期
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string IssueDate { get; set; }

        /// <summary>
        /// 校验码
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string InvoiceCheckCode { get; set; }

        /// <summary>
        /// 机器码
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string MachineNo { get; set; }

        /// <summary>
        /// 二维码base64字符
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string GraphCode { get; set; }

        /// <summary>
        /// 密码区
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string TaxControlCode { get; set; }

        /// <summary>
        /// 购买方
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public BuyerInfo Buyer { get; set; }

        /// <summary>
        /// 销售方
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public SellerInfo Seller { get; set; }

        /// <summary>
        /// 加税合计
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string TaxInclusiveTotalAmount { get; set; }

        /// <summary>
        /// 价税合计中文
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string TaxInclusiveTotalAmountWithWords { get; set; }

        /// <summary>
        /// 合计金额
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string TaxExclusiveTotalAmount { get; set; }

        /// <summary>
        /// 合计税额
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string TaxTotalAmount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string Note { get; set; }

        /// <summary>
        /// 开票人
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string InvoiceClerk { get; set; }

        /// <summary>
        /// 收款人
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string Payee { get; set; }

        /// <summary>
        /// 复核人
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string Checker { get; set; }

        /// <summary>
        /// 签名值
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string Signature { get; set; }

        /// <summary>
        /// 认证金额
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string DeductibleAmount { get; set; }

        /// <summary>
        /// 原发票代码
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string OriginalInvoiceCode { get; set; }

        /// <summary>
        /// 原发票号码
        /// </summary>
        [XmlElement(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string OriginalInvoiceNo { get; set; }

        /// <summary>
        /// 商品信息
        /// </summary>
        [XmlArray(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        [XmlArrayItem("GoodsInfo", IsNullable = false)]
        public List<GoodsInfo> GoodsInfos { get; set; }
    }
}
