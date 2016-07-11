using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Extensions
{
    public static class SMExtensions
    {
        /// <summary>
        /// Check if source string is equal to dest string, ignore white space in start and end string
        /// </summary>
        /// <param name="src">Source string</param>
        /// <param name="dest">Destination string</param>
        /// <returns></returns>
        public static bool SuperEquals(this string src, string dest)
        {
            return src.Equals(dest, StringComparison.OrdinalIgnoreCase);
        }
    }
}
