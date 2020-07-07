using System.Xml;

namespace OfdSharp.Core.Basic.PageTree
{
    public  class Pages:OfdElement
    {
        public Pages(XmlDocument xmlDocument) : base(xmlDocument, "Pages")
        {
        }

        public Page Page { get; set; }
    }
}
