using System.Xml;

namespace OfdSharp.Core.Action
{
    public abstract class BaseAction : OfdElement
    {
        protected BaseAction(XmlDocument xmlDocument, string name) : base(xmlDocument, name)
        {
        }
    }
}
