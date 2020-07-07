using System;
using System.Xml;

namespace OfdSharp.Core.Basic.Doc.View.Zoom
{
    /// <summary>
    /// 自动缩放模式
    /// </summary>
    public class ZoomMode : ZoomScale
    {
        public ZoomMode(XmlDocument xmlDocument, string type) : base(xmlDocument, "ZoomMode")
        {
            Element.InnerText = type;
        }

        public ZoomType Type => Enum.TryParse<ZoomType>(Element.InnerText, out ZoomType type) ? type : ZoomType.Default;
    }
}
