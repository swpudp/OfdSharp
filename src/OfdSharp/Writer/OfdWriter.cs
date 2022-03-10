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
        private MemoryStream _root;


        public void WriteOfdRoot()
        {
            XNamespace xNamespace = XNamespace.Get("http://www.ofdspec.org/2016");
            //XmlNamespaceManager oManager = new XmlNamespaceManager(new NameTable());
            //oManager.AddNamespace("ofd", xNamespace.NamespaceName);
            XDeclaration declaration = new XDeclaration("1.0", "UTF-8", string.Empty);
            //XDocument document = new XDocument(declaration);


            XElement root = new XElement(XName.Get("ofd", xNamespace.NamespaceName));
            root.SetAttributeValue("DocType", "OFD");
            //root.SetAttributeValue(XName.Get("ofd", XNamespace.Xmlns.NamespaceName), "ofd");

            XNamespace xmlns = "XSDName";
            XNamespace xsi = @"http://www.w3.org/2001/XMLSchema-instance";
            XNamespace schemaloc = @"XSDName XSDName.xsd";
            XDocument document = new XDocument(
                new XElement(xmlns + "BaseReport",
                new XAttribute(xsi + "schemaLocation", schemaloc),
                new XAttribute(XNamespace.Xmlns + "ns1", xmlns),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi)));

            document.Add(root);
            _root = new MemoryStream();
            document.Save(_root);
        }

        public byte[] Flush()
        {
            return _root.ToArray();
        }
    }
}
