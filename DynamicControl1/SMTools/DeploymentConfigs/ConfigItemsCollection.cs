using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMTools.Extensions;
using SMTools.DeploymentConfigs;

namespace SMTools.Utility
{
    public class ConfigItemCollection : List<IConfigItem>
    {
        /// <summary>
        /// Get or set item value by item name
        /// </summary>
        /// <param name="itemName">Item's name</param>
        /// <returns>Item's value</returns>
        public string this[string itemName]
        {
            get
            {
                return this.GetConfigItemValue(itemName);
            }
            set
            {
                this.SetItemValue(itemName, value);
            }
        }
        public IConfigItem GetConfigItem(string itemName)
        {
            return this.FirstOrDefault(x => x.GetName().SuperEquals(itemName));
        }
        public IConfigItem GetConfigAttribute(string parent, string attributeName)
        {
            IConfigItem item = GetConfigItem(parent);
            IConfigItem res = null;
            if (item != null)
            {
                res = item.GetAttributes().FirstOrDefault(x => x.GetName().SuperEquals(attributeName));
            }
            return res;
        }
        
        private void SetItemValue(string itemName, string value)
        {
            IConfigItem item = GetConfigItem(itemName);
            if (item != null)
            {
                item.SetValue(value);
            }
        }
        private string GetConfigItemValue(string itemName)
        {
            var item = this.GetConfigItem(itemName);
            return item != null ? item.GetValue() : string.Empty;
        }
    }
}
