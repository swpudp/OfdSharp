namespace OfdSharp.Primitives
{
    /// <summary>
    /// 点坐标，以空格分割，前者为x值，后者为y值，可以是整数或浮点数，如"0 0"
    /// </summary>
    public class Position
    {
        /// <summary>
        /// X坐标
        /// 从左 到 右
        /// </summary>
        public double XCoordinate { get; }

        /// <summary>
        /// y坐标
        /// 从上 到 下
        /// </summary>
        public double YCoordinate { get; }

        public Position(double xCoordinate, double yCoordinate)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
        }

        public override string ToString()
        {
            return $"{XCoordinate} {YCoordinate}";
        }

        public static bool operator ==(Position a, Position b)
        {
            return a.XCoordinate == b.XCoordinate && a.YCoordinate == b.YCoordinate;
        }

        public static bool operator !=(Position a, Position b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Position position))
            {
                return false;
            }
            return position.XCoordinate == XCoordinate && position.YCoordinate == YCoordinate;
        }

        public override int GetHashCode()
        {
            return (XCoordinate + YCoordinate).GetHashCode();
        }
    }
}
