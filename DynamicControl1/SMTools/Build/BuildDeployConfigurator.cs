using SMTools.Deployment.Base;
using SMTools.Deployment.Configurator;
using System;

namespace SMTools.Build.Base
{
    public class BuildDeployConfigurator : ConfiguratorBase
    {
        protected const string _PublishProfile = "/p:PublishProfile=";
        protected const string _LogFile = "/flp:LogFile=";
        protected const string _PublishUrl = "publishUrl";
        protected const string _ProjectPath = "ProjectPath";
        protected const string _SolutionPath = "SolutionPath";

        public BuildDeployConfigurator(string configFile, string configType) : base(configFile, configType) { }

        public override void ApplyConfig(ProcessBase process)
        {
            BuildDeployProcess buildProcess = process as BuildDeployProcess;
            buildProcess.BuildCommand = new System.Text.StringBuilder();
            foreach (var item in this.ConfigItems)
            {
                buildProcess.BuildCommand.Append(item.Name + item.Value);
                buildProcess.BuildCommand.Append(" ");
            }
            buildProcess.LogFile = this.ConfigItems.GetConfigItemValue(_LogFile);
        }

        public override void SaveConfiguration(ProcessBase process)
        {
            throw new NotImplementedException();
        }
    }
}
