using System.Collections;
using System.Collections.Generic;
using Org.BouncyCastle.Asn1;

namespace OfdSharp.Ses.V4
{
    /// <summary>
    /// 签章者证书列表
    /// </summary>
    public class CertInfoCollect : Asn1Encodable, IEnumerable<Asn1OctetString>
    {
        private readonly List<Asn1OctetString> _dataSequence = new List<Asn1OctetString>();

        public CertInfoCollect(List<Asn1OctetString> asn1OctetStrings)
        {
            _dataSequence = asn1OctetStrings;
        }

        public CertInfoCollect(Asn1Sequence sequence)
        {
            for (int i = 0; i < sequence.Count; i++)
            {
                _dataSequence.Add(Asn1OctetString.GetInstance(sequence[i]));
            }
        }

        public CertInfoCollect Add(Asn1OctetString obj)
        {
            _dataSequence.Add(obj);
            return this;
        }

        public Asn1OctetString this[int index] => _dataSequence[index];

        public override Asn1Object ToAsn1Object()
        {
            Asn1EncodableVector vector = new Asn1EncodableVector(_dataSequence.Count);
            foreach (Asn1OctetString asn1OctetString in _dataSequence)
            {
                vector.Add(asn1OctetString);
            }
            return new DerSequence(vector);
        }

        public static CertInfoCollect GetInstance(object obj)
        {
            if (obj is CertInfoCollect certInfoCollect)
            {
                return certInfoCollect;
            }
            return obj != null ? new CertInfoCollect(Asn1Sequence.GetInstance(obj)) : null;
        }

        public IEnumerator<Asn1OctetString> GetEnumerator()
        {
            return _dataSequence.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
