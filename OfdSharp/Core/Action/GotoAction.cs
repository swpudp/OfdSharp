using System.Xml;

namespace OfdSharp.Core.Action
{
    public class GotoAction : BaseAction
    {
        public GotoAction(XmlDocument xmlDocument) : base(xmlDocument, "Goto")
        {
        }
    }
}
