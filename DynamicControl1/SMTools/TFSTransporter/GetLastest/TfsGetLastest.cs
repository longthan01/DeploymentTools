using Microsoft.TeamFoundation.VersionControl.Client;
using SMTools.Deployment.Base;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SMTools.Tfs.GetLastest
{
    public class TfsGetLastest : TfsTransporter
    {
        public TfsGetLastest(IDeployConfigurator configurator)
            : base(configurator)
        {
           
        }

        #region IDeployment Members

        public override void Run()
        {
            GetLastestOutput _Output = new GetLastestOutput();
            string dir = Path.GetDirectoryName(TfsInfor.WorkspaceMapping);
            GetRequest request = new GetRequest(new ItemSpec(dir, RecursionType.Full), VersionSpec.Latest);
            GetStatus status = CurrentWorkspace.Get(request, GetOptions.GetAll);
            _Output.Failures = status.GetFailures().ToList();
            ProcessOutput = _Output;
        }
        #endregion
    }
}
