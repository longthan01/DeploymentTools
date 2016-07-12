using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTools.Extensions;
namespace SMDeployment.AppCodes
{
    public class ConfiguratorSetting
    {
        public IDeployConfigurator Configurator
        {
            get;
            set;
        }
        public Section Section
        {
            get;
            set;
        }
        public Project Project
        {
            get;
            set;
        }
    }
    public static class ConfiguratorFactory
    {
        private static List<ConfiguratorSetting> _Cache
        {
            get;
            set;
        }
        static ConfiguratorFactory()
        {
            _Cache = new List<ConfiguratorSetting>();
        }
        public static T GetConfigurator<T>(string section, string project = "") where T : IDeployConfigurator
        {
            var cacheItem = _Cache.FirstOrDefault(x => x.Section.ToString().SuperEquals(section));
            IDeployConfigurator res = null;
            if (cacheItem != null)
            {
                res = cacheItem.Configurator;
            }
            else
            {
                res = (T)Activator.CreateInstance(typeof(T), "ProcessConfig.xml", section + project);
                _Cache.Add(new ConfiguratorSetting()
                {
                    Configurator = res,
                    Section = (Section)Enum.Parse(typeof(Section), section),
                    Project = (Project)Enum.Parse(typeof(Project), project)
                });
            }
            return (T)res;
        }
        public static T GetConfigurator<T>(Section section, Project project) where T : IDeployConfigurator
        {
            return GetConfigurator<T>(section.ToString(), project.ToString());
        }
        public static T GetConfigurator<T>(Section section) where T : IDeployConfigurator
        {
            return GetConfigurator<T>(section.ToString());
        }
        private static IDeployConfigurator AddCache(IDeployConfigurator config, Section section, Project project)
        {
            if (!_Cache.Any(x => x.Configurator == config))
            {
                _Cache.Add(new ConfiguratorSetting()
                {
                    Configurator = config,
                    Project = project,
                    Section = section
                });
            }
            return config;
        }
    }
}
