using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools
{
    public static class SMExtensions
    {
        public static bool SuperEquals(this string src, string dest)
        {
            return src.Equals(dest, StringComparison.OrdinalIgnoreCase);
        }
    }
}
