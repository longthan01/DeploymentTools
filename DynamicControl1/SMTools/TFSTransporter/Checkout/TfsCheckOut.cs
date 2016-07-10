using Microsoft.TeamFoundation.VersionControl.Client;
using SMTools.Deployment.Utility;
using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTools.Extensions;
using SMTools.DeploymentBase.TFSTransporter;

namespace SMTools.Tfs.Checkout
{
    public class TfsCheckOut : TfsTransporter
    {
        private CheckOutOutput _Output = new CheckOutOutput();
        public string SourceFolder
        {
            get;
            set;
        }
        #region constructors
        public TfsCheckOut()
            : base()
        {
            this.Configurator = new TfsConfigurator();
        }
        public TfsCheckOut(IDeployConfigurator configurator, string sourceFolder)
            : base(configurator)
        {
            this.SourceFolder = sourceFolder;
        }
        #endregion

        #region IDeployment Members

        public override void Run()
        {
            if (VersionControlServer != null)
            {
                var list = DeploymentUtility.GetAllFiles(SourceFolder);
                foreach (string fileName in list)
                {
                    Workspace wpInfo = VersionControlServer.GetWorkspace(WorkspaceMapping);
                    if (wpInfo != null)
                    {
                        // map file to solution's directory
                        string solutionDir = Path.GetDirectoryName(WorkspaceMapping);
                        string destFile = fileName;
                        // if file is not in current solution, map the file's path to solution's path
                        if (!SourceFolder.Contains(solutionDir)) 
                        {
                            destFile = fileName.Replace(SourceFolder, solutionDir);
                        }
                        // check out
                        int affectedFiles = wpInfo.PendEdit(destFile, RecursionType.None);

                        bool alreadyCheckedOut = false;
                        if (affectedFiles > 0)
                        {
                            this._Output.AffectedFiles.Add(fileName);
                        }
                        else
                        {
                            ItemSpec iSpec = new ItemSpec(destFile, RecursionType.None);
                            PendingSet[] pendingSets = VersionControlServer.QueryPendingSets(new ItemSpec[]{iSpec}, wpInfo.Name, this.UserName, false);
                            foreach (PendingSet pset in pendingSets)
                            {
                                foreach (PendingChange pc in pset.PendingChanges)
                                {
                                    if (pc.ChangeType == ChangeType.Edit 
                                        && pc.VersionControlServer.AuthorizedUser.SuperEquals(this.UserName))
                                    {
                                        alreadyCheckedOut = true;
                                        break;
                                    }
                                }
                            }
                            if (!alreadyCheckedOut)
                            {
                                this._Output.ErrorFiles.Add(fileName);
                            }
                        }
                    }
                }
            }
        }

        public override DeployOutputBase GetOutput()
        {
            return _Output;
        }
        #endregion
    }
}
