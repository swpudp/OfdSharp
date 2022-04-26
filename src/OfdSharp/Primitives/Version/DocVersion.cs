using System;

namespace OfdSharp.Primitives.Version
{
    /// <summary>
    /// 版本
    /// </summary>
    public class DocVersion 
    {
        /// <summary>
        /// 版本标识符
        /// </summary>
        public CtId Id { get; set; }

        /// <summary>
        /// 该文件适用的格式版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 版本名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// 该版本的入口文件
        /// </summary>
        public CtLocation DocRoot { get; set; }

        /// <summary>
        /// 文件列表文件描述
        /// </summary>
        public FileList FileList { get; set; }
    }
}
