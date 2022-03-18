﻿using System;
using System.Collections.Generic;

namespace OfdSharp.Primitives.Entry
{
    /// <summary>
    /// 文档元数据信息描述
    /// </summary>
    public class DocInfo
    {
        /// <summary>
        /// 文件标识符,采用UUID算法生成的32个字符组成的文件标识。每个DocID在文档创建或生成的时候分配
        /// </summary>
        public string DocId { get; set; }

        /// <summary>
        /// 文档标题。标题可以与文件名不同
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文档作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 文档主题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 文档摘要与注释
        /// </summary>
        public string Abstract { get; set; }

        /// <summary>
        /// 文件创建日期
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// 文档最近修改日期
        /// </summary>
        public DateTime ModDate { get; set; }

        /// <summary>
        /// 文档分类
        /// </summary>
        public DocUsage DocUsage { get; set; }

        /// <summary>
        /// 文档封面，此路径指向一个图片文件
        /// </summary>
        public string Cover { get; set; }

        /// <summary>
        /// 关键词集合
        /// 每一个关键词用一个“Keyword”子节点来表达
        /// </summary>
        public List<string> Keywords { get; set; }

        /// <summary>
        /// 创建文档的应用程序
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建文档的应用程序版本信息
        /// </summary>
        public string CreatorVersion { get; set; }

        /// <summary>
        /// 用户自定义元数据集合
        /// </summary>
        public List<CustomData> CustomDataList { get; set; }
    }
}
