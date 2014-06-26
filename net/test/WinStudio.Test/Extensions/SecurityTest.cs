using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WinStudio.Security;

namespace WinStudio.Test.Extensions
{
    [TestFixture]
    public class SecurityTest
    {
        [TestCase]
        public void TestSecurity()
        {
            string msg = "abc";

            string md5 = msg.ToMD5();
            string sh1 = msg.ToSHA1();

            Assert.IsTrue(Signer.Create(AlgorithmName.md5).VerifySign(msg, md5));
            Assert.IsTrue(Signer.Create(AlgorithmName.sha1).VerifySign(msg, sh1));
        }

        public void TestRsa()
        {
            string msg = "abc123@itripping.cn";
            var keys = RsaDecrypter.GenerateKeys();
        }
    }
}
