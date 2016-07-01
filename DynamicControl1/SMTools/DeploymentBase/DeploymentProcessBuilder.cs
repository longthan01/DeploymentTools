using SMTools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.DeploymentBase
{
    public class DeploymentProcessBuilder
    {
        /// <summary>
        /// Raise when begining process.
        /// </summary>
        public event EventHandler OnProcessBegining;
        /// <summary>
        /// Raise when process is completed.
        /// </summary>
        public event ProcessCompletedEventHandler OnProcessCompleted;

        public IDeployment DeploymentProcess
        {
            get;
            set;
        }
        public DeploymentProcessBuilder(IDeployment process)
        {
            this.DeploymentProcess = process;
        }
        public void Start()
        {
            this.DeploymentProcess.ApplyConfiguration();
            if (OnProcessBegining != null)
            {
                OnProcessBegining(this, null);
            }
            this.DeploymentProcess.Run();
            if (OnProcessCompleted != null)
            {
                OnProcessCompleted(this, new ProcessCompletedEventArgs(this.DeploymentProcess.GetOutput()));
            }
        }

        public async Task StartAsync()
        {
            Action a = new Action(this.Start);
            await Task.Run(a);
        }
    }
}
