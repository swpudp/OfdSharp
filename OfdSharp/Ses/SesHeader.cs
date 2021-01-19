using System.Collections;
using OfdSharp.Extensions;
using Org.BouncyCastle.Asn1;

namespace OfdSharp.Ses
{
    public class SesHeader : Asn1Encodable
    {
        /**
         * 电子印章数据结构版本号，V4
         */
        public static readonly DerInteger V4 = new DerInteger(4);

        /// <summary>
        /// 电子印章数据标识符
        /// 固定值“ES”
        /// </summary>
        private static readonly DerIA5String Identified = new DerIA5String("ES");

        /// <summary>
        /// id
        /// </summary>
        public DerIA5String Id { get; }

        /// <summary>
        /// 电子印章数据版本号标识，V4
        /// </summary>
        public DerInteger Version { get; }

        /// <summary>
        /// 电子印章厂商ID
        /// 在互联互通时，用于识别不同的软件厂商实现
        /// </summary>
        public DerIA5String Vid { get; set; }

        public SesHeader(DerInteger version, DerIA5String manufacturer)
        {
            Id = Identified;
            Version = version;
            Vid = manufacturer;
        }

        public SesHeader(Asn1Sequence seq)
        {
            IEnumerator e = seq.GetEnumerator();
            Id = DerIA5String.GetInstance(e.Next());
            Version = DerInteger.GetInstance(e.Next());
            Vid = DerIA5String.GetInstance(e.Next());
        }

        public static SesHeader GetInstance(object o)
        {
            if (o is SesHeader sesHeader)
            {
                return sesHeader;
            }
            return o != null ? new SesHeader(Asn1Sequence.GetInstance(o)) : null;
        }

        public override Asn1Object ToAsn1Object()
        {
            Asn1EncodableVector v = new Asn1EncodableVector(3) { Id, Version, Vid };
            return new DerSequence(v);
        }
    }
}
