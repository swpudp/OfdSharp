using System.Xml;

namespace OfdSharp.Core.Annotation
{
    public class Annotations : OfdElement
    {
        public Annotations(XmlDocument xmlDocument) : base(xmlDocument, "Annotations")
        {
        }
    }
}
