using Microsoft.TeamFoundation.VersionControl.Client;
using SMTools.Deployment.Base;
using System.Collections.Generic;

namespace SMTools.Tfs.GetLastest
{
    public class GetLastestOutput : ProcessOutputBase
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
