using System;
using System.IO;

namespace OfdSharp.Font
{
    /// <summary>
    /// 字体
    /// </summary>
    public class Font
    {
        /// <summary>
        /// 字体名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字体族名称
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// 字体文件路径
        /// </summary>
        public FileInfo FontFile { get; set; }

        /// <summary>
        /// 可打印字符宽度映射
        /// </summary>
        public double[] PrintableAsciiWidthMap { get; set; }

        /// <summary>
        /// 默认字体
        /// </summary>
        public static Font Default => FontSet.Get(FontName.SimSun);

        /// <summary>
        /// 获取字符占比
        /// </summary>
        /// <param name="txt">字符</param>
        /// <returns>0~1 占比</returns>
        public double GetCharWidthScale(char txt)
        {
            // 如果存在字符映射那么从字符映射中获取宽度占比
            if (PrintableAsciiWidthMap != null)
            {
                // 所有 ASCII码均采用半角
                if (txt >= 32 && txt <= 126)
                {
                    // 根据可打印宽度比例映射表打印
                    return PrintableAsciiWidthMap[txt - 32];
                }
                else
                {
                    // 非英文字符
                    return 1;
                }
            }
            else
            {
                // 不存在字符映射，那么认为是等宽度比例 ASCII 为 0.5 其他为 1
                return (txt >= 32 && txt <= 126) ? 0.5 : 1;
            }
        }

        /// <summary>
        /// 获取字体全名
        /// </summary>
        /// <returns></returns>
        public String GetFullFontName()
        {
            if (FamilyName == null)
            {
                return Name;
            }
            else
            {
                return Name + "-" + FamilyName;
            }
        }
    }
}