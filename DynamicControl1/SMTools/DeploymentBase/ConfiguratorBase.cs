using System;
using SMTools.Deployment.Base;
using System.Xml;
using SMTools.Utility;
using System.Collections.Generic;
using SMTools.Deployment.Base;

namespace SMTools.Deployment.Configurator
{
    public abstract class ConfiguratorBase : IDeployConfigurator
    {
        public ConfigItemCollection ConfigItems { get; set; }
       
        public ConfiguratorBase()
        {

        }
        /// <summary>
        /// Initialize new instance using multiple config item
        /// </summary>
        /// <param name="collection">The collection of config item</param>
        public ConfiguratorBase(ConfigItemCollection collection)
        {
            this.ConfigItems = collection;
        }

        public virtual void ApplyConfig(ProcessBase process)
        {
            if (this.ConfigItems == null)
            {
                throw new ArgumentNullException("ConfigItems", "ConfigItems must be initialized first");
            }
        }

        public virtual void SaveConfiguration(ProcessBase process)
        {
            
        }
    }
}
