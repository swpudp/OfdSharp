using System.Xml.Linq;

namespace OfdSharp.Invoice
{
    /// <summary>
    /// 发票信息元素构建器
    /// </summary>
    public static class InvoiceInfoBuilder
    {
        /// <summary>
        /// 发票命名空间
        /// </summary>
        private static readonly XNamespace invoiceNamespace = "http://www.edrm.org.cn/schema/e-invoice/2019";

        /// <summary>
        /// 发票xmlns地址
        /// </summary>
        private const string invoiceXmlns = "http://www.edrm.org.cn/schema/e-invoice/2019";

        /// <summary>
        /// 创建发票元素
        /// </summary>
        /// <param name="invoiceInfo"></param>
        /// <returns></returns>
        public static XElement CreateInvoiceElement(InvoiceInfo invoiceInfo)
        {
            XElement element = new XElement("eInvoice", new XAttribute(XNamespace.Xmlns + "fp", invoiceXmlns), new XAttribute("Version", "1.0"));
            element.AddElement("DocID", invoiceInfo.DocId);
            element.AddElement("AreaCode", invoiceInfo.AreaCode);
            element.AddElement("TypeCode", invoiceInfo.TypeCode);
            element.AddElement("InvoiceSIA2", invoiceInfo.InvoiceSIA2);
            element.AddElement("InvoiceSIA1", invoiceInfo.InvoiceSIA1);
            element.AddElement("InvoiceCode", invoiceInfo.InvoiceCode);
            element.AddElement("InvoiceNo", invoiceInfo.InvoiceNo);
            element.AddCDataElement("IssueDate", invoiceInfo.IssueDate);
            element.AddCDataElement("InvoiceCheckCode", invoiceInfo.InvoiceCheckCode);
            element.AddElement("MachineNo", invoiceInfo.MachineNo);
            element.AddCDataElement("GraphCode", invoiceInfo.GraphCode);
            element.AddCDataElement("TaxControlCode", invoiceInfo.TaxControlCode);

            XElement buyerElement = CreateElement("Buyer");
            buyerElement.AddCDataElement("BuyerName", invoiceInfo.Buyer.BuyerName);
            buyerElement.AddElement("BuyerTaxID", invoiceInfo.Buyer.BuyerTaxNo);
            buyerElement.AddCDataElement("BuyerAddrTel", invoiceInfo.Buyer.BuyerAddressTel);
            buyerElement.AddCDataElement("BuyerFinancialAccount", invoiceInfo.Buyer.BuyerBankAccount);
            element.Add(buyerElement);

            XElement sellerElement = CreateElement("Seller");
            sellerElement.AddCDataElement("SellerName", invoiceInfo.Seller.SellerName);
            sellerElement.AddElement("SellerTaxID", invoiceInfo.Seller.SellerTaxNo);
            sellerElement.AddCDataElement("SellerAddrTel", invoiceInfo.Seller.SellerAddressTel);
            sellerElement.AddCDataElement("SellerFinancialAccount", invoiceInfo.Seller.SellerBankAccount);
            element.Add(sellerElement);

            element.AddElement("TaxInclusiveTotalAmount", invoiceInfo.TaxInclusiveTotalAmount);
            element.AddElement("TaxInclusiveTotalAmountWithWords", invoiceInfo.TaxInclusiveTotalAmountWithWords);
            element.AddElement("TaxExclusiveTotalAmount", invoiceInfo.TaxExclusiveTotalAmount);
            element.AddElement("TaxTotalAmount", invoiceInfo.TaxTotalAmount);
            element.AddCDataElement("Note", invoiceInfo.Note);
            element.AddCDataElement("InvoiceClerk", invoiceInfo.InvoiceClerk);
            element.AddCDataElement("Payee", invoiceInfo.Payee);
            element.AddCDataElement("Checker", invoiceInfo.Checker);
            element.AddElement("Signature", invoiceInfo.Signature);
            element.AddElement("DeductibleAmount", invoiceInfo.DeductibleAmount);
            element.AddElement("OriginalInvoiceCode", invoiceInfo.OriginalInvoiceCode);
            element.AddElement("OriginalInvoiceNo", invoiceInfo.OriginalInvoiceNo);

            XElement goodsElement = CreateElement("GoodsInfos");
            foreach (GoodsInfo goods in invoiceInfo.GoodsInfos)
            {
                XElement goodsInfoElement = CreateElement("GoodsInfo");
                goodsInfoElement.AddElement("LineNo", goods.LineNo);
                goodsInfoElement.AddElement("LineNature", goods.LineNature);
                goodsInfoElement.AddCDataElement("Item", goods.Item);
                goodsInfoElement.AddElement("Code", goods.Code);
                goodsInfoElement.AddElement("Specification", goods.Specification);
                goodsInfoElement.AddCDataElement("MeasurementDimension", goods.MeasurementDimension);
                goodsInfoElement.AddElement("Price", goods.Price);
                goodsInfoElement.AddElement("Quantity", goods.Quantity);
                goodsInfoElement.AddElement("Amount", goods.Amount);
                goodsInfoElement.AddElement("TaxScheme", goods.TaxScheme);
                goodsInfoElement.AddElement("TaxAmount", goods.TaxAmount);
                goodsInfoElement.AddElement("PreferentialMark", goods.PreferentialMark);
                goodsInfoElement.AddElement("FreeTaxMark", goods.FreeTaxMark);
                goodsInfoElement.AddElement("VATSpecialManagement", goods.VATSpecialManagement);
                goodsElement.Add(goodsInfoElement);
            }
            element.Add(goodsElement);

            if (invoiceInfo.SystemInfo != null)
            {
                XElement systemInfoElement = CreateElement("SystemInfos");
                systemInfoElement.AddElement("SystemCode", invoiceInfo.SystemInfo.SystemCode);
                systemInfoElement.AddElement("SystemName", invoiceInfo.SystemInfo.SystemName);
                systemInfoElement.AddElement("SystemType", invoiceInfo.SystemInfo.SystemType);
                element.Add(systemInfoElement);
            }
            return element;
        }

