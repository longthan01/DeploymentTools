using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Deployment.Base
{
    public interface IDeployment
    {
        void ApplyConfiguration();
        void Run();
        DeployOutputBase GetOutput();
    }
}
