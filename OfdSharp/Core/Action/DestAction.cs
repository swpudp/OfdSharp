using System.Globalization;
using System.Xml;

namespace OfdSharp.Core.Action
{
    /// <summary>
    /// 目标区域
    /// </summary>
    public class DestAction : BaseAction
    {
        /// <summary>
        /// 目标区域的描述方法
        /// </summary>
        public DestType Type { get; }

        /// <summary>
        /// 引用跳转目标页面的标识
        /// </summary>
        public string PageId { get; }

        /// <summary>
        /// 目标区域左上角 x坐标
        /// 默认值为 0
        /// </summary>
        public double Left { get; }

        /// <summary>
        /// 目标区域右上角 x坐标
        /// 默认值为 0
        /// </summary>
        public double Right { get; }

        /// <summary>
        /// 目标区域左上角 y坐标
        /// 默认值为 0
        /// </summary>
        public double Top { get; }

        /// <summary>
        /// 目标区域右下角 y坐标
        /// 默认值为 0
        /// </summary>
        public double Bottom { get; }

        /// <summary>
        /// 目标区域页面缩放比例
        /// 为 0 或不出现则按照但前缩放比例跳转，可取值范围[0.1 64.0]
        /// </summary>
        public double Zoom { get; }

        public DestAction(XmlDocument xmlDocument, DestType type, string pageId, double left, double right, double top, double bottom, double zoom) : base(xmlDocument, "Dest")
        {
            Type = type;
            PageId = pageId;
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
            Zoom = zoom;

            Element.SetAttribute("Type", type.ToString());
            Element.SetAttribute("PageID", pageId);
            Element.SetAttribute("Left", left.ToString(CultureInfo.InvariantCulture));
            Element.SetAttribute("Right", right.ToString(CultureInfo.InvariantCulture));
            Element.SetAttribute("Top", top.ToString(CultureInfo.InvariantCulture));
            Element.SetAttribute("Bottom", bottom.ToString(CultureInfo.InvariantCulture));
            Element.SetAttribute("Zoom", zoom.ToString(CultureInfo.InvariantCulture));
        }
    }
}
