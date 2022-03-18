using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Org.BouncyCastle.Asn1;

namespace OfdSharp.Ses
{
    /// <summary>
    /// 自定义属性字段序列
    /// </summary>
    public class ExtensionData : Asn1Encodable, IEnumerable<ExtraData>
    {
        private readonly List<ExtraData> _dataSequence;

        public ExtensionData(IEnumerable<ExtraData> extraData)
        {
            _dataSequence = extraData.ToList();
        }

        public ExtensionData(Asn1Sequence seq)
        {
            _dataSequence = new List<ExtraData>(seq.Count);
            for (int i = 0; i != seq.Count; i++)
            {
                Add(ExtraData.GetInstance(seq[i]));
            }
        }

        public ExtensionData Add(ExtraData o)
        {
            _dataSequence.Add(o);
            return this;
        }

        public ExtraData this[int index] => _dataSequence[index];

        public override Asn1Object ToAsn1Object()
        {
            Asn1EncodableVector v = new Asn1EncodableVector(_dataSequence.Count);
            _dataSequence.ForEach(item => v.Add(item));
            return new DerSequence(v);
        }

        public static ExtensionData GetInstance(object obj)
        {
            if (obj is ExtensionData extensionData)
            {
                return extensionData;
            }
            return obj != null ? new ExtensionData(Asn1Sequence.GetInstance(obj)) : null;
        }

        public IEnumerator<ExtraData> GetEnumerator()
        {
            return _dataSequence.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
