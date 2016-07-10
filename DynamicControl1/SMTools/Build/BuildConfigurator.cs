using SMTools.Build;
using SMTools.Deployment.Base;
using SMTools.Deployment.Configurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Deployment.Build
{
    public class BuildConfigurator : ConfiguratorBase
    {
        protected const string _PublishProfile = "/p:PublishProfile=";
        protected const string _PublishUrl = "publishUrl";
        protected const string _ProjectPath = "ProjectPath";
        protected const string _LogFile = "/flp:LogFile=";
        protected const string _SolutionPath = "SolutionPath";

        public override void ApplyConfig(ProcessBase process)
        {
            BuildProcess buildProcess = process as BuildProcess;
            
        }

        public override void SaveConfiguration(ProcessBase process, string file)
        {
            throw new NotImplementedException();
        }
    }
}
