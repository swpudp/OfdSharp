using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using OfdSharp.Primitives.Fonts;

namespace OfdSharp
{
    internal static class FontCache
    {
        private static readonly ConcurrentDictionary<OfdFont, CtFont> _fonts = new ConcurrentDictionary<OfdFont, CtFont>();

        public static CtFont GetCtFont(OfdFont font, IdGen idGen)
        {
            if (_fonts.TryGetValue(font, out CtFont ctFont))
            {
                return ctFont;
            }

            ctFont = new CtFont
            {
                Id = idGen.NewId(),
                FamilyName = font.FamilyName,
                FontName = font.FontName
            };
            _fonts.TryAdd(font, ctFont);
            return ctFont;
        }

        public static List<CtFont> GetCtFonts()
        {
            return _fonts.Values.ToList();
        }
    }
}