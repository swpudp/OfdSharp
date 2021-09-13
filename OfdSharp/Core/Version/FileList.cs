using System.Collections.Generic;

namespace OfdSharp.Core.Version
{
    /// <summary>
    /// 版本包含的文件列表
    /// </summary>
    public class FileList
    {
        /// <summary>
        /// 文件列表文件描述
        /// </summary>
        public IList<File> Files { get; set; }
    }
}
