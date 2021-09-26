using OfdSharp.Primitives.PageDescription;

namespace OfdSharp.Primitives.CompositeObj
{
    /// <summary>
    /// 复合对象：复合对象是一种特殊的图元对象，拥有图元对象的一切特性，
    /// 但其内容在ResourceID指向的矢量图像资源中进行描述，
    /// 一个资源可以被多个复合对象所引用。通过这种方式可实现对文档内矢量图文内容的复用。
    /// </summary>
    public class CompositeObject : GraphicUnit
    {
        /// <summary>
        /// 引用资源文件中定义的矢量图像的标识
        /// </summary>
        public RefId ResourceId { get; set; }
    }
}
