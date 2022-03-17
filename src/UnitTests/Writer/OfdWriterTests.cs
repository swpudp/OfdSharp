using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfdSharp.Primitives.Invoice;
using OfdSharp.Writer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OfdSharp.Writer.Tests
{
    [TestClass()]
    public class OfdWriterTests
    {
        [TestMethod()]
        public void WriteOfdRootTest()
        {
            OfdWriter writer = new OfdWriter(true);

            writer.WriteOfdRoot();
            writer.WriteDocument();
            writer.WriteDocumentRes();
            writer.WritePublicRes();
            writer.WritePages();
            InvoiceInfo invoiceInfo = CreateInvoiceInfo();
            XElement attachment = CreateInvoiceElement(invoiceInfo);
            XElement tag = CreateInvoiceTagElement();

            writer.AddAttachment("original_invoice", "original_invoice.xml", "xml", false, attachment);
            writer.WriteTemplate();

            writer.WriteCustomerTag(tag);
            writer.WriteAnnotation();

            byte[] content = writer.Flush();
            string fileName = Path.Combine(Directory.GetCurrentDirectory(), "test-root.ofd");
            File.WriteAllBytes(fileName, content);
            Assert.IsTrue(File.Exists(fileName));
        }

        private static InvoiceInfo CreateInvoiceInfo()
        {
            InvoiceInfo invoiceInfo = new InvoiceInfo
            {
                DocId = "01",
                AreaCode = "100",
                TypeCode = "0",
                InvoiceCode = "011002680026",
                InvoiceNo = "71500013",
                IssueDate = "2020年02月20日",
                InvoiceCheckCode = "03272875819686271285",
                MachineNo = string.Empty,
                GraphCode = "iVBORw0KGgoAAAANSUhEUgAAAJQAAACUCAIAAAD6XpeDAAAC+UlEQVR42u3aUU7EMAwEUO5/aTgBEm1nHHd5/UTsbpsXKWO7X1+F6/sP12//f/V7nnz/k+d68ryrL3jw4MH7J3iNxXoCllrcUxul/Vvw4MGDBy+DlzrAU2DtYNKGaawzPHjw4MF7B96pUPMkOJwKZfDgwYMH7314pwCuLnq7MQ0PHjx48HbhTQ4/ry7oZLN4sthfN1WABw8evA/GS13thvXb/776ggQPHrwPxvsuX41g8iS8nBrGVtYWHjx48ODdrvlONYsbvzU5jD3VfIcHDx48eNeesXEIpwaYjYZ1quncDjLw4MGDB28X3oYXhxrh4kkYGR0Ow4MHDx68scK8HQra4aj9ItaT4v1P3wkPHjx48G7jPVmU1OZohKb2hph8dnjw4MGDl8Frh4vJQrgd0NY1puHBgwcPXry3nLrRtxTmsXAxuEHhwYMHD14X79QhvHnQ2tjQ8ODBgwcvvz7biugUUqOBMBle4MGDBw9efp7XKIpPvaiTSmvtxvTl9YcHDx48eJHzr/3iTbu4TgWW9vA29ezw4MGDB6+Ll3rgdrHcbr6n1hAePHjw4M3htcNLo0h/C9KxqQI8ePDgwasWxY3AkiquG+HoNY1pePDgwYM3Vjg3CtJ2UdweIFea4/DgwYMH73a9u23I2fjd1Gcn1xMePHjw4GXwrh68k8V7qkHcBlsxVYAHDx48eMfBNjSOUwFq3VQBHjx48ODdxouloEKI2Jz6VgQWePDgwYN3++BtD10bhXP73lJruPrtMXjw4MH7YLz2yzapgrcRjtphLRVw4MGDBw/e/aFr49rwW082UypcVIIMPHjw4ME7cdbWQ0ojjDRyQAogNSiGBw8ePHjnb7qxoO0NN3nPlz8LDx48ePBu4214UacdNFKD5ckAWJ8qwIMHDx68eKO23fxtBKjGsLpSpMODBw8evFV4jaI7tTka99BuMsCDBw8evMwhmSp4Gws6OXGe3KwrhrHw4MGD98F4o43U8iB0spGdWvRRJHjw4MH7f3g/srP3AoM7TQkAAAAASUVORK5CYII=",
                TaxControlCode = "0092--9<+4-+/8+>-0<1<>157731<+/82/>8*//1+>75-36+9960016643*160///7-25/*<7>34+02>/-24892577>8548+690161611992<795",
                Buyer = new BuyerInfo
                {
                    BuyerName = "百望股份有限公司",
                    BuyerTaxNo = "91110108339805094M",
                    BuyerAddressTel = "北京市海淀区北清路81号一区1号楼14层15层 010-84782665",
                    BuyerBankAccount = "中国建设银行北京苏州桥支行11001079800053014887"
                },
                Seller = new SellerInfo
                {
                    SellerName = "ukey测试",
                    SellerTaxNo = "91110101202001201",
                    SellerAddressTel = "重庆市沙坪坝区狮子路11号",
                    SellerBankAccount = "重庆商业银行624562566022125"
                },
                TaxInclusiveTotalAmount = "10000.00",
                TaxInclusiveTotalAmountWithWords = "壹万圆整",
                TaxExclusiveTotalAmount = "9709.00",
                TaxTotalAmount = "291.00",
                Note = "稻谷交易",
                InvoiceClerk = "李大山",
                Payee = "付强",
                Checker = "王宝宝",
                Signature = string.Empty,
                DeductibleAmount = string.Empty,
                OriginalInvoiceCode = string.Empty,
                OriginalInvoiceNo = string.Empty,
                GoodsInfos = Enumerable.Range(0, 2).Select(f => new GoodsInfo
                {
                    LineNo = f,
                    LineNature = 0,
                    Item = "*谷物*稻谷",
                    Code = "1010101010000000000",
                    Specification = string.Empty,
                    MeasurementDimension = "克",
                    Price = "97.09",
                    Quantity = "1",
                    Amount = "97.09",
                    TaxScheme = "3%",
                    TaxAmount = "2.91",
                    PreferentialMark = string.Empty,
                    FreeTaxMark = string.Empty,
                    VATSpecialManagement = string.Empty
                }).ToList()
            };
            return invoiceInfo;
        }


        private static XElement CreateInvoiceElement(InvoiceInfo invoiceInfo)
        {
            XNamespace invoiceNamespace = "http://www.edrm.org.cn/schema/e-invoice/2019";
            string invoiceXmls = "http://www.edrm.org.cn/schema/e-invoice/2019";
            XElement element = new XElement("eInvoice", new XAttribute(XNamespace.Xmlns + "fp", invoiceXmls), new XAttribute("Version", "1.0"));

            element.Add(new XElement(invoiceNamespace + "DocID", invoiceInfo.DocId));
            element.Add(new XElement(invoiceNamespace + "AreaCode", invoiceInfo.AreaCode));
            element.Add(new XElement(invoiceNamespace + "TypeCode", invoiceInfo.TypeCode));
            element.Add(new XElement(invoiceNamespace + "InvoiceSIA2", invoiceInfo.InvoiceSIA2));
            element.Add(new XElement(invoiceNamespace + "InvoiceSIA1", invoiceInfo.InvoiceSIA1));
            element.Add(new XElement(invoiceNamespace + "InvoiceCode", invoiceInfo.InvoiceCode));
            element.Add(new XElement(invoiceNamespace + "InvoiceNo", invoiceInfo.InvoiceNo));
            element.Add(new XElement(invoiceNamespace + "IssueDate", new XCData(invoiceInfo.IssueDate)));
            element.Add(new XElement(invoiceNamespace + "InvoiceCheckCode", new XCData(invoiceInfo.InvoiceCheckCode)));
            element.Add(new XElement(invoiceNamespace + "MachineNo", invoiceInfo.MachineNo));
            element.Add(new XElement(invoiceNamespace + "GraphCode", new XCData(invoiceInfo.GraphCode)));
            element.Add(new XElement(invoiceNamespace + "TaxControlCode", new XCData(invoiceInfo.TaxControlCode)));

            XElement buyerElement = new XElement(invoiceNamespace + "Buyer");
            buyerElement.Add(new XElement("BuyerName", new XCData(invoiceInfo.Buyer.BuyerName)));
            buyerElement.Add(new XElement("BuyerTaxID", invoiceInfo.Buyer.BuyerTaxNo));
            buyerElement.Add(new XElement("BuyerAddrTel", new XCData(invoiceInfo.Buyer.BuyerAddressTel)));
            buyerElement.Add(new XElement("BuyerFinancialAccount", new XCData(invoiceInfo.Buyer.BuyerBankAccount)));
            element.Add(buyerElement);

            XElement sellerElement = new XElement(invoiceNamespace + "Seller");
            sellerElement.Add(new XElement("SellerName", new XCData(invoiceInfo.Seller.SellerName)));
            sellerElement.Add(new XElement("SellerTaxID", invoiceInfo.Seller.SellerTaxNo));
            sellerElement.Add(new XElement("SellerAddrTel", new XCData(invoiceInfo.Seller.SellerAddressTel)));
            sellerElement.Add(new XElement("SellerFinancialAccount", new XCData(invoiceInfo.Seller.SellerBankAccount)));
            element.Add(sellerElement);

            element.Add(new XElement(invoiceNamespace + "TaxInclusiveTotalAmount", invoiceInfo.TaxInclusiveTotalAmount));
            element.Add(new XElement(invoiceNamespace + "TaxInclusiveTotalAmountWithWords", invoiceInfo.TaxInclusiveTotalAmountWithWords));
            element.Add(new XElement(invoiceNamespace + "TaxExclusiveTotalAmount", invoiceInfo.TaxExclusiveTotalAmount));
            element.Add(new XElement(invoiceNamespace + "TaxTotalAmount", invoiceInfo.TaxTotalAmount));
            element.Add(new XElement(invoiceNamespace + "Note", new XCData(invoiceInfo.Note)));
            element.Add(new XElement(invoiceNamespace + "InvoiceClerk", new XCData(invoiceInfo.InvoiceClerk)));
            element.Add(new XElement(invoiceNamespace + "Payee", new XCData(invoiceInfo.Payee)));
            element.Add(new XElement(invoiceNamespace + "Checker", new XCData(invoiceInfo.Checker)));
            element.Add(new XElement(invoiceNamespace + "Signature", invoiceInfo.Signature));
            element.Add(new XElement(invoiceNamespace + "DeductibleAmount", invoiceInfo.DeductibleAmount));
            element.Add(new XElement(invoiceNamespace + "OriginalInvoiceCode", invoiceInfo.OriginalInvoiceCode));
            element.Add(new XElement(invoiceNamespace + "OriginalInvoiceNo", invoiceInfo.OriginalInvoiceNo));

            XElement goodsElement = new XElement(invoiceNamespace + "GoodsInfos");
            foreach (var goods in invoiceInfo.GoodsInfos)
            {
                XElement goodsInfoElement = new XElement(invoiceNamespace + "GoodsInfo");
                goodsInfoElement.Add(new XElement(invoiceNamespace + "LineNo", goods.LineNo));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "LineNature", goods.LineNature));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "Item", new XCData(goods.Item)));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "Code", goods.Code));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "Specification", goods.Specification));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "MeasurementDimension", new XCData(goods.MeasurementDimension)));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "Price", goods.Price));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "Quantity", goods.Quantity));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "Amount", goods.Amount));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "TaxScheme", goods.TaxScheme));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "TaxAmount", goods.TaxAmount));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "PreferentialMark", goods.PreferentialMark));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "FreeTaxMark", goods.FreeTaxMark));
                goodsInfoElement.Add(new XElement(invoiceNamespace + "VATSpecialManagement", goods.VATSpecialManagement));
                goodsElement.Add(goodsInfoElement);
            }
            element.Add(goodsElement);

            XElement systemInfoElement = new XElement(invoiceNamespace + "SystemInfos");
            systemInfoElement.Add(new XElement(invoiceNamespace + "SystemCode", "bw"));
            systemInfoElement.Add(new XElement(invoiceNamespace + "SystemName", "百望云"));
            systemInfoElement.Add(new XElement(invoiceNamespace + "SystemType", "2"));
            element.Add(systemInfoElement);

            return element;
        }

        private static XElement CreateInvoiceTagElement()
        {
            XNamespace invoiceNamespace = "http://www.edrm.org.cn/schema/e-invoice/2019";
            string invoiceXmls = "http://www.edrm.org.cn/schema/e-invoice/2019";
            XElement element = new XElement("eInvoice", new XAttribute(XNamespace.Xmlns + "fp", invoiceXmls));
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
    }
}