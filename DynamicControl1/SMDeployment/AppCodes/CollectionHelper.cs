using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDeployment.AppCodes
{
    public static class CollectionHelper
    {
        public static List<object> GetList(params object[] items)
        {
            return new List<object>(items);
        }

        public static List<string> GetEnumList(Type type)
        {
            List<string> res = new List<string>();
            var arr = Enum.GetValues(type);
            foreach (var item in arr)
            {
                res.Add(item.ToString());
            }
            return res;
        }
    }
}
