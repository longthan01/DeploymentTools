using SMTools.DeploymentBase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SMDeployment.AppCodes
{
    public enum ConfigKey
    {
        FileCopierConfig,
        DevBuilderConfig,
        QABuilderConfig,
        USBuilderConfig,
        DevTFSConfig,
        QATFSConfig,
        USTFSConfig
    }

    public static class DeploymentConfiguration
    {
        private static string ExecuteLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string GetAppSettingValue(ConfigKey appSettingKey)
        {
            return Path.Combine(ExecuteLocation,ConfigurationManager.AppSettings[appSettingKey.ToString()]);
        }
        public static string GetProjectPath(ConfigKey key)
        {
            string p = GetAppSettingValue(key);
            var collection = XmlLoader.GetConfig(p);
            return collection.GetConfigItemValue("ProjectPath");
        }
    }
}
