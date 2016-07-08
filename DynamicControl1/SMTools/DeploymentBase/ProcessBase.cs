using SMTools.Deployment.Base;
using SMTools.DeploymentBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SMTools.Deployment.Base
{
    public abstract class ProcessBase
    {
        #region properties, fields
        /// <summary>
        ///  Extension configuration
        /// </summary>
        public IDeployConfiguration Configurator { get; set; }

        /// <summary>
        /// Collection of configuration items in xml.
        /// </summary>
        public ConfigItemCollection ConfigurationItems
        {
            get;
            set;
        }
        /// <summary>
        /// Path to configuration file.
        /// </summary>
        public string ConfigurationFile
        {
            get;
            set;
        }

        #endregion

        #region constructors
        public ProcessBase(string configFile)
        {
            this.ConfigurationFile = configFile;
            this.ConfigurationItems = XmlLoader.GetConfig(configFile);
        }
        public ProcessBase()
        {
            LoadDefaultConfiguration();
        }
        #endregion

        #region public methods
        /// <summary>
        /// Load default configuration for process
        /// </summary>
        public virtual void LoadDefaultConfiguration()
        {
            if (!string.IsNullOrEmpty(this.ConfigurationFile))
            {
                this.ConfigurationItems = XmlLoader.GetConfig(this.ConfigurationFile);
            }
        }
        /// <summary>
        /// Manually load configuration
        /// </summary>
        public virtual void ApplyExtesionConfiguration()
        {
            if (Configurator != null)
            {
                Configurator.ApplyConfig(this);
            }
        }
        /// <summary>
        /// Save configuration back to xml
        /// </summary>
        public void SaveConfiguration()
        {
            if (string.IsNullOrEmpty(this.ConfigurationFile))
            {
                throw new FileNotFoundException("The configuration file is not found.");
            }
            XmlLoader.Save(this.ConfigurationItems, this.ConfigurationFile);
        }

        #endregion

        #region protected methods
        protected ConfigItem GetConfigItem(string name)
        {
            return this.ConfigurationItems == null ? null : this.ConfigurationItems.GetConfigItem(name);
        }

        protected string GetConfigItemValue(string nodeName)
        {
            return this.ConfigurationItems == null ? null : this.ConfigurationItems.GetConfigItemValue(nodeName);
        }
        protected ConfigItem GetConfigAttribute(string nodeName, string attributeName)
        {
            return this.ConfigurationItems == null ? null : this.ConfigurationItems.GetConfigAttribute(nodeName, attributeName);
        }

        #endregion
    }
}
