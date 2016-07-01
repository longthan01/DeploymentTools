using SMTools.DeploymentBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Deployment.Utility
{
    public class FileDirCollection : List<FileDirInfor>
    {
        public FileDirCollection Add(string filePath)
        {
            FileDirInfor fdi = DeploymentUtility.GetInfor(filePath);
            this.Add(fdi);
            return this;
        }
    }
}
