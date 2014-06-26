using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace System.Security
{
    public class CertificationSigner
    {

        public string Encrypt(string source)
        {
            var cert = GetX509Cert("itripping.cn", "itripping.cn");
            if (cert == null) return null;
            return RsaDecrypter.Encrypt(source, cert.PublicKey.Key.ToXmlString(false));
        }
        public string Decrypt(string source)
        {
            var cert = GetX509Cert("itripping.cn", "itripping.cn");
            if (cert == null) return null;
            return RsaDecrypter.Decrypt(source, cert.PrivateKey.ToXmlString(true));
        }

        private X509Certificate2 GetX509Cert(string name, string pwd)
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);
                //轮询存储区中的所有证书  
                foreach (X509Certificate2 myX509Certificate2 in store.Certificates)
                {
                    //将证书的名称跟要导出的证书MyTestCert比较,找到要导出的证书  
                    if (myX509Certificate2.Subject == "CN=" + name)
                    {
                        return myX509Certificate2;
                    }
                }
                return null;
            }
            catch (Exception e) { return null; }
            finally
            {
                store.Close();
            }
        }

        private void test()
        {
            string str = "abc";
            Console.WriteLine(str);
            string str_enc = Encrypt(str);
            Console.WriteLine(str_enc);
            string str_dec = Decrypt(str_enc);
            Console.WriteLine(str_dec);
        }
    }
}
