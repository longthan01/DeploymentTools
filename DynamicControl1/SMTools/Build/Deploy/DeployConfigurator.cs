using SMTools.Build.Base;
using SMTools.Build.Deployment;
using SMTools.Deployment.Base;

namespace SMTools.DeploymentBase.Build.Deploy
{
    public class DeployConfigurator : BuildDeployConfigurator
    {
        public DeployConfigurator(string configFile, string configType) : base(configFile, configType) { }
        public string GetProjectPath()
        {
            return this.ConfigItems.GetConfigItemValue(_ProjectPath);
        }
        public string GetSolutionPath()
        {
            return this.ConfigItems.GetConfigItemValue(_SolutionPath);
        }
        public override void ApplyConfig(ProcessBase process)
        {
            base.ApplyConfig(process);
            Deploymenter builder = process as Deploymenter;
            var itemValue = this.ConfigItems.GetConfigItemValue(_SolutionPath);
            builder.BuildCommand = builder.BuildCommand
                .Replace(_ProjectPath, string.Empty)// remove string ProjectPath in build command
                .Replace(_SolutionPath, string.Empty) // remove string SolutionPath in build command
                .Replace(itemValue, string.Empty); // remove SolutionPath string value in build command
        }
        public override void SaveConfiguration(ProcessBase process)
        {
            base.SaveConfiguration(process);
        }
    }
}
