using SMTools.Build.Base;
using SMTools.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Build
{
    public class BuildConfigurator : BuildDeployConfigurator
    {
        public BuildConfigurator(ConfigItemCollection itemsCollection)
            : base(itemsCollection)
        {

        }
        public string GetSolutionPath()
        {
            return BuildPath;
        }
    }
}
