using Microsoft.TeamFoundation.VersionControl.Client;
using SMTools.Deployment.Utility;
using SMTools.DeploymentBase;
using SMTools.DeploymentBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.TFSTransporter
{
    public class TFSCheckOut : TFSTransporterBase, IDeployment
    {
        private CheckOutOutput _Output = new CheckOutOutput();
        public string SourceFolder
        {
            get;
            set;
        }
        #region constructors
        public TFSCheckOut(string configFile)
            : base(configFile)
        {
        }
        public TFSCheckOut(string configFile, string sourceFolder)
            : base(configFile)
        {
            this.SourceFolder = sourceFolder;
        }
        #endregion

        #region IDeployment Members

        public void Run()
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
                            PendingSet[] pendingSets = VersionControlServer.QueryPendingSets(new ItemSpec[]{iSpec}, wpInfo.Name, this._UserName, false);
                            foreach (PendingSet pset in pendingSets)
                            {
                                foreach (PendingChange pc in pset.PendingChanges)
                                {
                                    if (pc.ChangeType == ChangeType.Edit 
                                        && pc.VersionControlServer.AuthorizedUser.SuperEquals(this._UserName))
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

        public StepOutputBase GetOutput()
        {
            return _Output;
        }

        public void ApplyConfiguration()
        {
           
        }

        #endregion
    }
}
