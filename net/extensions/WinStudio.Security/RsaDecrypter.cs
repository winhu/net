using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Reflection;

namespace WinStudio.Security
{
    public class RsaDecrypter
    {
        /// <summary>
        /// 产生公钥和私钥：Array[0]私钥，Array[1]公钥
        /// </summary>
        /// <returns></returns>
        public static string[] GenerateKeys()
        {
            string[] sKeys = new String[2];
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            sKeys[0] = rsa.ToXmlString(true);
            sKeys[1] = rsa.ToXmlString(false);
            return sKeys;
        }

        /// <summary>
        /// RSA 加密（RSA -> Base64）
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>加密结果（经Base64编码后的串）</returns>
        public static string Encrypt(string source, string key)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(key);
                byte[] ret = rsa.Encrypt(Encoding.UTF8.GetBytes(source), false);
                return Convert.ToBase64String(ret);
            }
            catch (Exception e) { return null; }
        }
        /// <summary>
        /// RSA 加密（RSA -> Base64）
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key" >公钥</param>
        /// <returns>加密结果（经Base64编码后的串）</returns>
        public static string Encrypt(string source)
        { return Encrypt(source, _publicKeyValue); }

        /// <summary>
        /// RSA 解密（Base64 -> RSA）
        /// </summary>
        /// <param name="sSource">源字符串（经Base64编码后的串）</param>
        /// <param name="key">私钥</param>
        /// <returns>解密结果</returns>
        public static string Decrypt(string source, string key)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(key);
                byte[] ret = rsa.Decrypt(Convert.FromBase64String(source), false);
                return Encoding.UTF8.GetString(ret);
            }
            catch (Exception e) { return null; }
        }
        /// <summary>
        /// RSA 加密（RSA -> Base64）
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>加密结果（经Base64编码后的串）</returns>
        public static string Decrypt(string source)
        { return Decrypt(source, _privateKeyValue); }

        /// <summary>
        /// 读取密钥文件，并默认为私钥
        /// </summary>
        /// <param name="xmlPath">密钥文件（xml格式）全路径</param>
        /// <returns>密钥字符串</returns>
        public static string ReadKey()
        {
            return ReadKey(true);
        }

        private static string _privateKeyValue = string.Empty;
        private static string _publicKeyValue = string.Empty;
        /// <summary>
        /// 读取密钥文件
        /// </summary>
        /// <param name="xmlPath">密钥文件（xml格式）全路径</param>
        /// <param name="IsPrivateKey">是否为私钥</param>
        /// <returns>密钥字符串</returns>
        public static string ReadKey(bool IsPrivateKey)
        {
            string path = IsPrivateKey ? PrivateKeyFile : PublicKeyFile;
            if (!File.Exists(path))
                return string.Empty;
            string[] lines = File.ReadAllLines(path);
            StringBuilder sb = new StringBuilder();
            foreach (string line in lines)
            { sb.Append(line.Trim()); }
            //if (IsPrivateKey)
            //    _privateKeyValue = sb.ToString();
            //else _publicKeyValue = sb.ToString();
            return IsPrivateKey ? _privateKeyValue = sb.ToString() : _publicKeyValue = sb.ToString();
        }

        /// <summary>
        /// 私钥文件
        /// </summary>
        public static string PrivateKeyFile
        {
            get
            {
                return Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "RsaKeys/RsaPrivateKeyValue.xml");
            }
        }

        /// <summary>
        /// 公钥文件
        /// </summary>
        public static string PublicKeyFile
        {
            get
            {
                return Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "RsaKeys/RsaPublicKeyValue.xml");
            }
        }

        /// <summary>
        /// 在当前目录下生成一对密钥（公钥+私钥）文件（xml格式）
        /// </summary>
        public static void WriteKeys()
        {
            string[] keys = GenerateKeys();
            DirectoryInfo dir = new DirectoryInfo(PrivateKeyFile);
            if (!dir.Parent.Exists)
                dir.Parent.Create();
            using (var sw = new StreamWriter(PrivateKeyFile))
            {
                sw.Write(keys[0]);
                sw.Flush();
                sw.Close();
            }
            using (var sw = new StreamWriter(PublicKeyFile))
            {
                sw.Write(keys[1]);
                sw.Flush();
                sw.Close();
            }
        }
    }
}
