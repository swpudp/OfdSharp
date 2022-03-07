namespace OfdSharp.Primitives.Invoice
{
    /// <summary>
    /// 商品信息
    /// </summary>
    public class GoodsInfo
    {
        /// <summary>
        /// 行号
        /// </summary>
        public int LineNo { get; set; }

        /// <summary>
        /// 折扣行标识
        /// 0-非折扣行 1-折扣行
        /// </summary>
        public int LineNature { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// 税收分类编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string MeasurementDimension { get; set; }

        /// <summary>
        /// 无税单价
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string Quantity { get; set; }

        /// <summary>
        /// 不含税金额
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// 税率
        /// </summary>
        public string TaxScheme { get; set; }

        /// <summary>
        /// 税额
        /// </summary>
        public string TaxAmount { get; set; }

        /// <summary>
        /// 优惠政策标识
        /// </summary>
        public string PreferentialMark { get; set; }

        /// <summary>
        /// 免税标识
        /// </summary>
        public string FreeTaxMark { get; set; }

        /// <summary>
        /// 增值税管理
        /// </summary>
        public string VATSpecialManagement { get; set; }
    }
}
