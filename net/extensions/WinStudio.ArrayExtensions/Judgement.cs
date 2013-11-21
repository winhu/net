using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class Judgement
    {
        /// <summary>
        /// is not null and length > 0
        /// </summary>
        /// <param name="arr">array</param>
        /// <returns></returns>
        public static bool HasValue(this Array arr)
        {
            return arr != null && arr.Length != 0;
        }

        /// <summary>
        /// compare with target(same length, everyone type and value)
        /// </summary>
        /// <param name="arr">array</param>
        /// <param name="target">target</param>
        /// <returns></returns>
        public static bool SameWith(this Array arr, Array target)
        {
            if (!arr.HasValue() && !target.HasValue()) return true;
            if (arr.HasValue() && target.HasValue())
            {
                if (arr.Length != target.Length) return false;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr.GetValue(i).GetType() != target.GetValue(i).GetType()) return false;
                    if (!arr.GetValue(i).Equals(target.GetValue(i))) return false;
                }
                return true;
            }
            return false;
        }
    }
}
