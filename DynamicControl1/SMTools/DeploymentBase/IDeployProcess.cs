using SMTools.Deployment.Base;

namespace SMTools.DeploymentBase.DeploymentBase
{
    public interface IDeployProcess
    {
        void ApplyConfiguration();
        void Run();
        StepOutput GetOutput();
    }
}
