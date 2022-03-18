// 注意: 生成的代码可能至少需要 .NET Framework 4.5 或 .NET Core/Standard 2.0。

namespace UnitTests.Reader
{
    /// <remarks/>
    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class eInvoice
    {

        private byte docIDField;

        private ushort areaCodeField;

        private byte typeCodeField;

        private object invoiceSIA2Field;

        private object invoiceSIA1Field;

        private ulong invoiceCodeField;

        private uint invoiceNoField;

        private string issueDateField;

        private string invoiceCheckCodeField;

        private object machineNoField;

        private string graphCodeField;

        private string taxControlCodeField;

        private Buyer buyerField;

        private Seller sellerField;

        private decimal taxInclusiveTotalAmountField;

        private string taxInclusiveTotalAmountWithWordsField;

        private decimal taxExclusiveTotalAmountField;

        private decimal taxTotalAmountField;

        private string noteField;

        private string invoiceClerkField;

        private string payeeField;

        private string checkerField;

        private object signatureField;

        private object deductibleAmountField;

        private object originalInvoiceCodeField;

        private object originalInvoiceNoField;

        private GoodsInfosGoodsInfo[] goodsInfosField;

        private SystemInfos systemInfosField;

        private decimal versionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public byte DocID
        {
            get
            {
                return this.docIDField;
            }
            set
            {
                this.docIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public ushort AreaCode
        {
            get
            {
                return this.areaCodeField;
            }
            set
            {
                this.areaCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public byte TypeCode
        {
            get
            {
                return this.typeCodeField;
            }
            set
            {
                this.typeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public object InvoiceSIA2
        {
            get
            {
                return this.invoiceSIA2Field;
            }
            set
            {
                this.invoiceSIA2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public object InvoiceSIA1
        {
            get
            {
                return this.invoiceSIA1Field;
            }
            set
            {
                this.invoiceSIA1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public ulong InvoiceCode
        {
            get
            {
                return this.invoiceCodeField;
            }
            set
            {
                this.invoiceCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public uint InvoiceNo
        {
            get
            {
                return this.invoiceNoField;
            }
            set
            {
                this.invoiceNoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string IssueDate
        {
            get
            {
                return this.issueDateField;
            }
            set
            {
                this.issueDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string InvoiceCheckCode
        {
            get
            {
                return this.invoiceCheckCodeField;
            }
            set
            {
                this.invoiceCheckCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public object MachineNo
        {
            get
            {
                return this.machineNoField;
            }
            set
            {
                this.machineNoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string GraphCode
        {
            get
            {
                return this.graphCodeField;
            }
            set
            {
                this.graphCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string TaxControlCode
        {
            get
            {
                return this.taxControlCodeField;
            }
            set
            {
                this.taxControlCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public Buyer Buyer
        {
            get
            {
                return this.buyerField;
            }
            set
            {
                this.buyerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public Seller Seller
        {
            get
            {
                return this.sellerField;
            }
            set
            {
                this.sellerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public decimal TaxInclusiveTotalAmount
        {
            get
            {
                return this.taxInclusiveTotalAmountField;
            }
            set
            {
                this.taxInclusiveTotalAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string TaxInclusiveTotalAmountWithWords
        {
            get
            {
                return this.taxInclusiveTotalAmountWithWordsField;
            }
            set
            {
                this.taxInclusiveTotalAmountWithWordsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public decimal TaxExclusiveTotalAmount
        {
            get
            {
                return this.taxExclusiveTotalAmountField;
            }
            set
            {
                this.taxExclusiveTotalAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public decimal TaxTotalAmount
        {
            get
            {
                return this.taxTotalAmountField;
            }
            set
            {
                this.taxTotalAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string Note
        {
            get
            {
                return this.noteField;
            }
            set
            {
                this.noteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string InvoiceClerk
        {
            get
            {
                return this.invoiceClerkField;
            }
            set
            {
                this.invoiceClerkField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string Payee
        {
            get
            {
                return this.payeeField;
            }
            set
            {
                this.payeeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public string Checker
        {
            get
            {
                return this.checkerField;
            }
            set
            {
                this.checkerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public object Signature
        {
            get
            {
                return this.signatureField;
            }
            set
            {
                this.signatureField = value;
            }
        }

        /// <remarks/>
        public object DeductibleAmount
        {
            get
            {
                return this.deductibleAmountField;
            }
            set
            {
                this.deductibleAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public object OriginalInvoiceCode
        {
            get
            {
                return this.originalInvoiceCodeField;
            }
            set
            {
                this.originalInvoiceCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public object OriginalInvoiceNo
        {
            get
            {
                return this.originalInvoiceNoField;
            }
            set
            {
                this.originalInvoiceNoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        [System.Xml.Serialization.XmlArrayItemAttribute("GoodsInfo", IsNullable = false)]
        public GoodsInfosGoodsInfo[] GoodsInfos
        {
            get
            {
                return this.goodsInfosField;
            }
            set
            {
                this.goodsInfosField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
        public SystemInfos SystemInfos
        {
            get
            {
                return this.systemInfosField;
            }
            set
            {
                this.systemInfosField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute]
        public decimal Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019", IsNullable = false)]
    public partial class Buyer
    {

        private string buyerNameField;

        private string buyerTaxIDField;

        private string buyerAddrTelField;

        private string buyerFinancialAccountField;

        /// <remarks/>
        public string BuyerName
        {
            get
            {
                return this.buyerNameField;
            }
            set
            {
                this.buyerNameField = value;
            }
        }

        /// <remarks/>
        public string BuyerTaxID
        {
            get
            {
                return this.buyerTaxIDField;
            }
            set
            {
                this.buyerTaxIDField = value;
            }
        }

        /// <remarks/>
        public string BuyerAddrTel
        {
            get
            {
                return this.buyerAddrTelField;
            }
            set
            {
                this.buyerAddrTelField = value;
            }
        }

        /// <remarks/>
        public string BuyerFinancialAccount
        {
            get
            {
                return this.buyerFinancialAccountField;
            }
            set
            {
                this.buyerFinancialAccountField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019", IsNullable = false)]
    public partial class Seller
    {

        private string sellerNameField;

        private ulong sellerTaxIDField;

        private string sellerAddrTelField;

        private string sellerFinancialAccountField;

        /// <remarks/>
        public string SellerName
        {
            get
            {
                return this.sellerNameField;
            }
            set
            {
                this.sellerNameField = value;
            }
        }

        /// <remarks/>
        public ulong SellerTaxID
        {
            get
            {
                return this.sellerTaxIDField;
            }
            set
            {
                this.sellerTaxIDField = value;
            }
        }

        /// <remarks/>
        public string SellerAddrTel
        {
            get
            {
                return this.sellerAddrTelField;
            }
            set
            {
                this.sellerAddrTelField = value;
            }
        }

        /// <remarks/>
        public string SellerFinancialAccount
        {
            get
            {
                return this.sellerFinancialAccountField;
            }
            set
            {
                this.sellerFinancialAccountField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
    public partial class GoodsInfosGoodsInfo
    {

        private byte lineNoField;

        private byte lineNatureField;

        private string itemField;

        private ulong codeField;

        private object specificationField;

        private string measurementDimensionField;

        private decimal priceField;

        private byte quantityField;

        private decimal amountField;

        private string taxSchemeField;

        private decimal taxAmountField;

        private object preferentialMarkField;

        private object freeTaxMarkField;

        private object vATSpecialManagementField;

        /// <remarks/>
        public byte LineNo
        {
            get
            {
                return this.lineNoField;
            }
            set
            {
                this.lineNoField = value;
            }
        }

        /// <remarks/>
        public byte LineNature
        {
            get
            {
                return this.lineNatureField;
            }
            set
            {
                this.lineNatureField = value;
            }
        }

        /// <remarks/>
        public string Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        public ulong Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        public object Specification
        {
            get
            {
                return this.specificationField;
            }
            set
            {
                this.specificationField = value;
            }
        }

        /// <remarks/>
        public string MeasurementDimension
        {
            get
            {
                return this.measurementDimensionField;
            }
            set
            {
                this.measurementDimensionField = value;
            }
        }

        /// <remarks/>
        public decimal Price
        {
            get
            {
                return this.priceField;
            }
            set
            {
                this.priceField = value;
            }
        }

        /// <remarks/>
        public byte Quantity
        {
            get
            {
                return this.quantityField;
            }
            set
            {
                this.quantityField = value;
            }
        }

        /// <remarks/>
        public decimal Amount
        {
            get
            {
                return this.amountField;
            }
            set
            {
                this.amountField = value;
            }
        }

        /// <remarks/>
        public string TaxScheme
        {
            get
            {
                return this.taxSchemeField;
            }
            set
            {
                this.taxSchemeField = value;
            }
        }

        /// <remarks/>
        public decimal TaxAmount
        {
            get
            {
                return this.taxAmountField;
            }
            set
            {
                this.taxAmountField = value;
            }
        }

        /// <remarks/>
        public object PreferentialMark
        {
            get
            {
                return this.preferentialMarkField;
            }
            set
            {
                this.preferentialMarkField = value;
            }
        }

        /// <remarks/>
        public object FreeTaxMark
        {
            get
            {
                return this.freeTaxMarkField;
            }
            set
            {
                this.freeTaxMarkField = value;
            }
        }

        /// <remarks/>
        public object VATSpecialManagement
        {
            get
            {
                return this.vATSpecialManagementField;
            }
            set
            {
                this.vATSpecialManagementField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019", IsNullable = false)]
    public partial class SystemInfos
    {

        private string systemCodeField;

        private string systemNameField;

        private byte systemTypeField;

        /// <remarks/>
        public string SystemCode
        {
            get
            {
                return this.systemCodeField;
            }
            set
            {
                this.systemCodeField = value;
            }
        }

        /// <remarks/>
        public string SystemName
        {
            get
            {
                return this.systemNameField;
            }
            set
            {
                this.systemNameField = value;
            }
        }

        /// <remarks/>
        public byte SystemType
        {
            get
            {
                return this.systemTypeField;
            }
            set
            {
                this.systemTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.edrm.org.cn/schema/e-invoice/2019", IsNullable = false)]
    public partial class GoodsInfos
    {

        private GoodsInfosGoodsInfo[] goodsInfoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("GoodsInfo")]
        public GoodsInfosGoodsInfo[] GoodsInfo
        {
            get
            {
                return this.goodsInfoField;
            }
            set
            {
                this.goodsInfoField = value;
            }
        }
    }
}