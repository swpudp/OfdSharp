using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;

namespace OfdSharp.Crypto
{
    public class Cipher
    {
        private int _ct;

        private ECPoint _p2;

        private SM3Digest _sm3KeyBase;

        private SM3Digest _sm3C3;

        private readonly byte[] _key;

        private byte _keyOff;

        public Cipher()
        {
            _ct = 1;
            _key = new byte[32];
            _keyOff = 0;
        }

        private static byte[] ByteConvert32Bytes(BigInteger n)
        {
            byte[] array;
            if (n.ToByteArray().Length == 33)
            {
                array = new byte[32];
                Array.Copy(n.ToByteArray(), 1, array, 0, 32);
            }
            else if (n.ToByteArray().Length == 32)
            {
                array = n.ToByteArray();
            }
            else
            {
                array = new byte[32];
                for (int i = 0; i < 32 - n.ToByteArray().Length; i++)
                {
                    array[i] = 0;
                }
                Array.Copy(n.ToByteArray(), 0, array, 32 - n.ToByteArray().Length, n.ToByteArray().Length);
            }
            return array;
        }

        private void Reset()
        {
            _sm3KeyBase = new SM3Digest();
            _sm3C3 = new SM3Digest();
            byte[] array = ByteConvert32Bytes(_p2.Normalize().XCoord.ToBigInteger());
            _sm3KeyBase.BlockUpdate(array, 0, array.Length);
            _sm3C3.BlockUpdate(array, 0, array.Length);
            array = ByteConvert32Bytes(_p2.Normalize().YCoord.ToBigInteger());
            _sm3KeyBase.BlockUpdate(array, 0, array.Length);
            _ct = 1;
            NextKey();
        }

        private void NextKey()
        {
            SM3Digest sM3Digest = new SM3Digest(_sm3KeyBase);
            sM3Digest.Update((byte)((_ct >> 24) & 0xFF));
            sM3Digest.Update((byte)((_ct >> 16) & 0xFF));
            sM3Digest.Update((byte)((_ct >> 8) & 0xFF));
            sM3Digest.Update((byte)(_ct & 0xFF));
            sM3Digest.DoFinal(_key, 0);
            _keyOff = 0;
            _ct++;
        }

        public ECPoint InitEnc(SM2Engine sm2, ECPoint userKey)
        {
            AsymmetricCipherKeyPair val = sm2.EccKeyPairGenerator.GenerateKeyPair();
            ECPrivateKeyParameters val2 = (ECPrivateKeyParameters)val.Private;
            ECPublicKeyParameters val3 = (ECPublicKeyParameters)val.Public;
            BigInteger d = val2.D;
            ECPoint q = val3.Q;
            _p2 = userKey.Multiply(d);
            Reset();
            return q;
        }

        public void Encrypt(byte[] data)
        {
            _sm3C3.BlockUpdate(data, 0, data.Length);
            for (int i = 0; i < data.Length; i++)
            {
                if (_keyOff == _key.Length)
                {
                    NextKey();
                }
                data[i] ^= _key[_keyOff++];
            }
        }

        public void InitDec(BigInteger userD, ECPoint c1)
        {
            _p2 = c1.Multiply(userD);
            Reset();
        }

        public void Decrypt(byte[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (_keyOff == _key.Length)
                {
                    NextKey();
                }
                data[i] ^= _key[_keyOff++];
            }
            _sm3C3.BlockUpdate(data, 0, data.Length);
        }

        public void DoFinal(byte[] c3)
        {
            byte[] array = ByteConvert32Bytes(_p2.Normalize().YCoord.ToBigInteger());
            _sm3C3.BlockUpdate(array, 0, array.Length);
            _sm3C3.DoFinal(c3, 0);
            Reset();
        }
    }
}
