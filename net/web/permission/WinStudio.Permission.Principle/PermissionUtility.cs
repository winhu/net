using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace System.Web
{
    public static class PermissionUtility
    {
        /// <summary>
        /// Encrypter for permission
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ByPermissionEncrypter(this string source)
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;
            return source.ToMD5();
        }

        /// <summary>
        /// Encrypter for token
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ByTokenEncrypter(this string source)
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;
            return source.ToSHA1();
        }
    }
}
