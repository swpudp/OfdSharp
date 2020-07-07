using System.Collections.Generic;
using System.Xml;

namespace OfdSharp.Core.Basic.Ofd
{
    /// <summary>
    /// 用户自定义元数据集合。其子节点为 CustomData
    /// </summary>
    public class CustomDataCollect : OfdElement
    {
        public CustomDataCollect(XmlDocument xmlDocument) : base(xmlDocument, "CustomDatas")
        {
        }

        /// <summary>
        /// 自定义元数据集合
        /// </summary>
        public IList<CustomData> CustomDatas { get; set; }
    }
}
