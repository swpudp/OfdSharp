namespace OfdSharp.Primitives
{
    /// <summary>
    /// 矩形区域，以空格分割
    /// 前两个值代表了改矩形的左上角坐标，后两个值依次表示该矩形的宽和高
    /// 可以是整数或浮点数，后两个值应该大于0
    /// </summary>
    public class Box
    {
        /// <summary>
        /// 左上角坐标
        /// </summary>
        public Position TopLeft { get; }

        /// <summary>
        /// 宽度
        /// </summary>
        public double Width { get; }

        /// <summary>
        /// 高度
        /// </summary>
        public double Height { get; }

        public Box(Position topLeft, double width, double height)
        {
            TopLeft = topLeft;
            Width = width;
            Height = height;
        }

        public Box(double topLeftX, double topLeftY, double width, double height)
        {
            TopLeft = new Position(topLeftX, topLeftY);
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{TopLeft.XCoordinate} {TopLeft.YCoordinate} {Width} {Height}";
        }

        public static Box Parse(string content)
        {
            var values = content.Split(' ');
            return new Box(double.Parse(values[0]), double.Parse(values[1]), double.Parse(values[2]), double.Parse(values[3]));
        }

        public static bool operator ==(Box a, Box b)
        {
            return a.TopLeft == b.TopLeft && a.Width == b.Width && a.Height == b.Height;
        }

        public static bool operator !=(Box a, Box b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Box box))
            {
                return false;
            }
            return box.TopLeft == TopLeft && box.Width == Width && box.Height == Height;
        }

        public override int GetHashCode()
        {
            return (TopLeft.XCoordinate + TopLeft.YCoordinate + Width + Height).GetHashCode();
        }
    }
}
