using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTools.Extensions;

namespace SMTools.MSBuilder
{
    public class Deploymenter : BuildProcess, IDeployment
    {
        public BuildProcess BaseBuilder
        {
            get;
            set;
        }

        public Deploymenter(BuildProcess baseBuilder)
        {
            this.BaseBuilder = baseBuilder;
        }

        public Deploymenter(string configFile)
            : base(configFile)
        {
            BaseBuilder = new BuildProcess(configFile);
        }

        #region IDeployment Members
        
        public void ApplyConfiguration()
        {
            var item = BaseBuilder.ConfigurationItems.FirstOrDefault(x => x.Name.SuperEquals(_SolutionPath));
            if (item != null)
            {
                BaseBuilder.ConfigurationItems.Remove(item);
            }
            BaseBuilder.ApplyConfiguration();
        }

        public void Run()
        {
            BaseBuilder.Run();
        }

        public DeployOutputBase GetOutput()
        {
            return BaseBuilder.GetOutput();
        }

        #endregion
    }
}
