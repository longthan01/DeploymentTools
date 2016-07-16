using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTools.Utility;
using System.Configuration;

namespace SMDeployment.AppCodes
{
    public static class SessionManager
    {
        private const string _PATH = "Path";
        private const string _USER_SETTINGS = "UserSettings";
        private const string _TFS_DOWNLOAD_FOLDER = "TfsDownloadOutputFolder";
        private const string _FILE_COPY = "FileCopy";
        public static ConfigItemCollection PathCollection
        {
            get
            {
                return XmlLoader.GetConfig(_PATH);
            }
        }

        public static ConfigItemCollection UserSettings
        {
            get
            {
                return XmlLoader.GetConfig(_USER_SETTINGS);
            }
        }

        public static ConfigItemCollection FileCopierPathCollection
        {
            get {
                return XmlLoader.GetConfig(_FILE_COPY);
            }
        }

        public static string WorkingTfs
        {
            get
            {
                return ConfigurationManager.AppSettings["WorkingTfs"];
            }
        }

        public static string CurrentTfsUser
        {
            get
            {
                return XmlLoader.GetConfig(SessionManager.WorkingTfs)[ConstantString.TFS_USERNAME];
            }
            set
            {
                XmlLoader.GetConfig(SessionManager.WorkingTfs)[ConstantString.TFS_USERNAME] = value;
            }
        }

        public static string TfsDownloadOutputFolder
        {
            get
            {
                return UserSettings[_TFS_DOWNLOAD_FOLDER];
            }
            set
            {
                UserSettings[_TFS_DOWNLOAD_FOLDER] = value;
            }
        }
    }
}
