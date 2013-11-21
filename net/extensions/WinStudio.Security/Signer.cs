using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Security
{
    public class Signer
    {
        /// <summary>
        /// 创建签名器
        /// </summary>
        /// <param name="provider">加密器</param>
        /// <returns></returns>
        public static ISignProvider Create(ICryptProvider provider)
        {
            return new AbstractSignProvider(provider);
        }
        /// <summary>
        /// 创建签名器（默认字符集UTF8）
        /// </summary>
        /// <param name="san">签名方式</param>
        /// <returns></returns>
        public static ISignProvider Create(AlgorithmName san)
        {
            return Create(san, Encoding.UTF8);
        }
        /// <summary>
        /// 创建签名器
        /// </summary>
        /// <param name="encoding">字符集</param>
        /// <param name="san">签名方式</param>
        /// <returns></returns>
        public static ISignProvider Create(AlgorithmName san, Encoding encoding)
        {
            switch (san)
            {
                case AlgorithmName.md5:
                    return new AbstractSignProvider(new Md5Provider(encoding));
                case AlgorithmName.sha1:
                    return new AbstractSignProvider(new Sha1Provider(encoding));
                default:
                    return new AbstractSignProvider(new Md5Provider(encoding));
            }
        }
    }
}
