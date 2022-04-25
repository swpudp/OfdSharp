namespace OfdSharp
{
    /// <summary>
    /// 外边距
    /// </summary>
    internal class Margin
    {
        /// <summary>
        /// 顶
        /// </summary>
        public float Top { get; }

        /// <summary>
        /// 右
        /// </summary>
        public float Right { get; }

        /// <summary>
        /// 底
        /// </summary>
        public float Bottom { get; }

        /// <summary>
        /// 左
        /// </summary>
        public float Left { get; }

        /// <summary>
        /// 0边距
        /// </summary>
        public static readonly Margin None = new Margin(0f, 0f);

        /// <summary>
        /// A4边距
        /// </summary>
        public static readonly Margin A4 = new Margin(25.4f, 31.8f);

        public Margin(float topAndBottom, float leftAndRight)
        {
            Top = topAndBottom;
            Bottom = topAndBottom;
            Left = leftAndRight;
            Right = leftAndRight;
        }

        public Margin(float top, float right, float bottom, float left)
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }
    }
}