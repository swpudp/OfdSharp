using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace OfdSharp.Core.PageDescription.Clips
{
  public  class ClipAble:OfdElement
    {
        public ClipAble(XmlDocument xmlDocument, string name) : base(xmlDocument, name)
        {
        }
    }
}
