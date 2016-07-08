using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Deployment.Utility
{
    public class FileInforCollection : List<FileInfor>
    {
        public FileInforCollection Add(string filePath)
        {
            FileInfor fdi = DeploymentUtility.GetFileInfor(filePath);
            this.Add(fdi);
            return this;
        }
    }
}
