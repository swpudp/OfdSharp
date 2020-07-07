using System.Collections.Generic;
using System.Xml;

namespace OfdSharp.Core.PageDescription.Clips
{
    /// <summary>
    /// 图元对象的裁剪区域序列
    ///
    /// 采用对象空间坐标系
    ///
    /// 当存在多个 Clip对象时，最终裁剪区为所有 Clip区域交集。
    ///
    /// 8.5 图元对象 图 45 表 34
    /// </summary>
    public class ClipCollect : OfdElement
    {
        public ClipCollect(XmlDocument xmlDocument) : base(xmlDocument, "Clips")
        {
        }

        /// <summary>
        /// 图元对象的裁剪区域序列
        /// </summary>
        public IList<Clip> Clips { get; set; }
    }
}
