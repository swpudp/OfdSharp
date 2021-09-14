using System.Xml.Serialization;

namespace OfdSharp.Core.Invoice
{
    /// <summary>
    /// 购买方信息
    /// </summary>
    [System.Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
    [XmlRoot(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019", IsNullable = false)]
    public class BuyerInfo
    {
        /// <summary>
        /// 购买方名称
        /// </summary>
        [XmlElement]
        public string BuyerName { get; set; }

        /// <summary>
        /// 购买方税号
        /// </summary>
        [XmlElement("BuyerTaxID")]
        public string BuyerTaxNo { get; set; }

        /// <summary>
        /// 购买方地址及电话
        /// </summary>
        [XmlElement("BuyerAddrTel")]
        public string BuyerAddressTel { get; set; }

        /// <summary>
        /// 购买方开户行及账号
        /// </summary>
        [XmlElement("BuyerFinancialAccount")]
        public string BuyerBankAccount { get; set; }

    }
}
