using System;
using SMTools.Deployment.Base;
using System.Xml;

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

        public ConfiguratorBase(string configFile, string configType)
        {
            this.ConfigurationFile = configFile;
            this.ConfigType = configType;
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
            throw new NotImplementedException();
        }
    }
}
