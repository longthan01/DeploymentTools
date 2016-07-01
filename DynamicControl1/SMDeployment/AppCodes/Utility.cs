using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDeployment.AppCodes
{
    public static class Utility
    {
        public static ConfigKey ToConfigKey(this string src)
        {
            ConfigKey key = (ConfigKey)Enum.Parse(typeof(ConfigKey), src);
            return key;
        }
        public static ConfigKey ToConfigKey(this object obj)
        {
            return ToConfigKey(obj.ToString());
        }
    }
}
