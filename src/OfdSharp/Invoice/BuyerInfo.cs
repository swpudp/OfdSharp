namespace OfdSharp.Invoice
{
    /// <summary>
    /// 购买方信息
    /// </summary>
    public class BuyerInfo
    {
        /// <summary>
        /// 购买方名称
        /// </summary>
        public string BuyerName { get; set; }

        /// <summary>
        /// 购买方税号
        /// </summary>
        public string BuyerTaxNo { get; set; }

        /// <summary>
        /// 购买方地址及电话
        /// </summary>
        public string BuyerAddressTel { get; set; }

        /// <summary>
        /// 购买方开户行及账号
        /// </summary>
        public string BuyerBankAccount { get; set; }
    }
}