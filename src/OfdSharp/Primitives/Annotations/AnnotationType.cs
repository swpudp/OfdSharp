namespace OfdSharp.Primitives.Annotations
{
    /// <summary>
    /// 注释类型
    /// </summary>
    public enum AnnotationType
    {
        /// <summary>
        /// 链接注释
        /// </summary>
        Link,

        /// <summary>
        /// 路径注释,一般为图形对象,比如矩形、多边形、贝塞尔曲线等
        /// </summary>
        Path,

        /// <summary>
        /// 高亮注释
        /// </summary>
        Highlight,

        /// <summary>
        /// 签章注释
        /// </summary>
        Stamp,

        /// <summary>
        /// 水印注释
        /// </summary>
        Watermark
    }
}
