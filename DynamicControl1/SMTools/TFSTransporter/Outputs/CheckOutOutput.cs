using SMTools.Deployment.Utility;
using SMTools.Deployment.Base;
using SMTools.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.TFSTransporter
{
    public class CheckOutOutput : DeployOutputBase
    {
        public FileInforCollection AffectedFiles
        {
            get;
            set;
        }
        public FileInforCollection ErrorFiles
        {
            get;
            set;
        }
       
        public CheckOutOutput()
        {
            ErrorFiles = new FileInforCollection();
            AffectedFiles = new FileInforCollection();
        }
    }
}
