using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace OfdSharp.Core.Basic.Res.Resources
{
  public  class FontCollect:OfdElement
    {
        public FontCollect(XmlDocument xmlDocument) : base(xmlDocument, "Fonts")
        {
        }
    }
}
