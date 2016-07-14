using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.DeploymentConfigs
{
    public interface IConfigItem
    {
        string GetName();
        string GetValue();
        void SetName(string name);
        void SetValue(string value);
        void SetAttributes(IEnumerable<IConfigItem> attributes);
        List<IConfigItem> GetAttributes();
    }
}
