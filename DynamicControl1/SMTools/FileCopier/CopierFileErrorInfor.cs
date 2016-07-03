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
       
        public CopierFileErrorInfor(string error, FileInfor file)
        {
            this.Error = error;
            this.FullPath = file.FullPath;
            this.ShortPath = file.ShortPath;
            this.ModifiedDate = file.ModifiedDate;
        }
    }
}
