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

        internal List<CtText> GetCtTexts(PageSize pageSize, Margin margin, int pCount)
        {
            List<CtText> ctTexts = new List<CtText>();

            foreach (Text t in _texts)
            {
                //每行文字可能换行
                CtFont font = FontCache.GetCtFont(t.Font, _idGen);
                //java TxtGlyph计算
                List<float> s = GetCharWithArray(t.Content, t.FontSize).ToList();
                float totalHeight = pCount * t.FontSize * ConstDefined.PtToMmRatio;
                float totalWidth = s.Sum();
                float availWidth = pageSize.Width - margin.Left - margin.Right;
                if (totalWidth <= availWidth)
                {
                    CtText ctText = new CtText
                    {
                        Id = _idGen.NewId(),
                        Boundary = new CtBox(margin.Left, margin.Top + totalHeight, s.Sum(), t.FontSize * ConstDefined.PtToMmRatio),
                        FillColor = t.Color.ToCtColor(),
                        Fill = t.Fill,
                        Stroke = t.Stroke,
                        StrokeColor = t.StrokeColor.ToCtColor(),
                        TextCode = new TextCode(t.Content)
                        {
                            X = 0,
                            Y = t.FontSize * ConstDefined.PtToMmRatio * 0.8f,
                            DeltaX = new CtArray(string.Join(" ", s))
                        },
                        Size = t.FontSize * ConstDefined.PtToMmRatio,
                        Font = new CtRefId(font.Id)
                    };
                    ctTexts.Add(ctText);
                }
                else
                {
                    IEnumerable<int> newLines = GetNewLineIndex(s, availWidth);
                    int idx = 0;
                    foreach (int newLine in newLines)
                    {
                        string newText = t.Content.Substring(idx, newLine);
                        idx = newLine;

                        List<float> newCharWidth = GetCharWithArray(newText, t.FontSize).ToList();

                        CtText ctText = new CtText
                        {
                            Id = _idGen.NewId(),
                            Boundary = new CtBox(margin.Left, margin.Top + totalHeight, newCharWidth.Sum(), t.FontSize * ConstDefined.PtToMmRatio),
                            FillColor = t.Color.ToCtColor(),
                            Fill = t.Fill,
                            Stroke = t.Stroke,
                            StrokeColor = t.StrokeColor.ToCtColor(),
                            TextCode = new TextCode(newText)
                            {
                                X = 0,
                                Y = t.FontSize * ConstDefined.PtToMmRatio * 0.8f,
                                DeltaX = new CtArray(string.Join(" ", s))
                            },
                            Size = t.FontSize * ConstDefined.PtToMmRatio,
                            Font = new CtRefId(font.Id)
                        };
                        ctTexts.Add(ctText);
                    }
                }
            }

            return ctTexts;
        }

        private static IEnumerable<int> GetNewLineIndex(List<float> charsWidth, float availWidth)
        {
            float lineWidth = 0f;
            foreach (float charWidth in charsWidth)
            {
                int index = charsWidth.IndexOf(charWidth);
                if (lineWidth > availWidth)
                {
                    lineWidth = 0f;
                    yield return index;
                    continue;
                }

                lineWidth += charWidth;
            }
        }

        private static IEnumerable<float> GetCharWithArray(string txt, float fontSize)
        {
            return txt.Select(c => fontSize * ConstDefined.PtToMmRatio * (c >= 32 && c <= 126 ? 0.5f : 1f));
        }
    }
}