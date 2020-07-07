using OfdSharp.Core.Basic.PageObj.Layer;
using System.Xml;

namespace OfdSharp.Core.Basic.PageObj
{
    /// <summary>
    /// 模板页
    /// ————《GB/T 33190-2016》 图 14
    /// </summary>
    public class TemplatePage : OfdElement
    {
        public TemplatePage(XmlDocument xmlDocument) : base(xmlDocument, "TemplatePage")
        {
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public LayerType ZOrder { get; set; }

        public string BaseLoc { get; set; }
    }
}
