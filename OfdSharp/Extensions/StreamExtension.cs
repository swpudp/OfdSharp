using System.IO;

namespace OfdSharp.Extensions
{
    /// <summary>
    /// 流扩展
    /// </summary>
    internal static class StreamExtension
    {
        /// <summary>
        /// 读取流
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ToArray(this Stream stream)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                stream.CopyTo(memory);
                return memory.ToArray();
            }
        }
    }
}
