using System;

namespace OfdSharp.Primitives.Doc.Permission
{
    /// <summary>
    /// 有效期
    /// 该文档允许访问的期限，其具体期限取决于开始日期和
    /// 结束日期，其中开始日期不能晚于结束日期，并且开始日期和结束
    /// 日期至少出现一个。当不设置开始日期时，代表不限定开始日期，
    /// 当不设置结束日期时代表不限定结束日期；当此不设置此节点时，
    /// 表示开始和结束日期均不受限
    /// </summary>
    public class ValidPeriod
    {
        /// <summary>
        /// 有效期开始日期
        /// </summary>
        public DateTime? StartDate { get; }

        /// <summary>
        /// 有效期结束日期
        /// </summary>
        public DateTime? EndDate { get; }
    }
}
