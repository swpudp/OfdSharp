namespace OfdSharp.Core.BaseType
{
    public class StBox
    {
        /// <summary>
        /// 左上角 x坐标
        /// 从左 到 右
        /// </summary>
        public double TopLeftX { get; }

        /// <summary>
        /// 左上角 y坐标
        /// 从上 到 下
        /// </summary>
        public double TopLeftY { get; }

        /// <summary>
        /// 宽度
        /// </summary>
        public double Width { get; }

        /// <summary>
        /// 高度
        /// </summary>
        public double Height { get; }

        public StBox(double topLeftX, double topLeftY, double width, double height)
        {
            TopLeftX = topLeftX;
            TopLeftY = topLeftY;
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{TopLeftX} {TopLeftY} {Width} {Height}";
        }
    }
}
