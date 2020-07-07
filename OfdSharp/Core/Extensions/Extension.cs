using System;
using System.Collections.Generic;
using System.Xml;

namespace OfdSharp.Core.Extensions
{
    /// <summary>
    /// 扩展信息节点
    /// </summary>
    public class Extension : OfdElement
    {
        public Extension(XmlDocument xmlDocument) : base(xmlDocument, "Extension")
        {
        }

        /// <summary>
        /// 用于生成或解释该自定义对象数据的扩展应用程序名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 形成此扩展信息的软件厂商标识
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 形成此扩展信息的软件版本
        /// </summary>
        public string AppVersion { get; set; }

        /// <summary>
        /// 形成此扩展信息的日期时间
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 引用扩展项针对的文档项目的标识
        /// </summary>
        public string RefId { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public IList<Property> Property { get; set; }

        /// <summary>
        /// 扩展数据文件所在位置
        /// </summary>
        public string ExtendData { get; set; }
    }
}
