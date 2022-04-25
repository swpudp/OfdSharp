using OfdSharp.Primitives;
using System.Collections.Generic;
using System.Linq;
using OfdSharp.Primitives.Fonts;
using OfdSharp.Primitives.Text;

namespace OfdSharp
{
    /// <summary>
    /// 一个段落
    /// </summary>
    public class Paragraph
    {
        private readonly LinkedList<Text> _texts;
        private readonly IdGen _idGen;

        internal Paragraph(IdGen idGen)
        {
            _idGen = idGen;
            _texts = new LinkedList<Text>();
        }

        public Text AddText(string content)
        {
            Text text = new Text(content)
            {
                Font = OfdFont.Simsun
            };
            _texts.AddLast(text);
            return text;
        }

        internal List<CtText> GetCtTexts(Margin margin, int pCount)
        {
            List<CtText> ctTexts = new List<CtText>();

            foreach (Text t in _texts)
            {
                CtFont font = FontCache.GetCtFont(t.Font, _idGen);
                //java TxtGlyph计算
                List<float> s = GetCharWithArray(t.Content, t.FontSize).ToList();
                float totalHeight = pCount * t.FontSize * ConstDefined.PtToMmRatio;
                CtText ctText = new CtText
                {
                    Id = _idGen.NewId(),
                    Boundary = new Box(margin.Left, margin.Top + totalHeight, s.Sum(), t.FontSize * ConstDefined.PtToMmRatio),
                    FillColor = t.Color.ToCtColor(),
                    Fill = t.Fill,
                    Stroke = t.Stroke,
                    StrokeColor = t.StrokeColor.ToCtColor(),
                    TextCode = new TextCode(t.Content)
                    {
                        X = 0,
                        Y = t.FontSize * ConstDefined.PtToMmRatio * 0.8f,
                        DeltaX = new Primitives.Array(string.Join(" ", s))
                    },
                    Size = t.FontSize * ConstDefined.PtToMmRatio,
                    Font = new RefId(font.Id)
                };
                ctTexts.Add(ctText);
            }

            return ctTexts;
        }

        private static IEnumerable<float> GetCharWithArray(string txt, float fontSize)
        {
            return txt.Select(c => fontSize * ConstDefined.PtToMmRatio * (c >= 32 && c <= 126 ? 0.5f : 1f));
        }
    }
}