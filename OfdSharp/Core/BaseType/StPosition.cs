namespace OfdSharp.Core.BaseType
{
    public class StPosition
    {
        /// <summary>
        /// X坐标
        /// 从左 到 右
        /// </summary>
        public double X { get; }

        /// <summary>
        /// y坐标
        /// 从上 到 下
        /// </summary>
        public double Y { get; }

        public StPosition(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X} {Y}";
        }
    }
}
