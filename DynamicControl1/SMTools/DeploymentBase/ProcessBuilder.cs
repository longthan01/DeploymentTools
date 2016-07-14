using SMTools.Deployment.Base;
using System;
using System.Threading.Tasks;

namespace SMTools.Deployment.Base
{
    /// <summary>
    /// The class Deployment process builder, to handle starting and stopping process
    /// </summary>
    public class ProcessBuilder
    {
        /// <summary>
        /// Raise when begining process.
        /// </summary>
        public event EventHandler OnProcessBegining;
        /// <summary>
        /// Raise when process is completed.
        /// </summary>
        public event ProcessCompletedEventHandler OnProcessCompleted;
        /// <summary>
        /// Raise when process failed.
        /// </summary>
        public event ProcessFailedEventHandler OnProcessFailed;
        public IDeployProcess DeploymentProcess
        {
            get;
            set;
        }
        public ProcessBuilder()
        {
        }
        public ProcessBuilder(IDeployProcess process)
        {
            this.DeploymentProcess = process;
        }
        public ProcessBuilder SetProcess(IDeployProcess process)
        {
            this.DeploymentProcess = process;
            return this;
        }
        public void Start()
        {
            try
            {
                if (OnProcessBegining != null)
                {
                    OnProcessBegining(this, null);
                }
                this.DeploymentProcess.ApplyConfiguration();
                this.DeploymentProcess.Run();
                if (OnProcessCompleted != null)
                {
                    OnProcessCompleted(this, new ProcessCompletedEventArgs(this.DeploymentProcess.GetOutput()));
                }
            }
            catch (Exception ex)
            {
                if (OnProcessFailed != null)
                {
                    OnProcessFailed(this, new ProcessFailedEventArgs(new Models.ProcessError()
                    {
                        ErrorMessage = ex.Message
                    }));
                }
            }
        }

        public async Task StartAsync()
        {
           await Task.Run(delegate { this.Start(); });
        }
    }
}
