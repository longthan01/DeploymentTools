using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDeployment.UIModels.MSBuilder
{
    public class BuildConfigInfor
    {
        public string PathToProject
        {
            get;
            set;
        }
        /// <summary>
        /// Path to deployment output folder.
        /// </summary>
        public string DeploymentOutput
        {
            get;
            set;
        }
    }
}
