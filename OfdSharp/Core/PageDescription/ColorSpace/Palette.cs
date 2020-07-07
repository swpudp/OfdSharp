using System;
using System.Xml;

namespace OfdSharp.Core.PageDescription.ColorSpace
{
    /// <summary>
    /// 调色板
    /// 调色板中颜色的索引编号从 0 开始
    /// </summary>
    public class Palette : OfdElement
    {
        public Palette(XmlDocument xmlDocument) : base(xmlDocument, "Palette")
        {
        }

        public ColorValue GetByIndex(int index)
        {
            throw new NotImplementedException();
        }
    }
}
