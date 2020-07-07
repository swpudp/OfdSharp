using System.Xml;

namespace OfdSharp.Core
{
    public abstract class OfdElement
    {
        protected readonly XmlElement Element;

        protected OfdElement(XmlDocument xmlDocument, string name)
        {
            Element = xmlDocument.CreateElement(name);
        }
    }
}