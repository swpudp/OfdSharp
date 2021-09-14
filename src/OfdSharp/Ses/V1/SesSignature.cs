using OfdSharp.Extensions;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections;

namespace OfdSharp.Ses.V1
{
    /// <summary>
    /// 电子签章数据
    /// </summary>
    public class SesSignature : Asn1Encodable
    {
        /// <summary>
        /// 待电子签章数据
        /// </summary>
        public TbsSign ToSign { get; }

        /// <summary>
        /// 电子签章中签名值
        /// </summary>
        public DerBitString Signature { get; }

        public SesSignature(TbsSign toSign, DerBitString signature)
        {
            ToSign = toSign;
            Signature = signature;
        }

        public SesSignature(Asn1Sequence seq)
        {
            IEnumerator e = seq.GetEnumerator();
            ToSign = TbsSign.GetInstance(e.Next());
            Signature = DerBitString.GetInstance(e.Next());
        }

        public static SesSignature GetInstance(Object o)
        {
            if (o is SesSignature sesSignature)
            {
                return sesSignature;
            }
            return o != null ? new SesSignature(Asn1Sequence.GetInstance(o)) : null;
        }

        public override Asn1Object ToAsn1Object()
        {
            Asn1EncodableVector v = new Asn1EncodableVector(2) { ToSign, Signature };
            return new DerSequence(v);
        }
    }
}
