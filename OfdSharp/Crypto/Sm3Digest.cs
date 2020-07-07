using System;

namespace OfdSharp.Crypto
{
    /// <summary>
	/// SM3摘要
    /// </summary>
	public class Sm3Digest : SmDigest
    {
        /// <summary>
        /// 摘要长度
        /// </summary>
        private const int DigestLength = 32;

        /// <summary>
        /// 初始值V0
        /// </summary>
        private static readonly int[] V0 = {
            1937774191,
            1226093241,
            388252375,
            -628488704,
            -1452330820,
            372324522,
            -477237683,
            -1325724082
        };

        private readonly int[] _v = new int[8];

        private readonly int[] _v1 = new int[8];

        private static readonly int[] X0 = new int[16];

        private static int[] X => new int[68];

        private int _offset;

        private const int T0 = 2043430169;

        private const int T1 = 2055708042;

        public override string AlgorithmName => "SM3";

        public override int GetDigestSize()
        {
            return DigestLength;
        }

        public Sm3Digest() { }

        public Sm3Digest(Sm3Digest t) : base(t)
        {
            Array.Copy(X, 0, X, 0, X.Length);
            _offset = t._offset;
            Array.Copy(t._v, 0, _v, 0, t._v.Length);
        }

        public override void Reset()
        {
            base.Reset();
            Array.Copy(V0, 0, _v, 0, V0.Length);
            _offset = 0;
            Array.Copy(X0, 0, X, 0, X0.Length);
        }

        /// <summary>
        /// 处理块消息
        /// </summary>
        protected override void ProcessBlock()
        {
            int[] x = X;
            int[] array = new int[64];
            for (int i = 16; i < 68; i++)
            {
                x[i] = P1(x[i - 16] ^ x[i - 9] ^ Rotate(x[i - 3], 15)) ^ Rotate(x[i - 13], 7) ^ x[i - 6];
            }
            for (int i = 0; i < 64; i++)
            {
                array[i] = x[i] ^ x[i + 4];
            }
            int[] array2 = _v;
            int[] array3 = _v1;
            Array.Copy(array2, 0, array3, 0, V0.Length);
            for (int i = 0; i < 16; i++)
            {
                int num = Rotate(array3[0], 12);
                int x2 = num + array3[4] + Rotate(T0, i);
                x2 = Rotate(x2, 7);
                int num2 = x2 ^ num;
                int num3 = Ff0(array3[0], array3[1], array3[2]) + array3[3] + num2 + array[i];
                int x3 = Gg0(array3[4], array3[5], array3[6]) + array3[7] + x2 + x[i];
                array3[3] = array3[2];
                array3[2] = Rotate(array3[1], 9);
                array3[1] = array3[0];
                array3[0] = num3;
                array3[7] = array3[6];
                array3[6] = Rotate(array3[5], 19);
                array3[5] = array3[4];
                array3[4] = P0(x3);
            }
            for (int i = 16; i < 64; i++)
            {
                int num = Rotate(array3[0], 12);
                int x2 = num + array3[4] + Rotate(T1, i);
                x2 = Rotate(x2, 7);
                int num2 = x2 ^ num;
                int num3 = Ff1(array3[0], array3[1], array3[2]) + array3[3] + num2 + array[i];
                int x3 = Gg1(array3[4], array3[5], array3[6]) + array3[7] + x2 + x[i];
                array3[3] = array3[2];
                array3[2] = Rotate(array3[1], 9);
                array3[1] = array3[0];
                array3[0] = num3;
                array3[7] = array3[6];
                array3[6] = Rotate(array3[5], 19);
                array3[5] = array3[4];
                array3[4] = P0(x3);
            }
            for (int i = 0; i < 8; i++)
            {
                array2[i] ^= array3[i];
            }
            _offset = 0;
            Array.Copy(X0, 0, X, 0, X0.Length);
        }

        protected override void ProcessWord(byte[] input, int offset)
        {
            int num = input[offset] << 24;
            num |= (input[++offset] & 0xFF) << 16;
            num |= (input[++offset] & 0xFF) << 8;
            num |= input[++offset] & 0xFF;
            X[this._offset] = num;
            if (++this._offset == 16)
            {
                ProcessBlock();
            }
        }

        protected override void ProcessLength(long bitLength)
        {
            if (_offset > 14)
            {
                ProcessBlock();
            }
            X[14] = (int)ByteUtils.RightShift(bitLength, 32);
            X[15] = (int)(bitLength & -1);
        }

        /// <summary>
        /// 初始化到大端存储
        /// </summary>
        /// <param name="n"></param>
        /// <param name="bs"></param>
        /// <param name="off"></param>
        private static void Init(int n, byte[] bs, int off)
        {
            bs[off] = (byte)ByteUtils.RightShift(n, 24);//取最高位字节
            bs[++off] = (byte)ByteUtils.RightShift(n, 16);//第三位字节
            bs[++off] = (byte)ByteUtils.RightShift(n, 8);//第二位字节
            bs[++off] = (byte)n;//第一位字节
        }

        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="output"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public override int DoFinal(byte[] output, int offset)
        {
            Finish();
            for (int i = 0; i < 8; i++)
            {
                Init(_v[i], output, offset + i * 4);
            }
            Reset();
            return 32;
        }

        /// <summary>
        /// 旋转
        /// </summary>
        /// <param name="x">x轴</param>
        /// <param name="n">长度</param>
        /// <returns></returns>
        private static int Rotate(int x, int n)
        {
            return (x << n) | ByteUtils.RightShift(x, 32 - n);
        }

        /// <summary>
        /// 消息填充
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static int P0(int x)
        {
            return x ^ Rotate(x, 9) ^ Rotate(x, 17);
        }

        /// <summary>
        /// 消息填充
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static int P1(int x)
        {
            return x ^ Rotate(x, 15) ^ Rotate(x, 23);
        }

        /// <summary>
        /// 0-15位
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        private static int Ff0(int x, int y, int z)
        {
            return x ^ y ^ z;
        }

        /// <summary>
        /// 16-63位
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        private static int Ff1(int x, int y, int z)
        {
            return (x & y) | (x & z) | (y & z);
        }

        /// <summary>
        /// 0-15位
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        private static int Gg0(int x, int y, int z)
        {
            return x ^ y ^ z;
        }

        /// <summary>
        /// 16-63位
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        private static int Gg1(int x, int y, int z)
        {
            return (x & y) | (~x & z);
        }
    }
}
