using OfdSharp.Primitives.Pages.Description.Color;

namespace OfdSharp
{
    public class OfdColor
    {
        private readonly int _r;
        private readonly int _g;
        private readonly int _b;
        public static readonly OfdColor Red = new OfdColor(255, 0, 0);
        public static readonly OfdColor Black = new OfdColor(0, 0, 0);

        public OfdColor(int r, int g, int b)
        {
            _r = r;
            _g = g;
            _b = b;
        }

        internal CtColor ToCtColor()
        {
            return new CtColor(_r.ToString(), _b.ToString(), _g.ToString());
        }
    }
}
