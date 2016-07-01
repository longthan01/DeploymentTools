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
        public FileDirCollection AffectedFiles
        {
            get;
            set;
        }
        public FileDirCollection ErrorFiles
        {
            get;
            set;
        }
       
        public CheckOutOutput()
        {
            ErrorFiles = new FileDirCollection();
            AffectedFiles = new FileDirCollection();
        }
    }
}
