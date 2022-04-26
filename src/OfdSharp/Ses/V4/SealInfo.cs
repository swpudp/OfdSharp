using System.Collections;
using OfdSharp.Extensions;
using Org.BouncyCastle.Asn1;

namespace OfdSharp.Ses.V4
{
    public class SealInfo : Asn1Encodable
    {
        /// <summary>
        /// 头信息
        /// </summary>
        public SesHeader Header { get; set; }


        /// <summary>
        /// 电子印章标识符
        /// 电子印章数据唯一标识编码
        /// </summary>
        public DerIA5String EsId { get; set; }

        /// <summary>
        /// 印章属性信息
        /// </summary>
        public SesPropertyInfo Property { get; set; }

        /// <summary>
        /// 电子印章图片数据
        /// </summary>
        public SesPictureInfo Picture { get; set; }

        /// <summary>
        /// 自定义数据
        /// </summary>
        public ExtensionData ExtensionData { get; set; }

        public SealInfo() { }

        public SealInfo(SesHeader header, DerIA5String esId, SesPropertyInfo property, SesPictureInfo picture, ExtensionData extData)
        {
            Header = header;
            EsId = esId;
            Property = property;
            Picture = picture;
            ExtensionData = extData;
        }

        public SealInfo(Asn1Sequence seq)
        {
            IEnumerator e = seq.GetEnumerator();
            Header = SesHeader.GetInstance(e.Next());
            EsId = DerIA5String.GetInstance(e.Next());
            Property = SesPropertyInfo.GetInstance(e.Next());
            Picture = SesPictureInfo.GetInstance(e.Next());
            if (e.MoveNext())
            {
                ExtensionData = ExtensionData.GetInstance(e.Current);
            }
        }

        public static SealInfo GetInstance(object o)
        {
            if (o is SealInfo sealInfo)
            {
                return sealInfo;
            }
            return o != null ? new SealInfo(Asn1Sequence.GetInstance(o)) : null;
        }

        public override Asn1Object ToAsn1Object()
        {
            Asn1EncodableVector v = new Asn1EncodableVector(5) { Header, EsId, Property, Picture };
            if (ExtensionData != null)
            {
                v.Add(ExtensionData);
            }
            return new DerSequence(v);
        }
    }
}