        /// <summary>
        /// 构建发票自定义标签
        /// todo 发票自定义标签如何定义？
        /// </summary>
        /// <returns></returns>
        public static XElement CreateInvoiceTagElement()
        {
            XElement element = new XElement("eInvoice", new XAttribute(XNamespace.Xmlns + "fp", invoiceXmlns));
            element.Add(new XElement("InvoiceCode", new XElement(invoiceNamespace + "ObjectRef", "64", new XAttribute("PageRef", "1"))));

            XElement buyerElement = new XElement("Buyer");
            buyerElement.Add(new XElement("BuyerName", new XElement(invoiceNamespace + "ObjectRef", "65", new XAttribute("PageRef", "1"))));
            buyerElement.Add(new XElement("BuyerTaxID", new XElement(invoiceNamespace + "ObjectRef", "66", new XAttribute("PageRef", "1"))));
            buyerElement.Add(new XElement("BuyerAddrTel", new XElement(invoiceNamespace + "ObjectRef", "76", new XAttribute("PageRef", "1"))));
            buyerElement.Add(new XElement("BuyerFinancialAccount", new XElement(invoiceNamespace + "ObjectRef", "79", new XAttribute("PageRef", "1"))));
            element.Add(buyerElement);

            XElement sellerElement = new XElement("Seller");
            sellerElement.Add(new XElement("SellerName", new XElement(invoiceNamespace + "ObjectRef", "68", new XAttribute("PageRef", "1"))));
            sellerElement.Add(new XElement("SellerTaxID", new XElement(invoiceNamespace + "ObjectRef", "69", new XAttribute("PageRef", "1"))));
            sellerElement.Add(new XElement("SellerAddrTel", new XElement(invoiceNamespace + "ObjectRef", "73", new XAttribute("PageRef", "1"))));
            sellerElement.Add(new XElement("SellerFinancialAccount", new XElement(invoiceNamespace + "ObjectRef", "77", new XAttribute("PageRef", "1"))));
            element.Add(sellerElement);

            return element;
        }

        private static XElement CreateElement(string nodeName)
        {
            return new XElement(invoiceNamespace + nodeName);
        }

        private static void AddElement(this XElement e, string nodeName, string value)
        {
            e.Add(new XElement(invoiceNamespace + nodeName, value));
        }

        private static void AddElement(this XElement e, string nodeName, int value)
        {
            e.Add(new XElement(invoiceNamespace + nodeName, value));
        }

        private static void AddCDataElement(this XElement e, string nodeName, string value)
        {
            e.Add(new XElement(invoiceNamespace + nodeName, new XCData(value)));
        }
    }
}
