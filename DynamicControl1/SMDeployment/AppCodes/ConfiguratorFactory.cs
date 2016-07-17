using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTools.Extensions;
using SMTools.Utility;
using SMTools.TFSTransporter;
using SMTools.FileCopier;
using SMTools.Build.Base;
using SMTools.Build;
namespace SMDeployment.AppCodes
{
    public static class ConfiguratorFactory
    {
        
        private static List<IDeployConfigurator> _Cache
        {
            get;
            set;
        }

        private static void AddCache(IDeployConfigurator configurator)
        {
            if (_Cache == null)
            {
                _Cache = new List<IDeployConfigurator>();
                _Cache.Add(configurator);
            }
            else
            {
                var item = _Cache.FirstOrDefault(x => x.GetType() == configurator.GetType());
                if (item == null)
                {
                    _Cache.Add(configurator);
                }
            }
        }

        private static bool IsOrInheritFrom(Type src, Type dest)
        {
            if (src == null)
            {
                return false;
            }
            if (src == dest)
            {
                return true;
            }
            else
            {
                return IsOrInheritFrom(src.BaseType, dest);
            }
        }

        public static T GetConfigurator<T>()
            where T : IDeployConfigurator
        {
            Type tType = typeof(T);
            IDeployConfigurator res = null;
            if (IsOrInheritFrom(tType, typeof(TfsConfigurator)))
            {
                res = GetConfigurator<T>((XmlConfigSection)Enum.Parse(typeof(XmlConfigSection), SessionManager.WorkingTfs));
            }
            if (IsOrInheritFrom(tType, typeof(DeployConfigurator)))
            {
                res = GetConfigurator<T>(XmlConfigSection.Deploy);
            }
            if (IsOrInheritFrom(tType, typeof(BuildConfigurator)))
            {
                res = GetConfigurator<T>(XmlConfigSection.Build);
            }
            return (T)res;
        }

        public static T GetConfigurator<T>(XmlConfigSection section) where T : IDeployConfigurator
        {
            IDeployConfigurator res = null;
            var deployItems = SessionManager.XmlLoader.GetConfig(section.ToString());
            if (deployItems != null)
            {
                res = (T)Activator.CreateInstance(typeof(T), deployItems);
            }
            return (T)res;
        }
    }
}
