using OfdSharp.Primitives.Ofd;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace OfdSharp.Writer
{
    public class OfdWriter
    {
        private XElement _root;


        public void WriteOfdRoot()
        {
            XNamespace aw = "http://www.ofdspec.org/2016";
            XElement docInfo = new XElement(aw + "DocInfo");
            docInfo.Add(new XElement(aw + "DocID", "c9a4ced42f284320964bf1e630d1fa2a"));
            docInfo.Add(new XElement(aw + "Author", "China Tax"));
            docInfo.Add(new XElement(aw + "CreationDate", "2020-11-16"));
            List<CustomData> customDatas = new List<CustomData>
            {
                new CustomData { Name = "template-version", Value = "1.0.20.0422" },
                new CustomData { Name = "native-producer", Value = "SuwellFormSDK" },
                new CustomData { Name = "producer-version", Value = "1.0.20.0603" },
                new CustomData { Name = "发票代码", Value = "032001900311" },
                new CustomData { Name = "发票号码", Value = "25577301" },
                new CustomData { Name = "合计税额", Value = "***" },
                new CustomData { Name = "合计金额", Value = "181.14" },
                new CustomData { Name = "开票日期", Value = "2020年11月12日" },
                new CustomData { Name = "校验码", Value = "58569 30272 33709 75117" },
                new CustomData { Name = "购买方纳税人识别号", Value = "91510700205412308D" },
                new CustomData { Name = "销售方纳税人识别号", Value = "91320111339366503A" }
            };
            XElement customDataElement = new XElement(aw + "CustomDatas");
            foreach (var item in customDatas)
            {
                customDataElement.Add(new XElement(aw + "CustomData", new XAttribute("Name", item.Name), item.Value));
            }
            docInfo.Add(customDataElement);
            XElement docBody = new XElement(aw + "DocBody", docInfo);
            docBody.Add(new XElement(aw + "DocRoot", "Doc_0/Document.xml"));
            docBody.Add(new XElement(aw + "Signatures", "Doc_0/Signs/Signatures.xml"));
            _root = new XElement(aw + "OFD", new XAttribute(XNamespace.Xmlns + "ofd", "http://www.ofdspec.org/2016"), new XAttribute("DocType", "OFD"), new XAttribute("Version", "1.1"), docBody);
        }

        public byte[] Flush()
        {
            using (var stream = new MemoryStream())
            {
                _root.Save(stream);
                return stream.ToArray();
            }
        }
    }
}
