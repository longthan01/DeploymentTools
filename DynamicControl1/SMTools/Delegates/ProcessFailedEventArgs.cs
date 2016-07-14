using SMTools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Deployment.Base
{
    public class ProcessFailedEventArgs : EventArgs
    {
        public ProcessError Error
        {
            get;
            set;
        }
        public ProcessFailedEventArgs(ProcessError error)
        {
            this.Error = error;
        }
    }
}
