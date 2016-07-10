using SMTools.Deployment.Base;
using System.Linq;
using SMTools.Extensions;
using SMTools.Build;

namespace SMTools.MSBuilder
{
    public class Builder : BuildProcess, IDeployment
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
