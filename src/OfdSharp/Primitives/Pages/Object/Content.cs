using System.Collections.Generic;

namespace OfdSharp.Primitives.Pages.Object
{
    /// <summary>
    /// 页面内容描述,该节点不存在时,表示空白页
    /// </summary>
    public class Content
    {
        /// <summary>
        /// 层节点，一个页可包含一个或多个层
        /// </summary>
        public List<Layer> Layers { get; set; }
    }
}
