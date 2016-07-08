using Microsoft.TeamFoundation.VersionControl.Client;
using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.TFSTransporter
{
    public class GetLastestOutput : StepOutput
    {
        public List<Failure> Failures
        {
            get;
            set;
        }
        public GetLastestOutput()
        {
            Failures = new List<Failure>();
        }
    }
}
