using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Security
{
    public class Crypter
    {
        /// <summary>
        /// 加密器
        /// </summary>
        /// <param name="san">加密方式</param>
        /// <returns></returns>
        public static ICryptProvider Create(AlgorithmName san)
        { return Create(san, Encoding.UTF8); }
        /// <summary>
        /// 加密器
        /// </summary>
        /// <param name="encoding">字符集</param>
        /// <param name="san">加密方式</param>
        /// <returns></returns>
        public static ICryptProvider Create(AlgorithmName san, Encoding encoding)
        {
            switch (san)
            {
                case AlgorithmName.md5:
                    return new Md5Provider(encoding);
                case AlgorithmName.sha1:
                    return new Sha1Provider(encoding);
                default:
                    return new Md5Provider(encoding);
            }
        }
    }
}
