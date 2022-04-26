namespace OfdSharp
{
    public class PageSize
    {
        private readonly float _x;

        private readonly float _y;

        private readonly float _width;

        private readonly float _height;

        public static readonly PageSize A0 = new PageSize(841f, 1189f);

        public static readonly PageSize A1 = new PageSize(594f, 841f);

        public static readonly PageSize A2 = new PageSize(420f, 594f);

        public static readonly PageSize A3 = new PageSize(297f, 420f);

        public static readonly PageSize A4 = new PageSize(210f, 297f);

        public static readonly PageSize A5 = new PageSize(148f, 210f);

        public static readonly PageSize A6 = new PageSize(105f, 148f);

        public static readonly PageSize A7 = new PageSize(74f, 105f);

        public static readonly PageSize A8 = new PageSize(52f, 74f);

        public static readonly PageSize A9 = new PageSize(37f, 52f);

        public static readonly PageSize A10 = new PageSize(26f, 37f);

        public static readonly PageSize B0 = new PageSize(1000f, 1414f);

        public static readonly PageSize B1 = new PageSize(707f, 1000f);

        public static readonly PageSize B2 = new PageSize(500f, 707f);

        public static readonly PageSize B3 = new PageSize(353f, 500f);

        public static readonly PageSize B4 = new PageSize(250f, 353f);

        public static readonly PageSize B5 = new PageSize(176f, 250f);

        public static readonly PageSize B6 = new PageSize(125f, 176f);

        public static readonly PageSize B7 = new PageSize(88f, 125f);

        public static readonly PageSize B8 = new PageSize(62f, 88f);

        public static readonly PageSize B9 = new PageSize(44f, 62f);

        public static readonly PageSize B10 = new PageSize(31f, 44f);

        public PageSize(float width, float height)
        {
            _x = 0;
            _y = 0;
            _width = width;
            _height = height;
        }

        public PageSize(float x, float y, float width, float height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }

        /// <summary>
        /// x轴距离，单位mm
        /// </summary>
        public float X => _x;

        /// <summary>
        /// y轴距离，单位mm
        /// </summary>
        public float Y => _y;

        /// <summary>
        /// 宽度，单位mm
        /// </summary>
        public float Width => _width;

        /// <summary>
        /// 高度，单位mm
        /// </summary>
        public float Height => _height;
    }
}
