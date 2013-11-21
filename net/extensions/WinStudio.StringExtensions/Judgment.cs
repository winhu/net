using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System
{
    public static class Judgment
    {
        /// <summary>
        /// is not null and empty
        /// </summary>
        /// <param name="data">string</param>
        /// <returns></returns>
        public static bool HasValue(this string data)
        {
            return !string.IsNullOrEmpty(data);
        }

        //=================================================================================================
        //ip
        private static string patterns_ip = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])(\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])){3}$";

        //email
        private static string patterns_email = @"^[\.\w-]+(\.[\.\w-]+)*@[\w-]+(\.[\w-]+)+$";

        //date
        private static string patterns_date = @"^\d{4}-(0?[1-9]|1[0-2])-(0?[1-9]|[1-2]\d|3[0-1])$";

        // time
        private static string patterns_time = @"^([0-1]\\d|2[0-3]):[0-5]\\d:[0-5]\\d$";

        private static string patterns_url = @"\b(([\w-]+://?|www[.])[^\s()<>]+(?:\([\w\d]+\)|([^[:punct:]\s]|/)))";
        private static string patterns_useraccount = @"^[a-zA-Z]\w{5,17}$";
        private static string patterns_userpassowrd = @"(?-i)(?=^.{8,}$)((?!.*\s)(?=.*[A-Z])(?=.*[a-z]))(?=(1)(?=.*\d)|.*[^A-Za-z0-9])^.*$";
        private static Regex reg;
        /// <summary>
        /// is email string
        /// </summary>
        /// <param name="s">string</param>
        /// <returns></returns>
        public static bool IsEmail(this string s)
        {
            reg = new Regex(patterns_email);
            return reg.IsMatch(s);
        }
        /// <summary>
        /// is ip string
        /// </summary>
        /// <param name="s">string</param>
        /// <returns></returns>
        public static bool IsIp(this string s)
        {
            reg = new Regex(patterns_ip);
            return reg.IsMatch(s);
        }
        /// <summary>
        /// is date string
        /// </summary>
        /// <param name="s">string</param>
        /// <returns></returns>
        public static bool IsDate(this string s)
        {
            reg = new Regex(patterns_date);
            return reg.IsMatch(s);
        }
        /// <summary>
        /// is time string
        /// </summary>
        /// <param name="s">string</param>
        /// <returns></returns>
        public static bool IsTime(this string s)
        {
            reg = new Regex(patterns_time);
            return reg.IsMatch(s);
        }
        /// <summary>
        /// is match with regex
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="regex">regex</param>
        /// <returns></returns>
        public static bool IsMatch(this string s, string regex)
        {
            reg = new Regex(regex);
            return reg.IsMatch(s);
        }
        /// <summary>
        /// is url
        /// </summary>
        /// <param name="s">string</param>
        /// <returns></returns>
        public static bool IsUrl(this string s)
        {
            reg = new Regex(patterns_url);
            return reg.IsMatch(s);
        }
        /// <summary>
        /// is account format(以字母开头，长度在6~18之间，只能包含字符、数字和下划线)
        /// </summary>
        /// <param name="s">string</param>
        /// <returns></returns>
        public static bool IsAccountFormat(this string s)
        {
            reg = new Regex(patterns_useraccount);
            return reg.IsMatch(s);
        }
        /// <summary>
        /// is password format(必须包含至少一个数字，一个大写字母)
        /// </summary>
        /// <param name="s">string</param>
        /// <returns></returns>
        public static bool IsPasswordFormat(this string s)
        {
            reg = new Regex(patterns_userpassowrd);
            return reg.IsMatch(s);
        }

        private static Regex _regex = new Regex("[^x00-xff]", RegexOptions.Compiled);
        public static bool HasChinese(this string s)
        {
            return _regex.IsMatch(s);
        }
        public static bool IsChinese(this char c)
        {
            return _regex.IsMatch(c.ToString());
        }

    }
}
