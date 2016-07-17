using SMTools.Build.Base;
using SMTools.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Build
{
    public class DeployConfigurator : BuildDeployConfigurator
    {
        public DeployConfigurator(ConfigItemCollection itemsCollection)
            : base(itemsCollection)
        {

        }
        public string GetDeployOutputFolder()
        {
            string publishPath = this.ConfigItems[ConstantString.DEPLOY_PublishProfile].Trim('"');
            if (System.IO.File.Exists(publishPath))
            {
                return XmlLoader
                    .GetValue(ConstantString.DEPLOY_PublishUrl, publishPath);
            }
            return string.Empty;
        }
        public string GetProjectPath()
        {
            return BuildPath;
        }
    }
}
