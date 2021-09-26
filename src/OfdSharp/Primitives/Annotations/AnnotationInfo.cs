using OfdSharp.Primitives.PageObject;
using System;
using System.Collections.Generic;

namespace OfdSharp.Primitives.Annotations
{
    /// <summary>
    /// 注释信息
    /// </summary>
    public class AnnotationInfo
    {
        /// <summary>
        /// 注释的标识
        /// </summary>
        public Id Id { get; set; }

        /// <summary>
        /// 注释类型
        /// </summary>
        public AnnotationType Type { get; set; }

        /// <summary>
        /// 注释创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 最近一次修改的时间
        /// </summary>
        public DateTime LastModDate { get; set; }

        /// <summary>
        /// 注释子类型
        /// </summary>
        public string Subtype { get; set; }

        /// <summary>
        /// 表示该注释对象是否显示，默认true
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// 对象的Remark信息是否随页面一起打印，默认true
        /// </summary>
        public bool Print { get; set; }

        /// <summary>
        /// 对象的Remark信息是否不随页面缩放而同步缩放，默认值为false
        /// </summary>
        public bool NoZoom { get; set; }

        /// <summary>
        /// 对象的Remark信息是否不随页面旋转而同步旋转，默认值为false
        /// </summary>
        public bool NoRotate { get; set; }

        /// <summary>
        /// 对象的Remark信息是否不能被用户更改，默认true
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// 注释说明内容
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 一组注释参数
        /// </summary>
        public List<Parameter> Parameters { get; set; }

        /// <summary>
        /// 注释的静态呈现效果,使用页面块定义来描述
        /// </summary>
        public PageBlock Appearance { get; set; }
    }
}
