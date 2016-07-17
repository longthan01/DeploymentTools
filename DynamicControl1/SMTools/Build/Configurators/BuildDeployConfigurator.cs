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
            var item = this.ConfigItems.GetConfigItem(ConstantString.BUILDDEPLOY_PATH);
            if (item != null)
            {
                item.SetValue(path);
            }
            else
            {
                this.ConfigItems.Insert(1, new ConfigItem()
                {
                    Name = ConstantString.BUILDDEPLOY_PATH,
                    Value = path
                });
            }
            return this;
        }

        public string GetLogFile()
        {
            return ConfigItems[ConstantString.BUILD_BuildLogFile];
        }
        public void SetLogFile(string path)
        {
            ConfigItems[ConstantString.BUILD_BuildLogFile] = path;
        }
        public override void ApplyConfig(ProcessBase process)
        {
            base.ApplyConfig(process);
            if (this.BuildPath == null)
            {
                ThrowException("Build path is empty, cannot start build process");
            }
            CommandLineProcess buildProcess = process as CommandLineProcess;
            foreach (var item in ConfigItems)
            {
                if (item.GetName().SuperEquals(ConstantString.BUILDDEPLOY_PATH))
                {
                    buildProcess.Command.Append(" " + item.GetValue()).Append(" ");
                }
                else
                {
                    buildProcess.Command.Append(item.GetName() + item.GetValue()).Append(" ");
                }
            }
            buildProcess.LogFile = ConfigItems[ConstantString.BUILD_BuildLogFile];
            if (System.IO.File.Exists(buildProcess.LogFile))
            {
                System.IO.File.Delete(buildProcess.LogFile);
            }
        }
    }
}
