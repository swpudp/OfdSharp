using System;
using System.Globalization;
using System.Text;

namespace OfdSharp.Crypto
{
    /// <summary>
    /// 字节操作工具
    /// </summary>
    public static class ByteUtils
    {
        /// <summary>
        /// 将Ascii字符转换为字节
        /// </summary>
        /// <param name="s">Ascii字符串</param>
        /// <returns></returns>
        internal static byte[] AsciiBytes(string s)
        {
            byte[] array = new byte[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                array[i] = (byte)s[i];
            }
            return array;
        }

        /// <summary>
        /// 16进制字符串转字节数组
        /// </summary>
        /// <param name="hexString">16进制字符串</param>
        /// <returns></returns>
        internal static byte[] HexToByteArray(this string hexString)
        {
            byte[] array = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i += 2)
            {
                string s = hexString.Substring(i, 2);
                array[i / 2] = byte.Parse(s, NumberStyles.HexNumber, null);
            }
            return array;
        }

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes">字节数据</param>
        /// <returns></returns>
        internal static string ByteArrayToHex(this byte[] bytes)
        {
            StringBuilder stringBuilder = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes)
            {
                stringBuilder.Append(b.ToString("X2"));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 字节数组转化为指定长度的16进制字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        internal static string ByteArrayToHex(this byte[] bytes, int len)
        {
            return bytes.ByteArrayToHex().Substring(0, len * 2);
        }

        /// <summary>
        /// 取到字节
        /// </summary>
        /// <param name="b"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        internal static byte[] RepeatByte(byte b, int count)
        {
            byte[] array = new byte[count];
            for (int i = 0; i < count; i++)
            {
                array[i] = b;
            }
            return array;
        }

        internal static byte[] SubBytes(this byte[] bytes, int startIndex, int length)
        {
            byte[] array = new byte[length];
            Array.Copy(bytes, startIndex, array, 0, length);
            return array;
        }

        /// <summary>
        /// 异或操作
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static byte[] Xor(this byte[] value)
        {
            byte[] array = new byte[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] ^= value[i];
            }
            return array;
        }

        /// <summary>
        /// 异或操作
        /// </summary>
        /// <param name="valueA"></param>
        /// <param name="valueB"></param>
        /// <returns></returns>
        internal static byte[] Xor(this byte[] valueA, byte[] valueB)
        {
            int num = valueA.Length;
            byte[] array = new byte[num];
            for (int i = 0; i < num; i++)
            {
                array[i] = (byte)(valueA[i] ^ valueB[i]);
            }
            return array;
        }

        /// <summary>
        /// 右移位操作
        /// </summary>
        /// <param name="number"></param>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static int RightShift(int number, int bits)
        {
            if (number >= 0)
            {
                return number >> bits;
            }
            return (number >> bits) + (2 << ~bits);
        }

        /// <summary>
        /// 右移位操作
        /// </summary>
        /// <param name="number"></param>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static long RightShift(long number, int bits)
        {
            if (number >= 0)
            {
                return number >> bits;
            }
            return (number >> bits) + (2L << ~bits);
        }
    }
}
