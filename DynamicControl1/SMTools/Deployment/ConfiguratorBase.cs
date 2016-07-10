using System;
using SMTools.Deployment.Base;
using System.Xml;

namespace SMTools.Deployment.Configurator
{
    public abstract class ConfiguratorBase : IDeployConfigurator
    {
        public string ConfigurationFile { get; set; }
        public ConfigItemCollection ConfigItems { get; set; }

        public ConfiguratorBase() {  }

        public ConfiguratorBase(string configFile)
        {
            this.ConfigurationFile = configFile;
        }

        /// <summary>
        /// Root node in config file
        /// </summary>
        public XmlElement XmlRoot { get; set; }
        
       
        public virtual void ApplyConfig(ProcessBase process)
        {
            XmlRoot = XmlLoader.GetRoot(this.ConfigurationFile);
            ConfigItems = XmlLoader.GetConfig(this.ConfigurationFile, process.GetProcessName());
        }

        public virtual void SaveConfiguration(ProcessBase process)
        {
            throw new NotImplementedException();
        }
    }
}
