namespace OfdSharp
{
    public class OfdFont
    {
        /// <summary>
        /// 字形名
        /// </summary>
        public string FontName { get; }

        /// <summary>
        /// 字型族名,用于匹配替代字型
        /// </summary>
        public string FamilyName { get; }

        public OfdFont(string fontName)
        {
            FontName = fontName;
        }

        public OfdFont(string fontName, string familyName)
        {
            FontName = fontName;
            FamilyName = familyName;
        }

        /// <summary>
        /// 宋体
        /// </summary>
        public static readonly OfdFont Simsun = new OfdFont("宋体", "宋体");

        /// <summary>
        /// 黑体
        /// </summary>
        public static readonly OfdFont Simhei = new OfdFont("黑体", "黑体");
    }
}