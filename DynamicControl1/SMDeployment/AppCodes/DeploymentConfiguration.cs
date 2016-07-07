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
    public static class DeploymentConfiguration
    {
        private static string ExecuteLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        /// <summary>
        /// Get configuration folder's path
        /// </summary>
        /// <param name="appSettingKey">The app setting key which config file is located</param>
        /// <returns>Absolute path to configuration file</returns>
        public static string GetConfigFolder(string appSettingKey)
        {
            return Path.Combine(ExecuteLocation, ConfigurationManager.AppSettings[appSettingKey]);
        }
        /// <summary>
        /// Get configuration file.
        /// </summary>
        /// <param name="folder">Folder which it is located</param>
        /// <param name="environment">The environment
        /// Ex:
        /// TFS/Dev.xml
        /// TFS/QA.xml
        /// </param>
        /// <returns></returns>
        public static string GetPath(ConfigFolder folder, DeployEnvironment environment)
        {
            return Path.Combine(GetConfigFolder(folder.ToString()), environment.ToString() + ".xml");
        }
        /// <summary>
        /// Get shared configuration file. Configuration file must be named the same as folder it is located
        /// </summary>
        /// <param name="folder">Shared folder. 
        /// Ex: FileCopier/FileCopier.xml
        /// </param>
        /// <returns>Path to config file</returns>
        public static string GetPath(SharedConfigFolder folder)
        {
            return Path.Combine(GetConfigFolder(folder.ToString()), folder.ToString() + ".xml");
        }
    }
}
