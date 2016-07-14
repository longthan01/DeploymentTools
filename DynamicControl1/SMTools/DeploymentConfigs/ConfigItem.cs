using SMTools.DeploymentConfigs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMTools.Utility
{
    public class ConfigItem : IConfigItem
    {
        public string Name
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }
        public List<ConfigItem> Attributes
        {
            get;
            set;
        }
        public ConfigItem()
        {
            this.Attributes = new List<ConfigItem>();
        }

        #region IConfigItem Members

        public string GetName()
        {
            return this.Name;
        }

        public string GetValue()
        {
            return this.Value;
        }

        #endregion

        #region IConfigItem Members


        public void SetName(string name)
        {
            this.Name = name;
        }

        public void SetValue(string value)
        {
            this.Value = value;
        }


        public void SetAttributes(IEnumerable<IConfigItem> attributes)
        {
            this.Attributes = attributes.Cast<ConfigItem>().ToList();
        }

        List<IConfigItem> IConfigItem.GetAttributes()
        {
            return this.Attributes.Cast<IConfigItem>().ToList();
        }

        #endregion
    }
}
