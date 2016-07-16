using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Deployment.Base
{
    public interface IDeployConfigurator : IExceptionHandled
    {
        void ApplyConfig(ProcessBase process);
        void SaveConfiguration(ProcessBase process);
    }
}
