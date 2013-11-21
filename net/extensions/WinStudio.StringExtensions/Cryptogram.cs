using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class Cryptogram
    {
        /// <summary>
        /// encrypt string with Base64
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="encoding">encoding</param>
        /// <returns></returns>
        public static string ToBase64String(this string s, Encoding encoding)
        {
            if (s.HasValue())
                return Convert.ToBase64String(encoding.GetBytes(s));
            return s;
        }

        /// <summary>
        /// encrypt string with Base64
        /// </summary>
        /// <param name="s">string</param>
        /// <returns></returns>
        public static string ToBase64String(this string s)
        {
            return s.ToBase64String(Encoding.UTF8);
        }

        /// <summary>
        /// decrypt string with Base64
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="encoding">encoding</param>
        /// <returns></returns>
        public static string FromBase64String(this string s, Encoding encoding)
        {
            if (s.HasValue())
                return encoding.GetString(Convert.FromBase64String(s));
            return s;
        }

        /// <summary>
        /// decrypt string with Base64
        /// </summary>
        /// <param name="s">string</param>
        /// <returns></returns>
        public static string FromBase64String(this string s)
        {
            return s.FromBase64String(Encoding.UTF8);
        }

    }
}
