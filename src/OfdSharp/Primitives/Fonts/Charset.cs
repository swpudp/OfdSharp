namespace OfdSharp.Primitives.Fonts
{
    /// <summary>
    /// 字形适用的字符分类
    /// 用于匹配替代字形
    /// 11.1 表 44
    /// 附录 A.5 CT_Font
    /// </summary>
    public enum Charset
    {
        Symbol,
        Prc,
        Big5,
        ShiftJis,
        WanSung,
        JoHab,
    }
}
