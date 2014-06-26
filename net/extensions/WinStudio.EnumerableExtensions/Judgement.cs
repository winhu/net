using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    public static class Judgement
    {
        /// <summary>
        /// is not null and length > 0
        /// </summary>
        /// <param name="arr">array</param>
        /// <returns></returns>
        public static bool HasValue<T>(this IEnumerable<T> arr)
        {
            return arr != null && arr.Count() > 0;
            //if (arr is Array)
            //{
            //    return (arr as Array).Length > 0;
            //}
            //else if (arr is List<T>)
            //{ return (arr as List<T>).Count > 0; }
            //return false;
        }
    }
}
