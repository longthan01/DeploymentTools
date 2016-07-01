using SMTools.Deployment.Utility;
using SMTools.DeploymentBase;

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
