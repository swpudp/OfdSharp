using System.Collections;
using Org.BouncyCastle.Asn1;

namespace OfdSharp.Ses.V1
{
    /// <summary>
    /// 厂商自定义数据
    /// </summary>
    public class ExtraData : Asn1Encodable
    {
        /// <summary>
        /// 自定义扩展字段标识
        /// </summary>
        public DerObjectIdentifier ExtId { get; }

        /// <summary>
        /// 自定义扩展字段是否关键
        /// </summary>
        public DerBoolean Critical { get; }

        /// <summary>
        /// 自定义扩展字段数据值
        /// </summary>
        public Asn1OctetString ExtValue { get; }

        public ExtraData(DerObjectIdentifier extId, DerBoolean critical, Asn1OctetString extValue) : base()
        {
            ExtId = extId;
            Critical = critical;
            ExtValue = extValue;
        }

        public ExtraData(Asn1Sequence seq)
        {
            IEnumerator enumerator = seq.GetEnumerator();

            enumerator.MoveNext();
            ExtId = DerObjectIdentifier.GetInstance(enumerator.Current);

            enumerator.MoveNext();
            Critical = DerBoolean.GetInstance(enumerator.Current);

            enumerator.MoveNext();
            ExtValue = Asn1OctetString.GetInstance(enumerator.Current);
        }

        public static ExtraData GetInstance(object o)
        {
            if (o is ExtraData extraData)
            {
                return extraData;
            }
            return o != null ? new ExtraData(Asn1Sequence.GetInstance(o)) : null;
        }
        public override Asn1Object ToAsn1Object()
        {
            Asn1EncodableVector vector = new Asn1EncodableVector { ExtId, ExtValue, Critical };
            return new DerSequence(vector);
        }
    }
}
