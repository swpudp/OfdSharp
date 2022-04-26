using System;
using System.Collections.Generic;

namespace OfdSharp.Primitives.Extensions
{
    /// <summary>
    /// 扩展信息节点
    /// </summary>
    public class ExtensionInfo 
    {
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
        public CtRefId RefId { get; set; }

        /// <summary>
        /// 扩展属性,"NameTypeValue"的数值组,用于简单的扩展
        /// </summary>
        public List<Property> Properties { get; set; }

        /// <summary>
        /// 扩展复杂属性,使用xs:anyType,用于较复杂的扩展
        /// </summary>
        public List<object> Data { get; set; }

        /// <summary>
        /// 扩展数据文件所在位置,用于扩展大量信息
        /// </summary>
        public CtLocation ExtendData { get; set; }
    }
}
