using OfdSharp.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace OfdSharp.Extensions
{
    /// <summary>
    /// Extension methods for the XElement class
    /// </summary>
    public static class XmlExtension
    {
        /// <summary>
        /// Gets the outer XML from the XElement
        /// </summary>
        /// <param name="element">the element</param>
        /// <returns>the Outer XML</returns>
        public static string OuterXml(this XElement element)
        {
            if (element == null)
            {
                throw new NullReferenceException("Cannot call this extension method on a null object.");
            }
            XmlReader xReader = element.CreateReader();
            xReader.MoveToContent();
            return xReader.ReadOuterXml();
        }

        /// <summary>
        /// Gets the Inner XML of an XElement
        /// </summary>
        /// <param name="element">the element</param>
        /// <returns>the inner XML</returns>
        public static string InnerXml(this XElement element)
        {
            if (element == null)
            {
                throw new NullReferenceException("Cannot call this extension method on a null object.");
            }
            XmlReader xReader = element.CreateReader();
            xReader.MoveToContent();
            return xReader.ReadInnerXml();
        }

        /// <summary>
        /// Allows a safe way to retrieve attribute values from an element
        /// </summary>
        /// <param name="element">A reference to the element object</param>
        /// <param name="attributeName">The name of the attribute</param>
        /// <returns>The attribute content or null</returns>
        public static string AttributeValueOrDefault(this XElement element, string attributeName)
        {
            return element?.Attribute(attributeName)?.Value;
        }

        /// <summary>
        /// Allows a safe way to retrieve the value of an attribute for a child element in the current element.
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="childElement">the name of the child element</param>
        /// <param name="attributeName">the name of the attribute whose value you want</param>
        /// <returns>the converted attribute value or default(T)</returns>
        public static string AttributeValueForElementOrDefault(this XElement parentElement, string childElement, string attributeName)
        {
            if (parentElement == null)
            {
                return null;
            }
            XNamespace rootNamespace = parentElement.Name.Namespace;
            XName xName = XName.Get(childElement, rootNamespace.NamespaceName);
            XElement child = parentElement.Element(xName);
            return child?.Attribute(attributeName)?.Value;
        }

        /// <summary>
        /// Allows a safe way to retrieve the value of a nested element for a child element in the current element.
        /// </summary>
        public static string ElementValueForElementOrDefault(this XElement rootElement, string parentElementName, string childElementName)
        {
            if (rootElement == null)
            {
                return null;
            }
            XNamespace rootNamespace = rootElement.Name.Namespace;
            XName xName = XName.Get(parentElementName, rootNamespace.NamespaceName);
            XElement parentElement = rootElement.Element(xName);
            if (parentElement == null)
            {
                return null;
            }
            XName childName = XName.Get(childElementName, rootNamespace.NamespaceName);
            return parentElement.Element(childName)?.Value;
        }

        /// <summary>
        /// Allows a safe way to retrieve the value of a nested element for a child element in the current element.
        /// </summary>
        public static string ElementValueForElementOrDefault(this XElement rootElement, XName xName, XName childName)
        {
            if (rootElement == null)
            {
                return null;
            }
            XElement parentElement = rootElement.Element(xName);
            if (parentElement == null)
            {
                return null;
            }
            return parentElement.Element(childName)?.Value;
        }

        /// <summary>
        /// Converts an XmlNode to an XElement for LINQ queries
        /// </summary>
        public static XElement GetChildElement(this XElement rootElement, string elementName)
        {
            if (rootElement == null)
            {
                return null;
            }
            XNamespace rootNamespace = rootElement.Name.Namespace;
            XName xName = XName.Get(elementName, rootNamespace.NamespaceName);
            return rootElement.Element(xName);
        }

        /// <summary>
        /// Allows a safe way to retrieve element data
        /// </summary>
        /// <param name="element">A reference to the element object</param>
        /// <param name="childElement"></param>
        /// <returns>Element content or an empty string</returns>
        public static string ElementValueOrDefault(this XElement element, string childElement)
        {
            if (element == null)
            {
                return null;
            }
            XNamespace elementNamespace = element.Name.Namespace;
            XName xName = XName.Get(childElement, elementNamespace.NamespaceName);
            return element.Element(xName)?.Value;
        }

        /// <summary>
        /// Allows a safe way to retrieve element data
        /// </summary>
        /// <param name="element">A reference to the element object</param>
        /// <param name="xName"></param>
        /// <returns>Element content or an empty string</returns>
        public static string ElementValueOrDefault(this XElement element, XName xName)
        {
            if (element == null)
            {
                return null;
            }
            return element.Element(xName)?.Value;
        }

        /// <summary>
        /// 下一个节点内容
        /// </summary>
        /// <param name="element"></param>
        /// <param name="childElementName"></param>
        /// <returns></returns>
        public static string ElementValue(this XElement element, string childElementName)
        {
            XNamespace rootNamespace = element.Name.Namespace;
            XName xName = XName.Get(childElementName, rootNamespace.NamespaceName);
            return element.Element(xName)?.Value;
        }

        /// <summary>
        /// Converts an XmlNode to an XDocument for LINQ queries
        /// </summary>
        public static XDocument GetXDocument(this XmlNode node)
        {
            if (node == null)
            {
                throw new NullReferenceException("Cannot call this extension method on a null object.");
            }
            XDocument xDoc = new XDocument();
            using (XmlWriter xmlWriter = xDoc.CreateWriter())
            {
                node.WriteTo(xmlWriter);
            }
            return xDoc;
        }

        /// <summary>
        /// Converts an XmlNode to an XElement for LINQ queries
        /// </summary>
        public static XElement GetXElement(this XmlNode node)
        {
            if (node == null)
            {
                throw new NullReferenceException("Cannot call this extension method on a null object.");
            }
            XDocument xDoc = new XDocument();
            using (XmlWriter xmlWriter = xDoc.CreateWriter())
            {
                node.WriteTo(xmlWriter);
            }
            return xDoc.Root;
        }

        /// <summary>
        /// Converts an XElement to an XmlNode
        /// </summary>
        public static XmlNode GetXmlNode(this XElement element)
        {
            if (element == null)
            {
                throw new NullReferenceException("Cannot call this extension method on a null object.");
            }
            using (XmlReader xmlReader = element.CreateReader())
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);
                return xmlDoc;
            }
        }

        /// <summary>
        /// 获取一个节点内容
        /// </summary>
        /// <param name="document"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static string FirstValueOrDefault(this XDocument document, string nodeName)
        {
            return document.GetDescendants(nodeName).FirstOrDefault()?.Value;
        }

        /// <summary>
        /// 获取后代节点
        /// </summary>
        /// <param name="document"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static IEnumerable<XElement> GetDescendants(this XDocument document, string nodeName)
        {
            XNamespace rootNamespace = document.Document?.Root?.Name.Namespace;
            XName xName = rootNamespace == null ? XName.Get(nodeName) : XName.Get(nodeName, rootNamespace.NamespaceName);
            return document.Descendants(xName);
        }

        /// <summary>
        /// 获取后代节点
        /// </summary>
        /// <param name="document"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static IEnumerable<XElement> GetDescendants(this XElement document, string nodeName)
        {
            XNamespace rootNamespace = document.Document?.Root?.Name.Namespace;
            XName xName = rootNamespace == null ? XName.Get(nodeName) : XName.Get(nodeName, rootNamespace.NamespaceName);
            return document.Descendants(xName);
        }

        /// <summary>
        /// 添加可选节点
        /// </summary>
        /// <param name="element"></param>
        /// <param name="nodeName"></param>
        /// <param name="value"></param>
        public static void AddOptionalElement(this XElement element, string nodeName, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }
            element.Add(new XElement(ConstDefined.OfdNamespace + nodeName, value));
        }

        /// <summary>
        /// 添加可选节点
        /// </summary>
        /// <param name="element"></param>
        /// <param name="nodeName"></param>
        /// <param name="value"></param>
        public static void AddOptionalElement(this XElement element, string nodeName, DateTime? value, string format = null)
        {
            if (!value.HasValue)
            {
                return;
            }
            element.Add(new XElement(ConstDefined.OfdNamespace + nodeName, value.Value.ToString(format ?? ConstDefined.DatetimeFormatter)));
        }

        /// <summary>
        /// 添加必选节点
        /// </summary>
        /// <param name="element"></param>
        /// <param name="nodeName"></param>
        /// <param name="value"></param>
        public static void AddRequiredElement(this XElement element, string nodeName, string value)
        {
            element.Add(new XElement(ConstDefined.OfdNamespace + nodeName, value));
        }

        /// <summary>
        /// 添加必选节点
        /// </summary>
        /// <param name="element"></param>
        /// <param name="nodeName"></param>
        /// <param name="value"></param>
        public static void AddRequiredElement(this XElement element, string nodeName, XElement value)
        {
            element.Add(new XElement(ConstDefined.OfdNamespace + nodeName, value));
        }

        /// <summary>
        /// 添加必选节点
        /// </summary>
        /// <param name="element"></param>
        /// <param name="nodeName"></param>
        /// <param name="value"></param>
        public static void AddRequiredElement(this XElement element, string nodeName, Id value)
        {
            element.Add(new XElement(ConstDefined.OfdNamespace + nodeName, value));
        }

        /// <summary>
        /// 添加必选节点
        /// </summary>
        /// <param name="element"></param>
        /// <param name="nodeName"></param>
        /// <param name="value"></param>
        public static void AddRequiredElement(this XElement element, string nodeName, string value, params XAttribute[] attributes)
        {
            XElement nodeElement = new XElement(ConstDefined.OfdNamespace + nodeName, value);
            foreach (XAttribute attribute in attributes)
            {
                nodeElement.Add(attribute);
            }
            element.Add(nodeElement);
        }

        /// <summary>
        /// 添加必选节点
        /// </summary>
        /// <param name="element"></param>
        /// <param name="nodeName"></param>
        /// <param name="value"></param>
        public static void AddRequiredElement(this XElement element, string nodeName, params XAttribute[] attributes)
        {
            XElement nodeElement = new XElement(ConstDefined.OfdNamespace + nodeName);
            foreach (XAttribute attribute in attributes)
            {
                nodeElement.Add(attribute);
            }
            element.Add(nodeElement);
        }

        /// <summary>
        /// 创建节点
        /// </summary>
        /// <param name="nodeName"></param>
        public static XElement CreateElement(string nodeName)
        {
            return new XElement(ConstDefined.OfdNamespace + nodeName);
        }

        /// <summary>
        /// 创建节点
        /// </summary>
        /// <param name="nodeName"></param>
        public static XElement CreateElement(string nodeName, string value)
        {
            return new XElement(ConstDefined.OfdNamespace + nodeName, value);
        }

        /// <summary>
        /// 创建节点
        /// </summary>
        /// <param name="nodeName"></param>
        public static XElement CreateElement(string nodeName, XElement value, params XAttribute[] attributes)
        {
            return new XElement(ConstDefined.OfdNamespace + nodeName, attributes, value);
        }

        /// <summary>
        /// 创建节点
        /// </summary>
        /// <param name="nodeName"></param>
        public static XElement CreateElement(string nodeName, params XAttribute[] attributes)
        {
            return new XElement(ConstDefined.OfdNamespace + nodeName, attributes);
        }
    }
}
