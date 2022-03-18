namespace OfdSharp.Invoice
{
    /// <summary>
    /// 销售方信息
    /// </summary>
    public class SellerInfo
    {
        /// <summary>
        /// 销售方名称
        /// </summary>
        public string SellerName { get; set; }

        /// <summary>
        /// 销售方税号
        /// </summary>
        public string SellerTaxNo { get; set; }

        /// <summary>
        /// 销售方地址及电话
        /// </summary>
        public string SellerAddressTel { get; set; }

        /// <summary>
        /// 销售方开户行及账号
        /// </summary>
        public string SellerBankAccount { get; set; }
    }
}
