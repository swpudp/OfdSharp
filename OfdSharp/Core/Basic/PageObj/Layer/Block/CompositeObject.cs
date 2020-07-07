using System.Xml;

namespace OfdSharp.Core.Basic.PageObj.Layer.Block
{
    /// <summary>
    /// 复合对象
    /// </summary>
    public class CompositeObject : OfdElement
    {
        public CompositeObject(XmlDocument xmlDocument) : base(xmlDocument, "CompositeObject")
        {
        }

        public string Id { get; set; }
    }
}
