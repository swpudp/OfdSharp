using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;

namespace OfdSharp.Crypto
{
    public class Sm2
    {
        private static readonly string[] Sm2Param = new string[6]
        {
            "FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00000000FFFFFFFFFFFFFFFF",
            "FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00000000FFFFFFFFFFFFFFFC",
            "28E9FA9E9D9F5E344D5A9E4BCF6509A7F39789F515AB8F92DDBCBD414D940E93",
            "FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFF7203DF6B21C6052B53BBF40939D54123",
            "32C4AE2C1F1981195F9904466A39C9948FE30BBFF2660BE1715A4589334C74C7",
            "BC3736A2F4F6779C59BDCEE36B692153D0A9877CC62A474002DF32E52139F0A0"
        };

        public readonly string[] EccParam;

        public readonly BigInteger EccP;

        public readonly BigInteger EccA;

        public readonly BigInteger EccB;

        public readonly BigInteger EccN;

        public readonly BigInteger EccGx;

        public readonly BigInteger EccGy;

        public readonly ECCurve EccCurve;

        public readonly ECPoint EccPointG;

        public readonly ECDomainParameters EccBcSpec;

        public readonly ECKeyPairGenerator EccKeyPairGenerator;

        public Sm2()
        {
            EccParam = Sm2Param;
            EccP = new BigInteger(EccParam[0], 16);
            EccA = new BigInteger(EccParam[1], 16);
            EccB = new BigInteger(EccParam[2], 16);
            EccN = new BigInteger(EccParam[3], 16);
            EccGx = new BigInteger(EccParam[4], 16);
            EccGy = new BigInteger(EccParam[5], 16);
            EccCurve = new FpCurve(EccP, EccA, EccB, null, null);
            EccPointG = EccCurve.CreatePoint(EccGx, EccGy);
            EccBcSpec = new ECDomainParameters(EccCurve, EccPointG, EccN);
            ECKeyGenerationParameters val = new ECKeyGenerationParameters(EccBcSpec, new SecureRandom());
            EccKeyPairGenerator = new ECKeyPairGenerator();
            EccKeyPairGenerator.Init(val);
        }

        public virtual byte[] Sm2GetZ(byte[] userId, ECPoint userKey)
        {
            Sm3Digest sM3Digest = new Sm3Digest();
            int num = userId.Length * 8;
            sM3Digest.Update((byte)((num >> 8) & 0xFF));
            sM3Digest.Update((byte)(num & 0xFF));
            sM3Digest.BlockUpdate(userId, 0, userId.Length);
            byte[] array = EccA.ToByteArray();
            sM3Digest.BlockUpdate(array, 0, array.Length);
            array = EccB.ToByteArray();
            sM3Digest.BlockUpdate(array, 0, array.Length);
            array = EccGx.ToByteArray();
            sM3Digest.BlockUpdate(array, 0, array.Length);
            array = EccGy.ToByteArray();
            sM3Digest.BlockUpdate(array, 0, array.Length);
            array = userKey.AffineXCoord.ToBigInteger().ToByteArray();
            sM3Digest.BlockUpdate(array, 0, array.Length);
            array = userKey.AffineYCoord.ToBigInteger().ToByteArray();
            sM3Digest.BlockUpdate(array, 0, array.Length);
            byte[] array2 = new byte[sM3Digest.GetDigestSize()];
            sM3Digest.DoFinal(array2, 0);
            return array2;
        }
    }
}
