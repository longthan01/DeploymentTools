﻿using SMTools.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SMTools.DeploymentBase
{
    public abstract class DeploymentProcessBase
    {
        #region properties, fields

        /// <summary>
        /// Collection of configuration items in file xml.
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

        public DeploymentProcessBase(string configFile)
        {
            this.ConfigurationFile = configFile;
            this.ConfigurationItems = XmlLoader.GetConfig(configFile);
        }

        #endregion

        #region public methods

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

        protected string GetConfigItemValue(string itemName)
        {
            return this.ConfigurationItems == null ? null : this.ConfigurationItems.GetConfigItemValue(itemName);
        }
        protected ConfigItem GetConfigAttribute(string nodeName, string attributeName)
        {
            return this.ConfigurationItems == null ? null : this.ConfigurationItems.GetConfigAttribute(nodeName, attributeName);
        }

        #endregion

    }
}