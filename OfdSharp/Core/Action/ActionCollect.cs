using System.Collections.Generic;
using System.Xml;

namespace OfdSharp.Core.Action
{
    /// <summary>
    /// 动作序列
    /// </summary>
    public class ActionCollect : BaseAction
    {
        public ActionCollect(XmlDocument xmlDocument) : base(xmlDocument, "Actions")
        {
        }

        public IList<BaseAction> Actions { get; set; }
    }
}
