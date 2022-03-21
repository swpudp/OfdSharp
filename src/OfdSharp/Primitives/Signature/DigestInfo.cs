﻿namespace OfdSharp.Primitives.Signature
{
    /// <summary>
    /// 文件签名摘要信息
    /// </summary>
    public class DigestInfo
    {
        /// <summary>
        /// 签名信息
        /// </summary>
        public SignedInfo SignedInfo { get; set; }

        /// <summary>
        /// 签名值
        /// </summary>
        public string SignedValue { get; set; }
    }
}
