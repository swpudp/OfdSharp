using System;
using System.IO;

namespace OfdSharp.Extensions
{
    /// <summary>
    /// 流扩展
    /// </summary>
    internal static class StreamExtension
    {
        // /// <summary>
        // /// 读取流
        // /// </summary>
        // /// <param name="stream"></param>
        // /// <returns></returns>
        // public static byte[] ToArray(this Stream stream)
        // {
        //     using (MemoryStream memory = new MemoryStream())
        //     {
        //         stream.CopyTo(memory);
        //         return memory.ToArray();
        //     }
        // }

        /// <summary>
        /// 读取stream
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] ReadToEnd(this Stream inputStream)
        {
            if (!inputStream.CanRead)
            {
                throw new NotSupportedException("stream can not read");
            }

            if (!inputStream.CanSeek)
            {
                throw new NotSupportedException("stream can not seek");
            }

            byte[] output = new byte[inputStream.Length];
            int bytesRead = inputStream.Read(output, 0, output.Length);
            if (bytesRead != output.Length)
            {
                throw new IOException("Bytes read from stream not matches stream length");
            }

            return output;
        }
    }
}