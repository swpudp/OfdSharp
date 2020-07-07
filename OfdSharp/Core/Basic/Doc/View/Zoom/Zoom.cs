using System.Xml;

namespace OfdSharp.Core.Basic.Doc.View.Zoom
{

    /// <summary>
    /// 文档的缩放率
    /// </summary>
    public class Zoom : ZoomScale
    {
        public Zoom(XmlDocument xmlDocument, double value) : base(xmlDocument, "Zoom")
        {
            Value = value;
        }

        public double Value { get; }
    }
}
