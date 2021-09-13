using System.Collections.Generic;

namespace OfdSharp.Core.Version
{
    /// <summary>
    /// 一个OFD文档可能有多个版本，版本序列
    /// </summary>
    public class VersionCollect
    {
        /// <summary>
        /// 版本描述入口列表
        /// </summary>
        public IList<Version> Versions { get; set; }
    }
}
