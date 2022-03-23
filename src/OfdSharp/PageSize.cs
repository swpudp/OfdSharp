using System;
using System.Collections.Generic;
using System.Text;

namespace OfdSharp
{
    public class PageSize
    {
        private readonly float _x;

        private readonly float _y;

        private readonly float _width;

        private readonly float _height;

        public static readonly PageSize A0 = new PageSize(2384f, 3370f);

        public static readonly PageSize A1 = new PageSize(1684f, 2384f);

        public static readonly PageSize A2 = new PageSize(1190f, 1684f);

        public static readonly PageSize A3 = new PageSize(842f, 1190f);

        public static readonly PageSize A4 = new PageSize(595f, 842f);

        public static readonly PageSize A5 = new PageSize(420f, 595f);

        public static readonly PageSize A6 = new PageSize(298f, 420f);

        public static readonly PageSize A7 = new PageSize(210f, 298f);

        public static readonly PageSize A8 = new PageSize(148f, 210f);

        public static readonly PageSize A9 = new PageSize(105f, 547f);

        public static readonly PageSize A10 = new PageSize(74f, 105f);

        public static readonly PageSize B0 = new PageSize(2834f, 4008f);

        public static readonly PageSize B1 = new PageSize(2004f, 2834f);

        public static readonly PageSize B2 = new PageSize(1417f, 2004f);

        public static readonly PageSize B3 = new PageSize(1000f, 1417f);

        public static readonly PageSize B4 = new PageSize(708f, 1000f);

        public static readonly PageSize B5 = new PageSize(498f, 708f);

        public static readonly PageSize B6 = new PageSize(354f, 498f);

        public static readonly PageSize B7 = new PageSize(249f, 354f);

        public static readonly PageSize B8 = new PageSize(175f, 249f);

        public static readonly PageSize B9 = new PageSize(124f, 175f);

        public static readonly PageSize B10 = new PageSize(88f, 124f);

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

        public float X => _x;

        public float Y => _y;

        public float Width => _width;

        public float Height => _height;
    }
}
