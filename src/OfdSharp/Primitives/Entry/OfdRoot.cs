using System.Collections.Generic;

namespace OfdSharp.Primitives.Entry
{
    /// <summary>
    /// xml序列化
    /// </summary>
    public class OfdRoot
    {
        /// <summary>
        /// 文件格式的版本号
        /// 固定值： 1.0
        /// </summary>
        public string Version => "1.0";

        /// <summary>
        /// 文件格式子集类型，取值为“OFD”，表明此文件符合本标准。
        /// </summary>
        public string DocType => "OFD";

        /// <summary>
        /// 文档正文，文件对象入口,可以存在多个,以便在一个文档中包含多个版式文档
        /// </summary>
        public List<DocBody> DocBodyList { get; } = new List<DocBody>();
    }
}