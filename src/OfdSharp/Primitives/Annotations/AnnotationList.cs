using System.Collections.Generic;

namespace OfdSharp.Primitives.Annotations
{
    /// <summary>
    /// 注释是版式文档形成后附加的图文信息,用户可通过鼠标或键盘与其进行交互。
    /// 本标准中,页面内容与注释内容是分文件描述的。文档的注释在注释列表文件中按照页面进行组织索引,注释的内容在分页注释文件中描述
    /// </summary>
    public class AnnotationList
    {
        /// <summary>
        /// 注释所在页
        /// </summary>
        public IList<RefPage> Pages { get; set; }
    }
}
