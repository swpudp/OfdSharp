﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfdSharp.Reader;
using OfdSharp.Verify;
using Org.BouncyCastle.X509;
using System;
using System.IO;
using OfdSharp.Primitives.Pages.Description.ColorSpace;
using OpenSsl.Crypto.Utility;

namespace UnitTests.Verify
{
    /// <summary>
    /// OFD电子签名验证引擎测试
    /// </summary>
    [TestClass]
    public class OfdValidatorTests
    {
        /// <summary>
        /// OFD电子签名验证
        /// </summary>
        [TestMethod]
        public void ExeValidateTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "test.ofd");
            OfdReader reader = new OfdReader(filePath);
            VerifyResult verifyResult = OfdValidator.Validate(reader);
            Assert.IsNotNull(verifyResult);
            Assert.AreEqual(VerifyResult.Success, verifyResult);
        }

        /// <summary>
        /// OFD电子签名验证
        /// </summary>
        [TestMethod]
        public void ExeValidateExecuteTest()
        {
            string filePath = "C:/Users/dingpin/Desktop/20220303.ofd";
            OfdReader reader = new OfdReader(filePath);
            VerifyResult verifyResult = OfdValidator.Validate(reader);
            Assert.IsNotNull(verifyResult);
            Assert.AreEqual(VerifyResult.Success, verifyResult);
        }

        /// <summary>
        /// OFD电子签名验证
        /// </summary>
        [TestMethod]
        public void ExeValidateInvalidFileTest()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "test2.ofd");
            OfdReader reader = new OfdReader(filePath);
            VerifyResult verifyResult = OfdValidator.Validate(reader);
            Assert.IsNotNull(verifyResult);
            Assert.AreEqual(VerifyResult.Success, verifyResult);
        }

        /// <summary>
        /// 证书验证测试
        /// </summary>
        [TestMethod]
        public void VerifyTest()
        {
            byte[] certDer = Convert.FromBase64String("MIIEdTCCBBmgAwIBAgIHFAMAAAAQeTAMBggqgRzPVQGDdQUAMFgxCzAJBgNVBAYTAkNOMRswGQYDVQQLDBLlm73lrrbnqI7liqHmgLvlsYAxLDAqBgNVBAMMI+eojuWKoeeUteWtkOivgeS5pueuoeeQhuS4reW/gyhTTTIpMB4XDTE5MTAxMTAwMDAwMFoXDTI0MTAxMTAwMDAwMFowXzELMAkGA1UEBhMCQ04xITAfBgNVBAsMGOW+geeuoeWSjOenkeaKgOWPkeWxleWkhDEtMCsGA1UEAwwk5Zu95a6256iO5Yqh5oC75bGA5rGf6IuP55yB56iO5Yqh5bGAMFkwEwYHKoZIzj0CAQYIKoEcz1UBgi0DQgAE2WSrTgYPKq3lIjmGPXoiSCg/I1hFGN+Ny/K515u7lK33QNs0Xr2gGjS3ZPVoRclrbKIBbQz7d8xRCIODgkG9paOCAsMwggK/MA4GA1UdDwEB/wQEAwID+DAMBgNVHRMBAf8EAjAAMB8GA1UdIwQYMBaAFLiwmmAH6gW3sLfny0J2MSmXFXazMB0GA1UdDgQWBBRf9zxqhfCp3dOSzfa8N5BDVuyTXDBEBgkqhkiG9w0BCQ8ENzA1MA4GCCqGSIb3DQMCAgIAgDAOBggqhkiG9w0DBAICAIAwBwYFKw4DAgcwCgYIKoZIhvcNAwcwgf0GA1UdHwSB9TCB8jB7oHmgd4Z1aHR0cDovL3RheGlhLmppYW5nc3UuY2hpbmF0YXguZ292LmNuOjIzODkvY249Y3JsMTQwMyxvdT1jcmwxNCxvdT1jcmwsYz1jbj9jZXJ0aWZpY2F0ZVJldm9jYXRpb25MaXN0LCo/YmFzZT9jbj1jcmwxNDAzMHOgcaBvhm1sZGFwOi8vdGF4aWEuY2hpbmF0YXguZ292LmNuOjIzODkvY249Y3JsMTQwMyxvdT1jcmwxNCxvdT1jcmwsYz1jbj9jZXJ0aWZpY2F0ZVJldm9jYXRpb25MaXN0LCo/YmFzZT9jbj1jcmwxNDAzMEcGCCsGAQUFBwEBBDswOTA3BggrBgEFBQcwAYYraHR0cDovL3RheGlhLmppYW5nc3UuY2hpbmF0YXguZ292LmNuOjE2NTg4LzAjBgorBgEEAalDZAUIBBUMEzAzLTAwMDEwMjkwOTI4NTQ1NDkwIwYKKwYBBAGpQ2QFCQQVDBMwMy0wMDAxMDI5MDkyODU0NTQ5MBIGCisGAQQBqUNkAgEEBAwCMTkwHQYFKlYLBwIEFAwS5rGf6IuP55yB56iO5Yqh5bGAMBYGBSpWCwcDBA0MCzEzMjAwMDAwMDAwMBsGBSpWCwcFBBIMEDAwMDEwMjkwOTI4NTQ1NDkwHgYIYIZIAYb4QwkEEgwQMDEwMDE0MDAwMDAwMDA4MjAMBggqgRzPVQGDdQUAA0gAMEUCIQDt7obW8LnEh+i07RsveubyA10lJbKeffdX2kT1SZjb7AIgH4egqQlLggtDvcpTWdC2v617m3C6EQdXwe4JLI/rDg0=");

            X509CertificateParser parser = new X509CertificateParser();
            var cert = parser.ReadCertificate(certDer);

            byte[] buf = Convert.FromBase64String("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");

            byte[] expect = Convert.FromBase64String("MEQCICe6czwafac84bKs3VuHhLyR8Ty2nvrt6blHvAPU/vntAiB9dmeT/l17JZR5zJ/lwGQPpSRw+95p761S96DviHVvbQ==");

            Console.WriteLine(Convert.ToBase64String(expect));

            bool result = SignatureUtils.Sm2Verify(cert, buf, expect);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// 枚举读取测试
        /// </summary>
        [TestMethod]
        [DataRow(null)]
        public void EnumReadTest(ColorSpaceType? type)
        {
            Assert.AreEqual(0, Convert.ToInt32(type));
            type = ColorSpaceType.CMYK;
            Assert.AreEqual(2, Convert.ToInt32(type));
        }
    }
}