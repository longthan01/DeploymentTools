using SMTools.Deployment.Base;
using SMTools.Deployment.ConfigurationModels;
using SMTools.Deployment.Configurator;
using System;
using SMTools.Extensions;
using SMTools.Utility;

namespace SMTools.Build.Base
{
    public class BuildDeployConfigurator : ConfiguratorBase
    {
        protected const string _PublishProfile = "/p:PublishProfile=";
        protected const string _BuildLogFile = "/flp:LogFile=";
        protected const string _PublishUrl = "publishUrl";
        protected const string _BuildPath = "BuildPath";

        public DeploymentPath BuildPath { get; set; }

        public BuildDeployConfigurator(string configFile, string configSection) : base(configFile, configSection) { }

        public string GetDeployOutFolder()
        {
            return XmlLoader.GetValueIteration(_PublishUrl, this.ConfigItems.GetConfigItemValue(_PublishProfile).Trim('"'));
        }

        public override void ApplyConfig(ProcessBase process)
        {
            BuildDeployProcess buildProcess = process as BuildDeployProcess;
            foreach (var item in ConfigItems)
            {
                if (item.Name.SuperEquals(_BuildPath))
                {
                    if (BuildPath != null) // if user override build path
                    {
                        buildProcess.BuildCommand
                            .Append(string.Format("\"{0}\"", BuildPath.Path))
                            .Append(" ");
                    }
                    else // use default in config file
                    { 
                        buildProcess.BuildCommand
                            .Append(string.Format("\"{0}\"", item.Value))
                            .Append(" ");
                    }
                }
                else
                {
                    buildProcess.BuildCommand.Append(item.Name + item.Value);
                    buildProcess.BuildCommand.Append(" "); // seperate each parameter
                }
            }
            buildProcess.LogFile = ConfigItems.GetConfigItemValue(_BuildLogFile);
        }

        public override void SaveConfiguration(ProcessBase process)
        {
            throw new NotImplementedException();
        }
    }
}
