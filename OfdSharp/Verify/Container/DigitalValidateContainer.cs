using OfdSharp.Core.Signs;
using OfdSharp.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities.Encoders;
using System;

namespace OfdSharp.Verify.Container
{
    /// <summary>
    /// 《《GM/T 0031-2014 安全电子签章密码技术规范》 电子印章数据验证
    /// 注意：仅用于测试，电子签章验证请使用符合国家规范的流程进行！
    /// </summary>
    public class DigitalValidateContainer : SignedDataValidateContainer
    {
        /// <summary>
        /// 验证使用的公钥
        /// </summary>
        private ECPublicKeyParameters _pk;

        public DigitalValidateContainer(ECPublicKeyParameters pk)
        {
            _pk = pk ?? throw new ArgumentNullException(nameof(pk));
        }


        //public DigitalValidateContainer(X509Certificate certificate)
        //{
        //    _pk = certificate();
        //}


        public override VerifyResult Validate(SigType type, byte[] tbsContent, byte[] signedValue)
        {
            if (type != SigType.Sign)
            {
                throw new ArgumentOutOfRangeException(nameof(type), "签名类型(type)必须是 Sign，不支持电子印章验证");
            }

            Sm2Utils.Verify("", Hex.ToHexString(tbsContent), Hex.ToHexString(signedValue));

            //Signature sg = Signature.getInstance(alg, new BouncyCastleProvider());
            //sg.initVerify(pk);
            //sg.update(tbsContent);
            //if (!sg.verify(signedValue))
            //{
            //    throw new InvalidSignedValueException("签名值不一致");
            //}

            return VerifyResult.Success;
        }
    }
}
