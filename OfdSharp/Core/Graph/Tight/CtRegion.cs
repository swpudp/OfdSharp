using System.Collections.Generic;
using System.Xml;

namespace OfdSharp.Core.Graph.Tight
{
    /// <summary>
    /// 图形也可以使用 XML 负载类型的方式进行描述，这种方式主要用于
    /// 区域（Region）。区域由一系列的分路径（Area）组成，每个路径都是闭合的.
    /// </summary>
    public class CtRegion : OfdElement
    {
        public CtRegion(XmlDocument xmlDocument) : base(xmlDocument, "Region")
        {
        }

        /// <summary>
        /// 区域中所有分路径
        /// </summary>
        public IList<CtArea> Areas { get; set; }
    }
}
