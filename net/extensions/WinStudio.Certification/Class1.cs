using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Certification
{
    public class Class1
    {
        public void test()
        {
            string certPath = @"d:\mine\itripping.pfx";
            X509Certificate2 cer = new X509Certificate2(certPath, "itripping", X509KeyStorageFlags.Exportable);
            Console.WriteLine(cer.HasPrivateKey);

            string msg = "abcdefg@123";
            string enc = RsaDecrypter.Encrypt(msg, cer.PublicKey.Key.ToXmlString(false));
            Console.WriteLine("enc=" + enc);
            enc = RsaDecrypter.Encrypt(msg, cer.PublicKey.Key.ToXmlString(false));
            Console.WriteLine("enc=" + enc);
            string dec = RsaDecrypter.Decrypt(enc, cer.PrivateKey.ToXmlString(true));
            Console.WriteLine("dec=" + dec);


            certPath = @"d:\itripping.cn.pfx";
            cer = new X509Certificate2(certPath, "itripping", X509KeyStorageFlags.Exportable);
            dec = RsaDecrypter.Decrypt(enc, cer.PrivateKey.ToXmlString(true));
            Console.WriteLine("dec=" + dec);
        }
    }
}
