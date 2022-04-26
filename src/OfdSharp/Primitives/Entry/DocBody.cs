﻿namespace OfdSharp.Primitives.Entry
{
    /// <summary>
    /// 文档正文
    /// </summary>
    public class DocBody
    {
        /// <summary>
        /// 文档信息
        /// </summary>
        public CtDocInfo DocInfo { get; set; }

        /// <summary>
        /// 文档入口文件
        /// </summary>
        public CtLocation DocRoot { get; set; }

        /// <summary>
        /// 签名入口
        /// </summary>
        public CtLocation Signatures { get; set; }
    }
}
