using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneShard_Mono.Extensions
{
    public static class StringExt
    {
        public static List<string> SplitWithCount(this string str, int count)
        {
            var ret = new List<string>();
            int i = 0;
            while (i < str.Length)
            {
                var sub = str.Substring(i, Math.Min(count, str.Length - i));

                ret.Add(sub);

                i += count;
            }

            return ret;
        }
    }
}
