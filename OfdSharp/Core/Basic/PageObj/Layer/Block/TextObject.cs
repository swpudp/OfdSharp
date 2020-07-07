using System.Xml;

namespace OfdSharp.Core.Basic.PageObj.Layer.Block
{
    /// <summary>
    /// 文字对象
    /// </summary>
    public class TextObject : PageBlockType
    {
        public TextObject(XmlDocument xmlDocument) : base(xmlDocument, "TextObject")
        {
        }

        public string Id { get; set; }
    }
}
