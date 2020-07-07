using System.Xml;

namespace OfdSharp.Core.Basic.Doc
{
    public class CommonData : OfdElement
    {
        public CommonData(XmlDocument xmlDocument, string maxUnitId) : base(xmlDocument, "CommonData")
        {
            MaxUnitId = maxUnitId;
        }

        /// <summary>
        /// 当前文档中所有对象使用标识的最大值
        /// 初始值为 0。MaxUnitID主要用于文档编辑，
        /// 在向文档增加一个新对象时，需要分配一个
        /// 新的标识符，新标识符取值宜为 MaxUnitID + 1，
        /// 同时需要修改此 MaxUnitID值。
        /// </summary>
        public string MaxUnitId { get; }

        /// <summary>
        /// 该文档页面区域的默认大小和位置
        /// </summary>
        public PageArea PageArea { get; set; }


    }
}
