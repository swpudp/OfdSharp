using System.Collections;
using OfdSharp.Extensions;
using Org.BouncyCastle.Asn1;

namespace OfdSharp.Ses.V1
{
    /// <summary>
    /// 印章信息
    /// </summary>
    public class SesSealInfo : Asn1Encodable
    {
        /// <summary>
        /// 头信息
        /// </summary>
        public SesHeader Header { get; }

        /// <summary>
        /// 电子印章标识符
        /// </summary>
        public DerIA5String EsId { get; }

        /// <summary>
        /// 印章属性信息
        /// </summary>
        public EsPropertyInfo Property { get; }

        /// <summary>
        /// 电子印章图片数据
        /// </summary>
        public SesPictureInfo Picture { get; }

        /// <summary>
        /// 自定义数据
        /// </summary>
        public ExtensionData ExtData { get; }

        public SesSealInfo() { }

        public SesSealInfo(SesHeader header, DerIA5String esId, EsPropertyInfo property, SesPictureInfo picture,ExtensionData extData)
        {
            Header = header;
            EsId = esId;
            Property = property;
            Picture = picture;
            ExtData = extData;
        }

        public SesSealInfo(Asn1Sequence seq)
        {
            IEnumerator e = seq.GetEnumerator();
            Header = SesHeader.GetInstance(e.Next());
            EsId = DerIA5String.GetInstance(e.Next());
            Property = EsPropertyInfo.GetInstance(e.Next());
            Picture = SesPictureInfo.GetInstance(e.Next());
            if (e.MoveNext())
            {
                ExtData = ExtensionData.GetInstance(e.Next());
            }
        }

        public static SesSealInfo GetInstance(object o)
        {
            if (o is SesSealInfo sesSealInfo)
            {
                return sesSealInfo;
            }
            return o != null ? new SesSealInfo(Asn1Sequence.GetInstance(o)) : null;
        }

        public override Asn1Object ToAsn1Object()
        {
            Asn1EncodableVector v = new Asn1EncodableVector(5) { Header, EsId, Property, Picture };
            if (ExtData != null)
            {
                v.Add(ExtData);
            }
            return new DerSequence(v);
        }
    }
}
