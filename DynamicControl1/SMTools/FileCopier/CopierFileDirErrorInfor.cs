using SMTools.Deployment.Utility;
using SMTools.DeploymentBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.FileCopier
{
    public class CopierFileDirErrorInfor : FileDirInfor
    {
        public string Error
        {
            get;
            set;
        }
       
        public CopierFileDirErrorInfor(string error, bool isEmptyDir, string fullPath, string shortPath, DateTime modifiedDate)
        {
            this.Error = error;
            this.FullPath = fullPath;
            this.IsEmptyDirectory = isEmptyDir;
            this.ShortPath = shortPath;
            this.ModifiedDate = modifiedDate;
        }
    }
}
