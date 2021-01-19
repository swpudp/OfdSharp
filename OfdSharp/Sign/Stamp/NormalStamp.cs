using OfdSharp.Reader;
using System.Collections.Generic;
using OfdSharp.Core.Signs;

namespace OfdSharp.Sign.Stamp
{
    /// <summary>
    /// 普通印章位置
    /// </summary>
    public class NormalStamp : IStampAppearance
    {
        /// <summary>
        /// 图章所在页面页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 图章左上角X坐标
        /// 单位毫米mm
        /// </summary>
        public double Tlx { get; set; }

        /// <summary>
        /// 图章左上角X坐标
        /// 单位毫米mm
        /// </summary>
        public double Tly { get; set; }

        /// <summary>
        /// 图章宽度
        /// 单位毫米mm
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// 图章高度
        /// 单位毫米mm
        /// </summary>
        public double Height { get; set; }

        public NormalStamp(int page, double tlx, double tly, double width, double height)
        {
            this.Page = page;
            this.Tlx = tlx;
            this.Tly = tly;
            this.Width = width;
            this.Height = height;
        }

        public List<StampAnnot> GetAppearance(OfdReader ctx, AtomicSignId idProvider)
        {
            // 解析OFD页码获取页面对应的ID
            string id = string.Empty; //ctx.getPageObjectId(page).ref ();
            StampAnnot annotation = new StampAnnot()
            {
                Id = idProvider.IncrementAndGet(),
                Boundary = $"{Tlx} {Tly} {Width} {Height}",
                PageRef = string.Empty
            };
            List<StampAnnot> res = new List<StampAnnot>(1) { annotation };
            return res;
        }
    }
}
