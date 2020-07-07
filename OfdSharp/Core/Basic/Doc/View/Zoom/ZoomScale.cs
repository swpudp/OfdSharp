using System.Xml;

namespace OfdSharp.Core.Basic.Doc.View.Zoom
{
    /// <summary>
    /// 缩放比例选择对象基类
    /// </summary>
    public class ZoomScale : OfdElement
    {
        public ZoomScale(XmlDocument xmlDocument, string name) : base(xmlDocument, name)
        {
        }
    }
}
