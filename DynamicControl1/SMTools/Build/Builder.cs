using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTools.Extensions;
namespace SMTools.MSBuilder
{
    public class Builder : BuildProcess, IDeployment
    {
        protected const string _DeployOnBuild = "/p:DeployOnBuild=";

        public BuildProcess BaseBuilder
        {
            get;
            set;
        }

        public Builder(BuildProcess baseBuilder)
        {
            this.BaseBuilder = baseBuilder;
        }

        public Builder(string configFile) : base(configFile)
        {
            if (BaseBuilder == null)
            {
                BaseBuilder = new BuildProcess(configFile);
            }
        }
        #region IDeployment Members
        
        public void ApplyConfiguration()
        {
            BaseBuilder.ConfigurationItems.SetItemValue(_DeployOnBuild, "false");
            var item = BaseBuilder.ConfigurationItems.FirstOrDefault(x => x.Name.SuperEquals(_ProjectPath));
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
