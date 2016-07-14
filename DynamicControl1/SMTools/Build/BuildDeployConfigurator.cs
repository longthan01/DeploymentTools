using SMTools.Deployment.Base;
using SMTools.Deployment.Configurator;
using System;
using System.Linq;
using SMTools.Extensions;
using SMTools.Utility;
using SMTools.Deployment.Base;
using SMTools.Models;
using System.Collections;
using System.Collections.Generic;

namespace SMTools.Build.Base
{
    public class BuildDeployConfigurator : ConfiguratorBase
    {
        protected string BuildPath;

        public BuildDeployConfigurator(ConfigItemCollection itemsCollection)
            : base(itemsCollection)
        {

        }

        public BuildDeployConfigurator SetBuildPath(string path)
        {
            BuildPath = path;
            this.ConfigItems.Insert(1, new ConfigItem()
            {
                Name = string.Empty,
                Value = path
            });
            return this;
        }

        public override void ApplyConfig(ProcessBase process)
        {
            base.ApplyConfig(process);
            if (this.BuildPath == null)
            {
                throw new ArgumentNullException("BuildPath", "Build path is empty, cannot start build process");
            }
            BuildDeployProcess buildProcess = process as BuildDeployProcess;
            foreach (var item in ConfigItems)
            {
                buildProcess.BuildCommand.Append(item.GetName()).Append(" ").Append(item.GetValue());
            }
            buildProcess.LogFile = ConfigItems[ConstantString.BUILD_BuildLogFile];
        }
    }
}
