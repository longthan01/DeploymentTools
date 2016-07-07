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
    }
}
