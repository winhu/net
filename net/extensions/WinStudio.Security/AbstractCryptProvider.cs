using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace WinStudio.Security
{
    public class AbstractCryptProvider : ICryptProvider
    {
        protected HashAlgorithm _provider;
        private Encoding _encoding = Encoding.UTF8;
        public Encoding Encoding { get { return _encoding; } }

        public AbstractCryptProvider(Encoding encoding)
        {
            this._encoding = encoding;
        }

        /// <summary>
        /// 此加密将哈希作为32字符的十六进制格式字符串返回
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string Encrypt(string source)
        {
            byte[] bytes = Encoding.GetBytes(source);
            bytes = _provider.ComputeHash(bytes);
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < bytes.Length; i++)
            {
                sBuilder.Append(bytes[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public string Decrypt(string source)
        {
            return null;
        }
    }
}
