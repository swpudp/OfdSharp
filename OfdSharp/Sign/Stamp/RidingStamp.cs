using OfdSharp.Core.Signatures;
using OfdSharp.Reader;
using System;
using System.Collections.Generic;

namespace OfdSharp.Sign.Stamp
{
    /// <summary>
    /// 骑缝章位置
    /// 默认图章放在边的正中央
    /// </summary>
    public class RidingStamp : IStampAppearance
    {
        /// <summary>
        /// 默认骑缝章以右侧边作为骑缝的位置
        /// </summary>
        public Side Side { get; set; }

        /// <summary>
        /// 图章整章宽度
        /// 单位毫米mm
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// 图章整章高度
        /// 单位毫米mm
        /// </summary>
        public double Height { get; set; }


        public RidingStamp(double width, double height)
        {
            this.Width = width;
            this.Height = height;
            Side = Side.Right;
        }

        public RidingStamp(Side side, double width, double height)
        {
            this.Side = side;
            this.Width = width;
            this.Height = height;
        }

        public List<StampAnnotation> GetAppearance(OfdReader ctx, AtomicSignId idProvider)
        {
            throw new NotImplementedException();
            //// 总页码数
            //int numPage = ctx.getNumberOfPages();
            //List<StampAnnot> res = new ArrayList<>(numPage);

            //if (side == Side.Right || side == Side.Left)
            //{
            //    // 按页码平分印章图片
            //    double itemWith = this.width / numPage;
            //    for (int i = 0; i < numPage; i++)
            //    {

            //        Page page = ctx.getPage(i + 1);
            //        ST_Box pageSize = ctx.getPageSize(page);
            //        double x;
            //        ST_Box clip = null;
            //        if (side == Side.Right)
            //        {
            //            x = pageSize.getWidth() - itemWith * (i + 1);
            //            clip = new ST_Box(i * itemWith, 0, itemWith, this.height);
            //        }
            //        else
            //        {
            //            x = 0 - itemWith * (numPage - 1 - i);
            //            clip = new ST_Box((numPage - 1 - i) * itemWith, 0, itemWith, this.height);
            //        }

            //        double y = pageSize.getHeight() / 2 - this.height / 2;

            //        ST_RefID ref = ctx.getPageObjectId(i + 1).ref ();
            //        StampAnnot annot = new StampAnnot()
            //                .setID(idProvider.incrementAndGet())
            //                .setBoundary(new ST_Box(x, y, this.width, this.height))
            //                .setPageRef(ref)
            //                .setClip(clip);
            //        res.add(annot);
            //    }
            //}
            //else
            //{
            //    double itemHeight = this.height / numPage;
            //    for (int i = 0; i < numPage; i++)
            //    {

            //        Page page = ctx.getPage(i + 1);
            //        ST_Box pageSize = ctx.getPageSize(page);

            //        double x = pageSize.getWidth() / 2 - this.width / 2;
            //        double y;
            //        ST_Box clip = null;
            //        if (side == Side.Bottom)
            //        {
            //            y = pageSize.getHeight() - itemHeight * (i + 1);
            //            clip = new ST_Box(0, itemHeight * i, this.width, itemHeight);
            //        }
            //        else
            //        {
            //            y = 0 - itemHeight * (numPage - 1 - i);
            //            clip = new ST_Box(0, (numPage - 1 - i) * itemHeight, this.width, itemHeight);
            //        }

            //        ST_RefID ref = ctx.getPageObjectId(i + 1).ref ();
            //        StampAnnot annot = new StampAnnot()
            //                .setID(idProvider.incrementAndGet())
            //                .setBoundary(new ST_Box(x, y, this.width, this.height))
            //                .setPageRef(ref)
            //                .setClip(clip);
            //        res.add(annot);
            //    }

            //}
            //return res;
        }
    }
}
