using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using OfdSharp.Core.Basic.PageObj.Layer.Block;

namespace OfdSharp.Core.Graph.PathObj
{
    /// <summary>
    /// 图形对象
    /// 图形对象具有一般图元的一切属性和行为特征。
    /// </summary>
    public class Path : OfdElement
    {
        public Path(XmlDocument xmlDocument) : base(xmlDocument, "Path")
        {
        }

        /// <summary>
        /// 对象ID
        /// </summary>
        public PathObject PathObject { get; set; }
    }
}
