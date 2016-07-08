using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.DeploymentBase
{
    public interface IDeployConfiguration
    {
        void ApplyConfig(ProcessBase process);
    }
}
