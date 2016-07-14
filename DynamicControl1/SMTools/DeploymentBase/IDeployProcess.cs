using SMTools.Deployment.Base;

namespace SMTools.Deployment.Base
{
    public interface IDeployProcess
    {
        void ApplyConfiguration();
        void Run();
        ProcessOutputBase GetOutput();
    }
}
