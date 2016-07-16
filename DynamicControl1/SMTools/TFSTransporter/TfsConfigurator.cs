using SMTools.Deployment.Configurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTools.Deployment.Base;
using SMTools.Tfs;
using SMTools.Utility;
using SMTools.Build.Base;
using SMTools.Models;

namespace SMTools.TFSTransporter
{
    public class TfsConfigurator : ConfiguratorBase
    {
        public TfsConfigurator(ConfigItemCollection configItems)
            : base(configItems)
        {
        }
        public string GetServerUrl()
        {
            return this.ConfigItems[ConstantString.TFS_SERVERURL];
        }
        public string GetWorkspaceMapping()
        {
            return this.ConfigItems[ConstantString.TFS_WORKSPACE_MAPPING];
        }
        public TfsConfigurator SetWorkspaceMapping(string workspace)
        {
            if (this.ConfigItems.GetConfigItem(ConstantString.TFS_WORKSPACE_MAPPING) != null)
            {
                this.ConfigItems[ConstantString.TFS_WORKSPACE_MAPPING] = workspace;
            }
            else
            {
                this.ConfigItems.Add(new ConfigItem()
                {
                    Name = ConstantString.TFS_WORKSPACE_MAPPING,
                    Value = workspace
                });
            }
            return this;
        }

        public override void ApplyConfig(ProcessBase process)
        {
            base.ApplyConfig(process);
            if (string.IsNullOrEmpty(this.GetWorkspaceMapping()))
            {
                ThrowException("WorkspaceMapping is null, cannot create Tfs instance");
            }
            TfsTransporter tfs = process as TfsTransporter;
            tfs.TfsInfor.WorkspaceMapping = GetWorkspaceMapping();
            tfs.TfsInfor.UserName = this.ConfigItems[ConstantString.TFS_USERNAME];
            tfs.TfsInfor.Password = this.ConfigItems[ConstantString.TFS_PASSWORD];
            tfs.TfsInfor.ServerUrl = this.ConfigItems[ConstantString.TFS_SERVERURL];
            tfs.TfsInfor.Domain = this.ConfigItems[ConstantString.TFS_DOMAIN];
            tfs.TfsInfor.NeedAuthenticate = this.ConfigItems[ConstantString.TFS_NEED_AUTHENTICATE] == "true";
        }
    }
}
