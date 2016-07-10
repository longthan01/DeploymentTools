using SMTools.Deployment.Configurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTools.Deployment.Base;
using SMTools.Tfs;

namespace SMTools.DeploymentBase.TFSTransporter
{
    public class TfsConfigurator : ConfiguratorBase
    {
        protected const string _USERNAME = "username";
        protected const string _PASSWORD = "password";
        protected const string _SERVERURL = "ServerUrl";
        protected const string _DOMAIN = "domain";
        protected const string _PORT = "port";
        protected const string _NEED_AUTHENTICATE = "NeedAuthenticate";
        protected const string _WORKSPACE_MAPPING = "ProjectPath";

        public TfsConfigurator(string configFile, string configType) : base(configFile, configType) { }

        public override void ApplyConfig(ProcessBase process)
        {
            TfsTransporter tfs = process as TfsTransporter;
            tfs.UserName = this.ConfigItems.GetConfigItemValue(_USERNAME);
            tfs.Password = this.ConfigItems.GetConfigItemValue(_PASSWORD);
            tfs.ServerUrl = this.ConfigItems.GetConfigItemValue(_SERVERURL);
            tfs.Domain = this.ConfigItems.GetConfigItemValue(_DOMAIN);
            tfs.NeedAuthenticate = this.ConfigItems.GetConfigItemValue(_NEED_AUTHENTICATE) == "true";
        }
    }
}
