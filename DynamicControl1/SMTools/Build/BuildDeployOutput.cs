using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Build.Base
{
    public class BuildDeployOutput : ProcessOutputBase
    {
        
        public string BuildOutMessage
        {
            get;
            set;
        }
        public string LogFile
        {
            get;
            set;
        }
        public BuildDeployOutput(string logFile)
        {
            this.LogFile = logFile;
        }
    }
}
