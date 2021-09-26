using OfdSharp.Primitives.CompositeObj;
using OfdSharp.Primitives.Graph;
using OfdSharp.Primitives.Image;
using OfdSharp.Primitives.Text;

namespace OfdSharp.Primitives.PageObject
{
    /// <summary>
    /// 页块结构
    /// 可以嵌套
    /// </summary>
    public class PageBlock
    {
        /// <summary>
        /// PageBlock：页面块,可以嵌套
        /// </summary>
        public PageBlock Next { get; set; }

        /// <summary>
        /// 文字对象
        /// </summary>
        public CtText TextObject { get; set; }

        /// <summary>
        /// 图形对象
        /// </summary>
        public Path PathObject { get; set; }

        /// <summary>
        /// 图像对象,见第10章,
        /// 带有播放视频动作时,见第12章
        /// </summary>
        public CtImage ImageObject { get; set; }

        /// <summary>
        /// 复合对象
        /// </summary>
        public CompositeObject CompositeObject { get; set; }
    }
}
