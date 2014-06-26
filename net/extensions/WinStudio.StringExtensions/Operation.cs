using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class Operation
    {
        /// <summary>
        /// length in char(2 chars for one chinese)
        /// </summary>
        /// <param name="s">string</param>
        /// <returns></returns>
        public static int LengthInChar(this string s)
        {
            int _length = s.Length;
            foreach (char c in s)
            {
                if (c.IsChinese())
                    _length++;
            }
            return _length;
        }
        /// <summary>
        /// remove special chars from string
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="chars">special chars：~!@#$%^&()+{}`[];',.()</param>
        /// <returns></returns>
        public static string RemoveSpecialChars(this string s, string chars = "~!@#$%^&()+{}`[];',.()")
        {
            if (!s.HasValue()) return s;
            StringBuilder sb = new StringBuilder();
            foreach (char c in s)
            {
                if (!chars.Contains(c)) sb.Append(c);
            }
            return sb.ToString();
        }

        /// <summary>
        /// cut string for count
        /// </summary>
        /// <param name="str">string</param>
        /// <param name="count">cut index</param>
        /// <param name="other">other to show</param>
        /// <returns></returns>
        public static string Cut(this string str, int count = 10, string other = "…")
        {
            if (!str.HasValue()) return string.Empty;
            if (str.LengthInChar() <= count) return str;
            int length = 0;
            foreach (char c in str)
            {
                if (c.IsChinese()) count--;
                length++;
                if (length >= count) break;
            }
            return str.Substring(0, length) + other;
        }
    }
}
