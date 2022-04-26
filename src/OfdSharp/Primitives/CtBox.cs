namespace OfdSharp.Primitives
{
    /// <summary>
    /// 矩形区域，以空格分割
    /// 前两个值代表了改矩形的左上角坐标，后两个值依次表示该矩形的宽和高
    /// 可以是整数或浮点数，后两个值应该大于0
    /// </summary>
    public class CtBox
    {
        /// <summary>
        /// 左上角坐标
        /// </summary>
        public CtPosition TopLeft { get; }

        /// <summary>
        /// 宽度
        /// </summary>
        public double Width { get; }

        /// <summary>
        /// 高度
        /// </summary>
        public double Height { get; }

        public CtBox(CtPosition topLeft, double width, double height)
        {
            TopLeft = topLeft;
            Width = width;
            Height = height;
        }

        public CtBox(double topLeftX, double topLeftY, double width, double height)
        {
            TopLeft = new CtPosition(topLeftX, topLeftY);
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{TopLeft.XCoordinate} {TopLeft.YCoordinate} {Width} {Height}";
        }

        public static CtBox Parse(string content)
        {
            var values = content.Split(' ');
            return new CtBox(double.Parse(values[0]), double.Parse(values[1]), double.Parse(values[2]), double.Parse(values[3]));
        }

        public static bool operator ==(CtBox a, CtBox b)
        {
            return !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.TopLeft == b.TopLeft && a.Width - b.Width == 0d && a.Height - b.Height == 0d;
        }

        public static bool operator !=(CtBox a, CtBox b)
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
            return obj is CtBox box && box.TopLeft == TopLeft && box.Width - Width == 0d && box.Height - Height == 0d;
        }

        public override int GetHashCode()
        {
            return (TopLeft.XCoordinate + TopLeft.YCoordinate + Width + Height).GetHashCode();
        }
    }
}