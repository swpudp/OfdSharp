﻿using OfdSharp.Primitives.Signature;

namespace OfdSharp.Primitives.Signatures
{
    /// <summary>
    /// 针对一个文件的摘要节点
    /// </summary>
    public class Reference
    {
        /// <summary>
        /// 指向包内的文件，使用绝对路径
        /// </summary>
        public string FileRef { get; set; }

        /// <summary>
        /// 对包内文件进行摘要计算值的杂凑值base64 编码
        /// </summary>
        public CheckValue CheckValue { get; set; }
    }
}
