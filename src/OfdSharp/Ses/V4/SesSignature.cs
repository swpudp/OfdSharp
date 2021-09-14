using System.Collections;
using OfdSharp.Extensions;
using Org.BouncyCastle.Asn1;

namespace OfdSharp.Ses.V4
{
    /// <summary>
    /// 电子签章数据
    /// </summary>
    public class SesSignature : Asn1Encodable
    {
        /// <summary>
        /// 签章者证书
        /// </summary>
        public TbsSign TbsSign { get; }

        /// <summary>
        /// 签章者证书
        /// </summary>
        public Asn1OctetString Cert { get; }

        /// <summary>
        /// 签名算法标识
        /// </summary>
        public DerObjectIdentifier SignatureAlgId { get; }

        /// <summary>
        /// 签名值
        /// </summary>
        public DerBitString Signature { get; private set; }

        /// <summary>
        /// 对签名值的时间戳
        /// </summary>
        public DerBitString TimeStamp { get; }

        public SesSignature(TbsSign toSign, Asn1OctetString cert, DerObjectIdentifier signatureAlgId, DerBitString signature, DerBitString timeStamp)
        {
            TbsSign = toSign;
            Cert = cert;
            SignatureAlgId = signatureAlgId;
            Signature = signature;
            TimeStamp = timeStamp;
        }

        public SesSignature(TbsSign toSign, Asn1OctetString cert, DerObjectIdentifier signatureAlgId, DerBitString signature)
        {
            TbsSign = toSign;
            Cert = cert;
            SignatureAlgId = signatureAlgId;
            Signature = signature;
        }

        public SesSignature(Asn1Sequence seq)
        {
            IEnumerator e = seq.GetEnumerator();
            TbsSign = TbsSign.GetInstance(e.Next());
            Cert = Asn1OctetString.GetInstance(e.Next());
            SignatureAlgId = DerObjectIdentifier.GetInstance(e.Next());
            Signature = DerBitString.GetInstance(e.Next());
            if (e.MoveNext())
            {
                TimeStamp = DerBitString.GetInstance(e.Current);
            }
        }

        public static SesSignature GetInstance(object o)
        {
            if (o is SesSignature sesSignature)
            {
                return sesSignature;
            }
            return o != null ? new SesSignature(Asn1Sequence.GetInstance(o)) : null;
        }

        public SesSignature SetSignature(byte[] signature)
        {
            Signature = new DerBitString(signature);
            return this;
        }

        public override Asn1Object ToAsn1Object()
        {
            Asn1EncodableVector v = new Asn1EncodableVector(5) { TbsSign, Cert, SignatureAlgId, Signature };
            if (TimeStamp != null)
            {
                v.Add(TimeStamp);
            }
            return new DerSequence(v);
        }
    }
}
