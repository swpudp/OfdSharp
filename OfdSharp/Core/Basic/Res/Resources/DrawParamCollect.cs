using OfdSharp.Core.PageDescription.DrawParam;
using System.Collections.Generic;
using System.Xml;

namespace OfdSharp.Core.Basic.Res.Resources
{
    /// <summary>
    /// 包含了一组绘制参数的描述
    /// </summary>
    public class DrawParamCollect : OfdElement
    {
        public DrawParamCollect(XmlDocument xmlDocument) : base(xmlDocument, "DrawParams")
        {
        }

        /// <summary>
        /// 绘制参数描述序列
        /// </summary>
        public IList<CtDrawParam> DrawParams { get; set; }
    }
}
