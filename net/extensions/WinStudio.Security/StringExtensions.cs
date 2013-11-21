﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStudio.Security;

namespace System
{
    public static class StringExtensions
    {
        /// <summary>
        /// 将字符串转换为MD5值
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToMD5(this string source)
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;
            return Signer.Create(AlgorithmName.md5).Sign(source);
        }
        /// <summary>
        /// 将字符串转换为SH1值
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToSHA1(this string source)
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;
            return Signer.Create(AlgorithmName.sha1).Sign(source);
        }
        /// <summary>
        /// 验证字符串是否为签名字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target">签名字符串</param>
        /// <returns></returns>
        public static bool IsEqualsMD5(this string source, string target)
        {
            return Signer.Create(AlgorithmName.md5).VerifySign(source, target);
        }
        /// <summary>
        /// 验证字符串是否为签名字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target">签名字符串</param>
        /// <returns></returns>
        public static bool IsEqualsSHA1(this string source, string target)
        {
            return Signer.Create(AlgorithmName.sha1).VerifySign(source, target);
        }
        /// <summary>
        /// 验证键值集合是否为签名键值集合
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target">签名键值集合</param>
        /// <returns></returns>
        public static bool IsEqualsMD5(this NameValueCollection source, string target)
        {
            return Signer.Create(AlgorithmName.md5).VerifySign(source, target);
        }
        /// <summary>
        /// 验证键值集合是否为签名键值集合
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target">签名键值集合</param>
        /// <returns></returns>
        public static bool IsEqualsSHA1(this NameValueCollection source, string target)
        {
            return Signer.Create(AlgorithmName.sha1).VerifySign(source, target);
        }
    }
}