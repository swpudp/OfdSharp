using System.Collections.Generic;
using System.Xml;

namespace OfdSharp.Core.Basic.Outlines
{
    /// <summary>
    /// 大纲按照树形结构进行组织
    /// </summary>
    public class OutlineCollect
    {
        /// <summary>
        /// 大纲节点集合
        /// </summary>
        public IList<OutlineElem> OutlineElems { get; set; }
    }
}
