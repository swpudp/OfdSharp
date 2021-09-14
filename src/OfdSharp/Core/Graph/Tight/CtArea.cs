using OfdSharp.Primitives;
using System.Collections.Generic;

namespace OfdSharp.Core.Graph.Tight
{
    /// <summary>
    /// 区域由一系列的分路径（Area）组成，每个路径都是闭合的
    /// </summary>
    public class CtArea
    {
        /// <summary>
        /// 定义字图形的起始点坐标
        /// </summary>
        public Position Start { get; set; }

        /// <summary>
        /// 从当前点移动到新的当前点
        /// </summary>
        public IList<Move> Moves { get; set; }

        /// <summary>
        /// 从当前点连接一条到指定点的线段,并将当前点移动到指定点
        /// </summary>
        public IList<Line> Lines { get; set; }

        /// <summary>
        /// 从当前点连接一条到Point2的二次贝塞尔曲线,并将当前点移动到Point2,此贝塞尔曲线使用Point1作为其控制点
        /// </summary>
        public IList<QuadraticBezier> QuadraticBeziers { get; set; }

        /// <summary>
        /// 从当前点连接一条到Point3的三次贝塞尔曲线,并将当前点移动到Point3,使用Point1和Point2作为控制点
        /// </summary>
        public IList<CubicBezier> CubicBeziers { get; set; }

        /// <summary>
        /// 从当前点连接一条到EndPoint点的圆弧,并将当前点移动到End-Point点
        /// </summary>
        public IList<Arc> Arcs { get; set; }

        /// <summary>
        /// 自动闭合到当前分路径的起始点,并以该点为当前点
        /// </summary>
        public IList<Close> Closes { get; set; }
    }
}
