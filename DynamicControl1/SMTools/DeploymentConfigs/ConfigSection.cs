using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Utility
{
    public class ConfigSection
    {
        public string SectionName { get; set; }
        public ConfigItemCollection Items { get; set; }
        public ConfigSection()
        {
            this.Items = new ConfigItemCollection();
        }
    }
}
