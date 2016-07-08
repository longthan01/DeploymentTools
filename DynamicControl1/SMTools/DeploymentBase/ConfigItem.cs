using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMTools.Deployment.Base
{
    public class ConfigItem
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
    }
}
