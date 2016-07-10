using SMTools.Deployment.Base;
using SMTools.Build.Base;

namespace SMTools.Build.Build
{
    public class BuildConfigurator : BuildDeployConfigurator
    {
        public BuildConfigurator(string configFile, string configType) : base(configFile, configType) { }
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
            Builder builder = process as Builder;
            var itemValue = this.ConfigItems.GetConfigItemValue(_ProjectPath);
            builder.BuildCommand = builder.BuildCommand
                .Replace(_SolutionPath, string.Empty)
                .Replace(_ProjectPath, string.Empty) // remove string ProjectPath in build command
                .Replace(itemValue, string.Empty); // remove ProjectPath string value in build command
        }
        public override void SaveConfiguration(ProcessBase process)
        {
            base.SaveConfiguration(process);
        }
    }
}
