using SMTools.Deployment.Utility;
using SMTools.DeploymentBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.FileCopier
{
    public class CopierFileErrorInfor : FileInfor
    {
        public string Error
        {
            get;
            set;
        }
       
        public CopierFileErrorInfor(string error, FileInfor fi)
        {
            this.Error = error;
            this.FileName = fi.FileName;
            this.FullPath = fi.FullPath;
            this.ShortPath = fi.ShortPath;
            this.ModifiedDate = fi.ModifiedDate;
        }
    }
}
