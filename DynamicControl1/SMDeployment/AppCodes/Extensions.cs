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
        /// Convert an object to enum <see cref="SMDeployment.AppCodes.ProjectPath"/>
        /// </summary>
        /// <param name="item">Object item</param>
        /// <returns>An object of <see cref="SMDeployment.AppCodes.ProjectPath"/></returns>
        public static ProjectPath ToProject(this object item)
        {
            return (ProjectPath)Enum.Parse(typeof(ProjectPath), item.ToString());
        }
        public static XmlConfigSection ToXmlConfigSection(this object item)
        {
            return (XmlConfigSection)Enum.Parse(typeof(XmlConfigSection), item.ToString());
        }
        /// <summary>
        /// Convert the list of T to list of V which V is inherited from T
        /// </summary>
        /// <typeparam name="parent">Parent list object</typeparam>
        /// <typeparam name="inheritance">Inheritance list object</typeparam>
        /// <param name="list">A list of child object which is inherited from T</param>
        /// <returns></returns>
        public static List<inheritance> Convert<parent, inheritance>(this List<parent> list)
            where parent : new()
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
        /// <summary>
        /// Add "_Solution" in returned string
        /// </summary>
        /// <param name="src">Source string object</param>
        /// <returns>A string following as format: source_Solution</returns>
        public static string ToStringWithSolution(this object src)
        {
            return src.ToString() + "_Solution";
        }
        /// <summary>
        /// Add "_Project" in returned string
        /// </summary>
        /// <param name="src">Source string object</param>
        /// <returns>A string following as format: source_Project</returns>
        public static string ToStringWithProject(this object src)
        {
            return src.ToString() + "_Project";
        }
        /// <summary>
        /// Check if an object string is empty
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static bool IsEmpty(this object src)
        {
            return string.IsNullOrEmpty(src.ToString());
        }
    }
}
