using System;
using System.Security.Cryptography;
using System.Text;

namespace System
{
    public static class NeedExtensions
    {
        /// <summary>
        /// convert string to hash code
        /// </summary>
        /// <param name="encoding">encoding like gbk,utf-8...</param>
        /// <param name="CryptogramType">cryptogram type</param>
        internal static byte[] ToHashCode(this string s, string type = "MD5")
        {
            byte[] data2sign = Encoding.UTF8.GetBytes(s);
            HashAlgorithm hash = HashAlgorithm.Create(type);
            return hash.ComputeHash(data2sign);
        }
        internal static string ToHexString(this byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }

        /// <summary>
        /// convert string to md5
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="encoding">encoding</param>
        /// <returns></returns>
        internal static string ToMD5(this string s)
        {
            return s.ToHashCode().ToHexString();
        }

        /// <summary>
        /// convert string to sha1
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="encoding">encoding</param>
        /// <returns></returns>
        internal static string ToSHA1(this string s)
        {
            return s.ToHashCode("SHA1").ToHexString();
        }
    }
}
