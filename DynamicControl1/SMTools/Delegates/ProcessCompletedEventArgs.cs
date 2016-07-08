using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMTools.Deployment.Base
{
    public class ProcessCompletedEventArgs : EventArgs
    {
        public StepOutput ProcessOutput
        {
            get;
            set;
        }
        public ProcessCompletedEventArgs(StepOutput output)
        {
            this.ProcessOutput = output;
        }
    }
}
