using SMTools.Deployment.Utility;
using SMTools.DeploymentBase;

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
