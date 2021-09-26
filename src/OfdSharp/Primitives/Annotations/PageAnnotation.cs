using System.Collections.Generic;

namespace OfdSharp.Primitives.Annotations
{
    /// <summary>
    /// 分页注释
    /// </summary>
    public class PageAnnotation
    {
        /// <summary>
        /// 注释信息列表
        /// </summary>
        public IList<AnnotationInfo> Annotations { get; set; }
    }
}
