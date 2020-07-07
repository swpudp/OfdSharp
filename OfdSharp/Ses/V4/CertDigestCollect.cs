using System;
using System.Collections;
using System.Collections.Generic;
using Org.BouncyCastle.Asn1;

namespace OfdSharp.Ses.V4
{
    /// <summary>
    /// 签章者证书杂凑值列表
    /// </summary>
    public class CertDigestCollect : Asn1Encodable, IEnumerable<CertDigestObj>
    {
        /**
        * 签章者证书杂凑值
        */
        private readonly List<CertDigestObj> _dataSequence;

        public CertDigestCollect(Asn1Sequence sequence)
        {
            _dataSequence = new List<CertDigestObj>();
            for (int i = 0; i < sequence.Count; i++)
            {
                _dataSequence.Add(CertDigestObj.GetInstance(sequence[i]));
            }
        }

        public override Asn1Object ToAsn1Object()
        {
            Asn1EncodableVector vector = new Asn1EncodableVector();
            foreach (CertDigestObj certDigestObj in _dataSequence)
            {
                vector.Add(certDigestObj);
            }
            return new DerSequence(vector);
        }

        public static CertDigestCollect GetInstance(Object obj)
        {
            if (obj is CertDigestCollect digestCollect)
            {
                return digestCollect;
            }
            return obj != null ? new CertDigestCollect(Asn1Sequence.GetInstance(obj)) : null;
        }

        public IEnumerator<CertDigestObj> GetEnumerator()
        {
            return _dataSequence.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
