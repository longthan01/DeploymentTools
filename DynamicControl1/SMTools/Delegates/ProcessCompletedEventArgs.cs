using SMTools.DeploymentBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMTools
{
    public class ProcessCompletedEventArgs : EventArgs
    {
        public StepOutputBase ProcessOutput
        {
            get;
            set;
        }
        public ProcessCompletedEventArgs(StepOutputBase output)
        {
            this.ProcessOutput = output;
        }
    }
}
