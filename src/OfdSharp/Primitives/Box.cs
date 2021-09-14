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

        public override string ToString()
        {
            return $"{TopLeft.XCoordinate} {TopLeft.YCoordinate} {Width} {Height}";
        }
    }
}
