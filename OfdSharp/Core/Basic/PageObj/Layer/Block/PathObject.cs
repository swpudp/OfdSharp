using System.Xml;

namespace OfdSharp.Core.Basic.PageObj.Layer.Block
{
    /// <summary>
    /// 图形对象
    /// </summary>
    public class PathObject : PageBlockType
    {
        public PathObject(XmlDocument xmlDocument) : base(xmlDocument, "PathObject")
        {
        }

        public string Id { get; set; }
    }
}
