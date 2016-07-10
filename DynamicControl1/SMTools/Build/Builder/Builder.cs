using SMTools.Deployment.Base;
using SMTools.Build.Base;

namespace SMTools.Build.Build
{
    public class Builder : BuildDeployProcess
    {
        protected const string _DeployOnBuild = "/p:DeployOnBuild=";

        public Builder(IDeployConfigurator configurator)
        {
            this.Configurator = configurator;
        }

        #region IDeployment Members
        
        public override void ApplyConfiguration()
        {
            this.Configurator.ApplyConfig(this);
        }

        public override void Run()
        {
            base.Run();
        }

        public override DeployOutputBase GetOutput()
        {
            return base.GetOutput();
        }

        #endregion
    }
}
