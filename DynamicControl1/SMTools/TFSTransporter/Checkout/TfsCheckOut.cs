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
using SMTools.TFSTransporter;

namespace SMTools.Tfs.Checkout
{
    public class TfsCheckOut : TfsTransporter
    {
        public string SourceFolder
        {
            get;
            set;
        }
        #region constructors
        public TfsCheckOut(IDeployConfigurator configurator, string sourceFolder)
            : base(configurator)
        {
            this.SourceFolder = sourceFolder;
        }
        #endregion

        #region IDeployment Members

        public override void Run()
        {
            CheckOutOutput output = new CheckOutOutput();
            var list = DeploymentUtility.GetAllFiles(SourceFolder);
            foreach (string fileName in list)
            {
                // map file to solution's directory
                string solutionDir = Path.GetDirectoryName(TfsInfor.WorkspaceMapping);
                string destFile = fileName;
                // if file is not in current solution, map the file's path to solution's path
                if (!SourceFolder.Contains(solutionDir))
                {
                    destFile = fileName.Replace(SourceFolder, solutionDir);
                }
                // check out
                int affectedFiles = this.CurrentWorkspace.PendEdit(destFile, RecursionType.None);

                bool alreadyCheckedOut = false;
                if (affectedFiles > 0)
                {
                    output.AffectedFiles.Add(fileName);
                }
                else
                {
                    ItemSpec iSpec = new ItemSpec(destFile, RecursionType.None);
                    PendingSet[] pendingSets = VersionControlServer.QueryPendingSets(new ItemSpec[] { iSpec }, CurrentWorkspace.Name, TfsInfor.UserName, false);
                    foreach (PendingSet pset in pendingSets)
                    {
                        foreach (PendingChange pc in pset.PendingChanges)
                        {
                            if (pc.ChangeType == ChangeType.Edit
                                && pc.VersionControlServer.AuthorizedUser.SuperEquals(TfsInfor.UserName))
                            {
                                alreadyCheckedOut = true;
                                break;
                            }
                        }
                    }
                    if (!alreadyCheckedOut)
                    {
                        output.ErrorFiles.Add(fileName);
                    }
                }
            }
            this.ProcessOutput = output;
        }

        #endregion
    }
}
