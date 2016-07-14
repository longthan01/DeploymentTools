using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMTools.Deployment.Base
{
    public class ProcessCompletedEventArgs : EventArgs
    {
        public ProcessOutputBase ProcessOutput
        {
            get;
            set;
        }
        public ProcessCompletedEventArgs(ProcessOutputBase output)
        {
            this.ProcessOutput = output;
        }
    }
}
