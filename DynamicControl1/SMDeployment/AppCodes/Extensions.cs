using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDeployment.AppCodes
{
    public static class Extensions
    {
        /// <summary>
        /// Convert an object to enum <see cref="SMDeployment.AppCodes.Project"/>
        /// </summary>
        /// <param name="item">Object item</param>
        /// <returns>An object of <see cref="SMDeployment.AppCodes.Project"/></returns>
        public static Project ToProject(this object item)
        {
            return (Project)Enum.Parse(typeof(Project), item.ToString());
        }

        /// <summary>
        /// Convert the list of T to list of V which V is inherited from T
        /// </summary>
        /// <typeparam name="parent">Parent list object</typeparam>
        /// <typeparam name="inheritance">Inheritance list object</typeparam>
        /// <param name="list">A list of child object which is inherited from T</param>
        /// <returns></returns>
        public static List<inheritance> Convert<parent, inheritance>(this List<parent> list) where parent : new()
                                                       where inheritance : new()
        {
            List<inheritance> res = new List<inheritance>();
            var vProps = typeof(inheritance).GetProperties();
            var tProps = typeof(parent).GetProperties();
            foreach (parent tItem in list)
            {
                inheritance v = new inheritance();
                foreach (var tProp in tProps)
                {
                    var vProp = vProps.FirstOrDefault(x => x.Name == tProp.Name);
                    if (vProp != null)
                    {
                        vProp.SetValue(v, tProp.GetValue(tItem));
                    }
                }
                res.Add(v);
            }
            return res;
        }
    }
}
