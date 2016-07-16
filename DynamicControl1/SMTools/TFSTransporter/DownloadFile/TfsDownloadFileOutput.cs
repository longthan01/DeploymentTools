using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.TFSTransporter.DownloadFile
{
    public class TfsDownloadFileOutput : ProcessOutputBase
    {
        public int FilesDownloaded
        {
            get;
            set;
        }
    }
}
