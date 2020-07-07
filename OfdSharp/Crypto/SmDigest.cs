using Org.BouncyCastle.Crypto;
using System;

namespace OfdSharp.Crypto
{
    /// <summary>
    /// 摘要
    /// </summary>
    public abstract class SmDigest : IDigest
    {
        /// <summary>
        /// 加密字节数组
        /// </summary>
        public byte[] Buffer { get; }

        /// <summary>
        /// 加密字节数组最后位置
        /// </summary>
        public int Offset { get; private set; }

        /// <summary>
        /// 字节长度
        /// </summary>
        public long ByteCount { get; private set; }

        /// <summary>
        /// 算法名称
        /// </summary>
        public abstract string AlgorithmName { get; }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        internal SmDigest()
        {
            Buffer = new byte[4];
        }

        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="digest"></param>
        internal SmDigest(SmDigest digest)
        {
            Buffer = digest.Buffer;
            Array.Copy(digest.Buffer, 0, Buffer, 0, digest.Buffer.Length);
            Offset = digest.Offset;
            ByteCount = digest.ByteCount;
        }

        /// <summary>
        /// 更新字节缓存
        /// </summary>
        /// <param name="input"></param>
        public void Update(byte input)
        {
            Buffer[Offset++] = input;
            if (Offset == Buffer.Length)
            {
                ProcessWord(Buffer, 0);
                Offset = 0;
            }
            ByteCount++;
        }

        /// <summary>
        /// 更新块
        /// </summary>
        /// <param name="input"></param>
        /// <param name="inOff"></param>
        /// <param name="length"></param>
        public void BlockUpdate(byte[] input, int inOff, int length)
        {
            while (Offset != 0 && length > 0)
            {
                Update(input[inOff]);
                inOff++;
                length--;
            }
            while (length > Buffer.Length)
            {
                ProcessWord(input, inOff);
                inOff += Buffer.Length;
                length -= Buffer.Length;
                ByteCount += Buffer.Length;
            }
            while (length > 0)
            {
                Update(input[inOff]);
                inOff++;
                length--;
            }
        }

        /// <summary>
        /// 完成
        /// </summary>
        protected void Finish()
        {
            long bitLength = ByteCount << 3;
            Update(128);
            while (Offset != 0)
            {
                Update(0);
            }
            ProcessLength(bitLength);
            ProcessBlock();
        }

        /// <summary>
        /// 重置
        /// </summary>
        public virtual void Reset()
        {
            ByteCount = 0L;
            Offset = 0;
            Array.Clear(Buffer, 0, Buffer.Length);
        }

        /// <summary>
        /// 字节长度
        /// </summary>
        /// <returns></returns>
        public int GetByteLength()
        {
            return 64;
        }

        /// <summary>
        /// 摘要大小
        /// </summary>
        /// <returns></returns>
        public abstract int GetDigestSize();

        /// <summary>
        /// 处理单词
        /// </summary>
        /// <param name="input"></param>
        /// <param name="inOff"></param>
        protected abstract void ProcessWord(byte[] input, int inOff);

        /// <summary>
        /// 处理长度
        /// </summary>
        /// <param name="bitLength"></param>
        protected abstract void ProcessLength(long bitLength);

        /// <summary>
        /// 处理字节块
        /// </summary>
        protected abstract void ProcessBlock();

        /// <summary>
        /// 完成加密
        /// </summary>
        /// <param name="output"></param>
        /// <param name="outOff"></param>
        /// <returns></returns>
        public abstract int DoFinal(byte[] output, int outOff);
    }
}
