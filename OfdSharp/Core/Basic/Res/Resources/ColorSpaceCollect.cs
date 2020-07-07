using System.Collections.Generic;
using System.Xml;
using OfdSharp.Core.PageDescription.ColorSpace;

namespace OfdSharp.Core.Basic.Res.Resources
{
    /// <summary>
    /// 包含了一组颜色空间的描述
    /// </summary>
    public class ColorSpaceCollect : OfdElement
    {
        public ColorSpaceCollect(XmlDocument xmlDocument) : base(xmlDocument, "ColorSpaces")
        {
        }

        public IList<CtColorSpace> ColorSpaces { get; set; }
    }
}
