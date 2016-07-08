﻿using Microsoft.TeamFoundation.VersionControl.Client;
using SMTools.Deployment.Base;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SMTools.TFSTransporter
{
    public class TFSGetLastest : TFSTransporterBase, IDeployment
    {
        private GetLastestOutput _Output = new GetLastestOutput();
        
        public TFSGetLastest(string configFile) : base(configFile)
        {
            _Output.Failures = new List<Failure>();
        }

        #region IDeployment Members

        public void Run()
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

        public StepOutput GetOutput()
        {
            return this._Output;
        }
        public void ApplyConfiguration()
        {

        }
        #endregion
    }
}
