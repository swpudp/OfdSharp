using System.Xml.Serialization;

namespace OfdSharp.Primitives.Invoice
{
    /// <summary>
    /// 销售方信息
    /// </summary>
    [System.Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
    [XmlRoot(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019", IsNullable = false)]
    public class SellerInfo
    {
        /// <summary>
        /// 销售方名称
        /// </summary>
        [XmlElement]
        public string SellerName { get; set; }

        /// <summary>
        /// 销售方税号
        /// </summary>
        [XmlElement("SellerTaxID")]
        public string SellerTaxNo { get; set; }

        /// <summary>
        /// 销售方地址及电话
        /// </summary>
        [XmlElement("SellerAddrTel")]
        public string SellerAddressTel { get; set; }

        /// <summary>
        /// 销售方开户行及账号
        /// </summary>
        [XmlElement("SellerFinancialAccount")]
        public string SellerBankAccount { get; set; }

    }
}
