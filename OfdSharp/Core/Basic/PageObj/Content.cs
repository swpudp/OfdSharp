using System.Collections.Generic;
using System.Xml;

namespace OfdSharp.Core.Basic.PageObj
{
    public class Content : OfdElement
    {
        public Content(XmlDocument xmlDocument) : base(xmlDocument, "Content")
        {
        }

        /// <summary>
        /// 层节点
        /// </summary>
        public IList<Layer.Layer> Layers { get; set; }
    }
}
