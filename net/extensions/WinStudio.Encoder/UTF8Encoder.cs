using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinStudio.Encoder
{
    public static class UTF8Encoder
    {
        /// <summary>
        /// 返回字符串中所有字符的ascii值之和
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int ToAsciiSum(this string source)
        {
            if (string.IsNullOrEmpty(source)) return 0;
            return source.Sum(delegate(char c)
             {
                 return (int)c;
             });
        }

        /// <summary>
        /// 将字符串转换为十进制数字
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        //public static int ToDec(this string source)
        //{
        //    if (string.IsNullOrEmpty(source)) return 0;
        //    byte[] bytes = Encoding.UTF8.GetBytes(source);

        //    return BitConverter.ToInt32(bytes, 0);
        //}

        /// <summary>
        /// 返回字符串转换为十六进制字符串形式（与FromHexString配对使用）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToHexString(this string source)
        {
            if (string.IsNullOrEmpty(source)) return source;
            byte[] bytes = Encoding.UTF8.GetBytes(source);

            var ret = BitConverter.ToString(bytes, 0).Replace("-", "");
            return ret;
        }
        /// <summary>
        /// 将十六进制字符串转换成原始字符串（与ToHexString配对使用，不支持汉字）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string FromHexString(this string source)
        {
            if (string.IsNullOrEmpty(source)) return source;
            if (source.Length % 2 != 0) throw new ArgumentException("Unvalid source string");
            StringBuilder sb = new StringBuilder(source.Length / 2);
            int index = 0;
            while (index < source.Length)
            {
                sb.Append((char)Convert.ToInt32(source.Substring(index, 2), 16));
                index += 2;
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将数字转换为十六进制字符串形式（Big-Endian）
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ToHexBEString(this int num)
        {
            byte[] bData = BitConverter.GetBytes(num);
            Array.Reverse(bData);
            return BitConverter.ToString(bData, 0);
        }
        /// <summary>
        /// 将数字转换为十六进制字符串形式（Little-Endian）
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ToHexLEString(this int num)
        {
            return BitConverter.ToString(BitConverter.GetBytes(num), 0);
        }

    }
    class tester
    {
        public void test1()
        {

            byte[] ret = BitConverter.GetBytes(1634234);
            foreach (byte b in ret)
            {
                Console.WriteLine(b);
            }
            int r = BitConverter.ToInt16(new byte[] { 6, 0 }, 0);
            Console.WriteLine(r);
        }
        public void test()
        {
            DateTime dt = DateTime.Now;
            string str = "w哇哇";
            Console.WriteLine(str.ToHexString().FromHexString());
            Console.WriteLine((DateTime.Now - dt).Ticks);

            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(3)));

            Guid guid = Guid.NewGuid();

            Guid g1 = new Guid(guid.ToByteArray());
            Guid g2 = new Guid(guid.ToString());
            Console.WriteLine(guid);
            Console.WriteLine(g1);
            Console.WriteLine(g2);

            var m = "winhuawinhuwinahuwinhuwinhuwinhuawinhuwinhuwianhuwinhuwinhuwinahuwianhuwinhuwinahuwinhuwinhuawinhuwianhuawinhuwianhuwainhu";
            //var mn = m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m + m;
            //int a = "winhu".ToDec();
            int b = "winhu".ToAsciiSum();
            //int c = m.ToDec();

            //string f = a.ToHexBEString();
            string g = b.ToHexLEString();
            //Console.WriteLine(a);
            Console.WriteLine(b);
            //Console.WriteLine(c);
            //Console.WriteLine(f);
            Console.WriteLine(g);
            Console.WriteLine(m.ToHexString());
            Console.WriteLine(Convert.ToBase64String(Encoding.UTF8.GetBytes(m)));
        }
    }
}
