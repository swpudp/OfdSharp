namespace OfdSharp.Primitives
{
    /// <summary>
    /// 点坐标，以空格分割，前者为x值，后者为y值，可以是整数或浮点数，如"0 0"
    /// </summary>
    public class CtPosition
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

        public CtPosition(double xCoordinate, double yCoordinate)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
        }

        public override string ToString()
        {
            return $"{XCoordinate} {YCoordinate}";
        }

        public static bool operator ==(CtPosition a, CtPosition b)
        {
            return !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.XCoordinate - b.XCoordinate == 0d && a.YCoordinate - b.YCoordinate == 0d;
        }

        public static bool operator !=(CtPosition a, CtPosition b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return false;
            }

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return true;
            }

            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return obj is CtPosition position && position.XCoordinate - XCoordinate == 0d && position.YCoordinate - YCoordinate == 0d;
        }

        public override int GetHashCode()
        {
            return (XCoordinate + YCoordinate).GetHashCode();
        }
    }
}