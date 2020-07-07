using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace OfdSharp.Core.CompositeObj
{
    /// <summary>
    /// 复合对象
    /// 复合对象是一种特殊的图元对象，拥有图元对象的一切特性，
    /// 但其内容在ResourceID指向的矢量图像资源中进行描述，
    /// 一个资源可以被多个复合对象所引用。通过这种方式可实现对
    /// 文档内矢量图文内容的复用。
    /// </summary>
    public class Composite : OfdElement
    {
        public Composite(XmlDocument xmlDocument, string name) : base(xmlDocument, name)
        {
        }

        public string CompositeObject { get; set; }
    }
}
