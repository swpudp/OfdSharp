using System.Collections.Generic;

namespace OfdSharp.Invoice
{
    /// <summary>
    /// 发票信息
    /// </summary>
    public class InvoiceInfo
    {
        /// <summary>
        /// 文档Id
        /// </summary>
        public string DocId { get; set; }

        /// <summary>
        /// 地区代码
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 开票类型0-正数发票 1-负数发票
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// 发票特殊标识区域2
        /// </summary>
        public string InvoiceSIA2 { get; set; }

        /// <summary>
        /// 发票特殊标识区域1
        /// </summary>
        public string InvoiceSIA1 { get; set; }

        /// <summary>
        /// 发票代码
        /// </summary>
        public string InvoiceCode { get; set; }

        /// <summary>
        /// 发票号码
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// 开票日期
        /// </summary>
        public string IssueDate { get; set; }

        /// <summary>
        /// 校验码
        /// </summary>
        public string InvoiceCheckCode { get; set; }

        /// <summary>
        /// 机器码
        /// </summary>
        public string MachineNo { get; set; }

        /// <summary>
        /// 二维码base64字符
        /// </summary>
        public string GraphCode { get; set; }

        /// <summary>
        /// 密码区
        /// </summary>
        public string TaxControlCode { get; set; }

        /// <summary>
        /// 购买方
        /// </summary>
        public BuyerInfo Buyer { get; set; }

        /// <summary>
        /// 销售方
        /// </summary>
        public SellerInfo Seller { get; set; }

        /// <summary>
        /// 加税合计
        /// </summary>
        public string TaxInclusiveTotalAmount { get; set; }

        /// <summary>
        /// 价税合计中文
        /// </summary>
        public string TaxInclusiveTotalAmountWithWords { get; set; }

        /// <summary>
        /// 合计金额
        /// </summary>
        public string TaxExclusiveTotalAmount { get; set; }

        /// <summary>
        /// 合计税额
        /// </summary>
        public string TaxTotalAmount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 开票人
        /// </summary>
        public string InvoiceClerk { get; set; }

        /// <summary>
        /// 收款人
        /// </summary>
        public string Payee { get; set; }

        /// <summary>
        /// 复核人
        /// </summary>
        public string Checker { get; set; }

        /// <summary>
        /// 签名值
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 认证金额
        /// </summary>
        public string DeductibleAmount { get; set; }

        /// <summary>
        /// 原发票代码
        /// </summary>
        public string OriginalInvoiceCode { get; set; }

        /// <summary>
        /// 原发票号码
        /// </summary>
        public string OriginalInvoiceNo { get; set; }

        /// <summary>
        /// 商品信息
        /// </summary>
        public List<GoodsInfo> GoodsInfos { get; set; }

        /// <summary>
        /// 系统信息
        /// </summary>
        public SystemInfo SystemInfo { get; set; }
    }
}
