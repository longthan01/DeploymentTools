using SMTools.Deployment.Base;
using SMTools.Build.Base;

namespace SMTools.Build.Deploy
{
    public class Deploymenter : BuildDeployProcess, IDeployment
    {
        public Deploymenter()
        {
            
        }

        public Deploymenter(IDeployConfigurator configurator)
            : base(configurator)
        {
           
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
