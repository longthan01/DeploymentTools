using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDeployment.UIModels.BuildDeploy
{
    public class BuildConfigInfor
    {
        public string SolutionPath
        {
            get;
            set;
        }
        public string LogFile
        {
            get;
            set;
        }
    }
    public class DeployConfigInfor
    {
        public string ProjectPath
        {
            get;
            set;
        }
        /// <summary>
        /// Path to deployment output folder.
        /// </summary>
        public string DeploymentOutputFolder
        {
            get;
            set;
        }
        public string LogFile
        {
            get;
            set;
        }
    }
}
