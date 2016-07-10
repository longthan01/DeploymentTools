using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMTools.Deployment.Base
{
    public class ProcessCompletedEventArgs : EventArgs
    {
        public DeployOutputBase ProcessOutput
        {
            get;
            set;
        }
        public ProcessCompletedEventArgs(DeployOutputBase output)
        {
            this.ProcessOutput = output;
        }
    }
}
