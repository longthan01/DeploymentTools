using SMTools.Deployment.Utility;
using SMTools.DeploymentBase;
using SMTools.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.TFSTransporter
{
    public class CheckOutOutput : StepOutputBase
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
