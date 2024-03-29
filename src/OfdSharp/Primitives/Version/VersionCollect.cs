﻿using System.Collections.Generic;

namespace OfdSharp.Primitives.Version
{
    /// <summary>
    /// 一个OFD文档可能有多个版本，版本序列
    /// </summary>
    public class VersionCollect
    {
        /// <summary>
        /// 版本描述入口列表
        /// </summary>
        public List<Version> Versions { get; set; }
    }
}
