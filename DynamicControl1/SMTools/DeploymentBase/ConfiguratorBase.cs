using System;
using SMTools.Deployment.Base;
using System.Xml;
using SMTools.Utility;

namespace SMTools.Deployment.Configurator
{
    public abstract class ConfiguratorBase : IDeployConfigurator
    {
        public string ConfigurationFile { get; set; }
        /// <summary>
        /// Type of config, can be TfsDev, TfsQA, BuildDev, BuildQA..., matching section in config file
        /// </summary>
        public string ConfigType { get; set; }

        public ConfigItemCollection ConfigItems { get; set; }
        public ConfiguratorBase()
        {
        }
        public ConfiguratorBase(string configFile, string configSection)
        {
            this.ConfigurationFile = configFile;
            this.ConfigType = configSection;
            ConfigItems = XmlLoader.GetConfig(this.ConfigurationFile, this.ConfigType);
        }

        /// <summary>
        /// Root node in config file
        /// </summary>
        public XmlElement XmlRoot { get; set; }
        
       
        public virtual void ApplyConfig(ProcessBase process)
        {
            XmlRoot = XmlLoader.GetRoot(this.ConfigurationFile);
        }

        public virtual void SaveConfiguration(ProcessBase process)
        {
           
        }
    }
}
