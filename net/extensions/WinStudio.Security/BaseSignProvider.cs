using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Security.Cryptography;

namespace System.Security
{
    public enum AlgorithmName
    {
        md5,
        sha1
    };
    public class BaseSignProvider : ISignProvider
    {
        private ICryptProvider crypter;
        public BaseSignProvider()
        { }

        public BaseSignProvider(ICryptProvider provider)
        {
            this.crypter = provider;
        }


        public string Sign(string source)
        {
            return crypter.Encrypt(source);
        }

        public string Sign(string source, string salt)
        {
            return crypter.Encrypt(string.Format("{0}{1}{0}", salt, source));
        }

        public string Sign(NameValueCollection nv)
        {
            return Sign(nv, string.Empty);
        }

        public string Sign(NameValueCollection nv, string salt)
        {
            Array.Sort(nv.AllKeys);
            StringBuilder sb = new StringBuilder();
            foreach (string k in nv.AllKeys)
            {
                if (sb.Length > 0) sb.Append("&");
                sb.AppendFormat("{0}={1}", k, nv[k]);
            }
            //string source = nv.ToStringSorted(new String[] { "salt", "__sign" });
            return crypter.Encrypt(string.Format("{0}{1}{0}", salt, sb.ToString()));
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="sign">签名字符串</param>
        /// <returns></returns>
        public bool VerifySign(string source, string sign)
        {
            if (string.IsNullOrEmpty(sign) || string.IsNullOrEmpty(source)) return false;
            return sign.Equals(Sign(source));
        }

        public bool VerifySign(string source, string sign, string salt)
        {
            if (string.IsNullOrEmpty(sign) || string.IsNullOrEmpty(source)) return false;
            return sign.Equals(Sign(source, salt));
        }

        public bool VerifySign(NameValueCollection nv, string sign)
        {
            if (string.IsNullOrEmpty(sign)) return false;
            return sign.Equals(Sign(nv));
        }

        public bool VerifySign(NameValueCollection nv, string sign, string salt)
        {
            if (string.IsNullOrEmpty(sign)) return false;
            return sign.Equals(Sign(nv, salt));
        }

        public void Dispose()
        {
            crypter.Dispose();
        }
    }
}
