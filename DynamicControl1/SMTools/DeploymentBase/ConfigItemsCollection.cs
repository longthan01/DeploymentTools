using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMTools.DeploymentBase
{
    public class ConfigItemCollection : List<ConfigItem>
    {
        public ConfigItem GetConfigItem(string itemName)
        {
            return this.FirstOrDefault(x => x.Name.SuperEquals(itemName));
        }
        public string GetConfigItemValue(string itemName)
        {
            var item = this.GetConfigItem(itemName);
            return item != null ? item.Value : string.Empty;
        }
        public ConfigItem GetConfigAttribute(string nodeName, string attributeName)
        {
            ConfigItem item = GetConfigItem(nodeName);
            ConfigItem res = null;
            if (item != null)
            {
                res = item.Attributes.FirstOrDefault(x => x.Name.SuperEquals(attributeName));
            }
            return res;
        }
    }
}
