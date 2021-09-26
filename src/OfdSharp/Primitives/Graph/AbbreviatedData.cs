using System;
using System.Collections.Generic;
using System.Globalization;

namespace OfdSharp.Primitives.Graph
{
    /// <summary>
    /// 图形轮廓数据
    /// 由一系列的紧缩的操作符和操作数构成
    /// </summary>
    public class AbbreviatedData 
    {
        /// <summary>
        /// 绘制数据队列
        /// </summary>
        private readonly LinkedList<string[]> _dataQueue= new LinkedList<string[]>();

        /// <summary>
        /// 从当前点连接到点（x，y）的圆弧，并将当前点移动到点（x，y）。
        /// rx 表示椭圆的长轴长度，ry 表示椭圆的短轴长度。angle 表示
        /// 椭圆在当前坐标系下旋转的角度，正值为顺时针，负值为逆时针，
        /// large 为 1 时表示对应度数大于180°的弧，为 0 时表示对应
        /// 度数小于 180°的弧。sweep 为 1 时表示由圆弧起始点到结束点
        /// 是顺时针旋转，为 0 时表示由圆弧起始点到结束点是逆时针旋转。
        /// </summary>
        /// <param name="rx">椭圆长轴长度</param>
        /// <param name="ry">椭圆短轴长度</param>
        /// <param name="angle">旋转角度，正值顺时针，负值逆时针</param>
        /// <param name="large">1 时表示对应度数大于 180°的弧，0 时表示对应度数小于 180°的弧</param>
        /// <param name="sweep">sweep 为 1 时表示由圆弧起始点到结束点是顺时针旋转，为 0 时表示由圆弧起始点到结束点是逆时针旋转。</param>
        /// <param name="x">目标点 x</param>
        /// <param name="y">目标点 y</param>
        /// <returns></returns>
        public AbbreviatedData Arc(double rx, double ry, double angle, int large, int sweep, double x, double y)
        {
            if (large != 0 && large != 1)
            {
                throw new NotSupportedException("large 只接受 0 或 1");
            }
            if (sweep != 0 && sweep != 1)
            {
                throw new NotSupportedException("sweep 只接受 0 或 1");
            }
            _dataQueue.AddLast(new[]{"A", " ", rx.ToString(CultureInfo.InvariantCulture), " ", ry.ToString(CultureInfo.InvariantCulture),
                " ",angle.ToString(CultureInfo.InvariantCulture), " ", large.ToString(CultureInfo.InvariantCulture),
                " ",sweep.ToString(CultureInfo.InvariantCulture), " ", x.ToString(CultureInfo.InvariantCulture), " ", y.ToString(CultureInfo.InvariantCulture)
            });
            return this;
        }
    }
}
