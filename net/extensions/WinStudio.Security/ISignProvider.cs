using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace System.Security
{
    public interface ISignProvider : IDisposable
    {
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns></returns>
        String Sign(String s);
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="salt">盐</param>
        /// <returns></returns>
        String Sign(String s, String salt);
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="nv">源键值集合</param>
        /// <returns></returns>
        String Sign(NameValueCollection nv);
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="nv">源键值集合</param>
        /// <param name="salt">盐</param>
        /// <returns></returns>
        String Sign(NameValueCollection nv, String salt);
        /// <summary>
        /// 验证源字符串与签名字符串
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="sign">盐</param>
        /// <returns></returns>
        bool VerifySign(String s, String sign);
        /// <summary>
        /// 验证源字符串与签名字符串
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="sign">签名字符串</param>
        /// <param name="salt">盐</param>
        /// <returns></returns>
        bool VerifySign(String s, String sign, String salt);
        /// <summary>
        /// 验证源键值集合与签名键值集合
        /// </summary>
        /// <param name="nv">源键值集合</param>
        /// <param name="sign">盐</param>
        /// <returns></returns>
        bool VerifySign(NameValueCollection nv, String sign);
        /// <summary>
        /// 验证源键值集合与签名键值集合
        /// </summary>
        /// <param name="nv">源键值集合</param>
        /// <param name="sign">签名键值集合</param>
        /// <param name="salt">盐</param>
        /// <returns></returns>
        bool VerifySign(NameValueCollection nv, String sign, String salt);
    }

    public interface ICryptProvider : IDisposable
    {
        Encoding Encoding { get; }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>加密后字符串</returns>
        string Encrypt(string source);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="source">机密字符串</param>
        /// <returns>解密后字符串</returns>
        string Decrypt(string source);
    }
}
