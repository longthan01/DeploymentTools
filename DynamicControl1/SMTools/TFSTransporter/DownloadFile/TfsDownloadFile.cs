using Microsoft.TeamFoundation.VersionControl.Client;
using SMTools.Deployment.Base;
using SMTools.Tfs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Tfs.DownloadFile
{
    public class TfsDownloadFile : TfsTransporter
    {
        #region IDeployment Members
        public string OutputFolder
        {
            get;
            set;
        }
        public List<string> Files
        {
            get;
            set;
        }
        public TfsDownloadFile(IDeployConfigurator configurator)
            : base(configurator)
        {
            
        }
        public override void Run()
        {
            var serverFolder = CurrentWorkspace.GetWorkingFolderForLocalItem(System.IO.Path.GetDirectoryName(TfsInfor.WorkspaceMapping));
            var localRootFolder = serverFolder.LocalItem;
            foreach (var f in Files)
            {
                var item = this.VersionControlServer.GetItem(f, VersionSpec.Latest);
                string destFile = f.Replace(localRootFolder, OutputFolder);
                item.DownloadFile(destFile);
            }
        }
        #endregion
    }
}
