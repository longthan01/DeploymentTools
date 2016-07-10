using Microsoft.TeamFoundation.VersionControl.Client;
using SMTools.Deployment.Base;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SMTools.Tfs.GetLastest
{
    public class TfsGetLastest : TfsTransporter
    {
        private GetLastestOutput _Output = new GetLastestOutput();
        
        public TfsGetLastest(IDeployConfigurator configurator) : base(configurator)
        {
            _Output.Failures = new List<Failure>();
        }

        #region IDeployment Members

        public override void Run()
        {
            Workspace wp = this.VersionControlServer.GetWorkspace(WorkspaceMapping);
            if (wp != null)
            {
                string dir = Path.GetDirectoryName(WorkspaceMapping);
                GetRequest request = new GetRequest(new ItemSpec(dir, RecursionType.Full), VersionSpec.Latest);
                GetStatus status = wp.Get(request, GetOptions.GetAll);
                _Output.Failures = status.GetFailures().ToList();
            }
        }

        public override DeployOutputBase GetOutput()
        {
            return this._Output;
        }
        #endregion
    }
}
