using System;
using System.IO;

namespace OfdSharp.Font
{
    /// <summary>
    /// 字体集合
    /// </summary>
    public sealed class FontSet
    {
        /// <summary>
        /// 可打印的ASCII表字母宽度所占用百分比
        /// ASCII区间: [32,126]
        /// 其中空格特殊处理，默认为半个字符宽度也就是 0.5
        /// </summary>
        public static double[] NOTO_PRINTABLE_ASCII_WIDTH_MAP = {
            0.5, 0.3125, 0.435546875, 0.63818359375, 0.58642578125, 0.8896484375, 0.8701171875, 0.25634765625, 0.333984375, 0.333984375, 0.455078125, 0.74169921875, 0.24072265625, 0.4326171875, 0.24072265625, 0.42724609375, 0.58642578125, 0.58642578125, 0.58642578125, 0.58642578125, 0.58642578125, 0.58642578125, 0.58642578125, 0.58642578125, 0.58642578125, 0.58642578125, 0.24072265625, 0.24072265625, 0.74169921875, 0.74169921875, 0.74169921875, 0.48291015625, 1.03125, 0.70361328125, 0.62744140625, 0.6689453125, 0.76171875, 0.5498046875, 0.53125, 0.74365234375, 0.7734375, 0.2939453125, 0.39599609375, 0.634765625, 0.51318359375, 0.97705078125, 0.81298828125, 0.81494140625, 0.61181640625, 0.81494140625, 0.65283203125, 0.5771484375, 0.5732421875, 0.74658203125, 0.67626953125, 1.017578125, 0.64501953125, 0.603515625, 0.6201171875, 0.333984375, 0.416015625, 0.333984375, 0.74169921875, 0.4482421875, 0.294921875, 0.552734375, 0.638671875, 0.50146484375, 0.6396484375, 0.5673828125, 0.3466796875, 0.6396484375, 0.61572265625, 0.26611328125, 0.26708984375, 0.54443359375, 0.26611328125, 0.93701171875, 0.6162109375, 0.6357421875, 0.638671875, 0.6396484375, 0.3818359375, 0.462890625, 0.37255859375, 0.6162109375, 0.52490234375, 0.78955078125, 0.5068359375, 0.529296875, 0.49169921875, 0.333984375, 0.26904296875, 0.333984375, 0.74169921875
        };

        /// <summary>
        /// Time New Roman 字体宽度比例
        /// </summary>
        public static double[] TIMES_NEW_ROMAN_PRINTABLE_ASCII_MAP = {
            0.25, 0.3330078125, 0.408203125, 0.5, 0.5, 0.8330078125, 0.77783203125, 0.18017578125, 0.3330078125, 0.3330078125, 0.5, 0.56396484375, 0.25, 0.3330078125, 0.25, 0.27783203125, 0.5, 0.46326171875, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.27783203125, 0.27783203125, 0.56396484375, 0.56396484375, 0.56396484375, 0.44384765625, 0.9208984375, 0.72216796875, 0.6669921875, 0.6669921875, 0.72216796875, 0.61083984375, 0.55615234375, 0.72216796875, 0.72216796875, 0.3330078125, 0.38916015625, 0.72216796875, 0.61083984375, 0.88916015625, 0.72216796875, 0.72216796875, 0.55615234375, 0.72216796875, 0.6669921875, 0.55615234375, 0.61083984375, 0.72216796875, 0.72216796875, 0.94384765625, 0.72216796875, 0.72216796875, 0.61083984375, 0.3330078125, 0.27783203125, 0.3330078125, 0.46923828125, 0.5, 0.3330078125, 0.44384765625, 0.5, 0.44384765625, 0.5, 0.44384765625, 0.3151220703125, 0.5, 0.5, 0.27783203125, 0.27783203125, 0.5, 0.27783203125, 0.77783203125, 0.5, 0.5, 0.5, 0.5, 0.3330078125, 0.38916015625, 0.27783203125, 0.5, 0.5, 0.72216796875, 0.5, 0.5, 0.44384765625, 0.47998046875, 0.2001953125, 0.47998046875, 0.541015625,
        };

        /// <summary>
        /// 通过文件名获取字体
        /// </summary>
        /// <param name="fontName"></param>
        /// <returns></returns>
        public static Font Get(FontName fontName)
        {
            String fileName = fontName.ToString();

            FileInfo path = null;
            if (fileName != null)
            {
                // 从jar包中加载字体
                path = LoadAndCacheFont(fileName);
            }

            switch (fontName)
            {
                case FontName.NotoSerif:
                    return new Font { Name = "Noto Serif CJK SC", FontFile = path, PrintableAsciiWidthMap = NOTO_PRINTABLE_ASCII_WIDTH_MAP };
                case FontName.NotoSerifBold:
                    return new Font { Name = "Noto Serif CJK SC Bold", FamilyName = "Bold", FontFile = path, PrintableAsciiWidthMap = NOTO_PRINTABLE_ASCII_WIDTH_MAP };
                case FontName.NotoSans:
                    return new Font { Name = "Noto Sans Mono CJK SC Regular", FontFile = path, PrintableAsciiWidthMap = NOTO_PRINTABLE_ASCII_WIDTH_MAP };
                case FontName.NotoSansBold:
                    return new Font { Name = "Noto Sans Mono CJK SC Bold", FamilyName = "Bold", FontFile = path, PrintableAsciiWidthMap = NOTO_PRINTABLE_ASCII_WIDTH_MAP };
                case FontName.SimSun:
                    return new Font { Name = "宋体", FamilyName = "宋体" };
                case FontName.SimHei:
                    return new Font { Name = "黑体", FamilyName = "黑体" };
                case FontName.KaiTi:
                    return new Font { Name = "楷体", FamilyName = "楷体" };
                case FontName.MSYahei:
                    return new Font { Name = "微软雅黑", FamilyName = "微软雅黑" };
                case FontName.FangSong:
                    return new Font { Name = "仿宋", FamilyName = "仿宋" };
                case FontName.TimesNewRoman:
                    return new Font { Name = "Times New Roman", FamilyName = "Times New Roman", PrintableAsciiWidthMap = TIMES_NEW_ROMAN_PRINTABLE_ASCII_MAP };
            }
            throw new NotSupportedException("不支持字体：" + fontName);

        }

        /// <summary>
        /// 加载并缓存字体文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static FileInfo LoadAndCacheFont(String fileName)
        {
            return new FileInfo(fileName);
        }
    }
}
