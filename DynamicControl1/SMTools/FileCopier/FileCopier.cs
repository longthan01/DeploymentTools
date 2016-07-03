using SMTools.Deployment.Utility;
using SMTools.DeploymentBase;
using SMTools.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SMTools.FileCopier
{
    /// <summary>
    /// Copy a folder to another folder in accordance with the structure of destination folder
    /// </summary>
    public class FileCopier : DeploymentProcessBase, IDeployment
    {
        #region fields, properties
        private const string _SEPERATOR = "__";
        private const string _FILE_DIR_INFOR_SEPERATOR = "___";
        private const string _BACKUP_ATTRIBUTE = "backup";
        private string _BackupPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\FileCopierBackup\\";

        private List<ConfigItem> _DestinationFoldersConfig;
        private List<string> _BackupFolders;
        private List<string> _ExcludeFolders;
        private List<FileCopierOutputItem> _CopierOutputs;

        /// <summary>
        /// Source folder to copy, can not be empty
        /// </summary>
        public string SourceFolder
        {
            get;
            set;
        }
        #endregion

        #region constructor
        public FileCopier(string configPath)
            : base(configPath)
        {

        }
        public FileCopier(string configPath, string sourceFolder)
            : base(configPath)
        {
            this.SourceFolder = sourceFolder;
        }
        #endregion

        #region protected methods
        private string GetDateTimeString()
        {
            string now = DateTime.Now
                .ToString()
                .Replace("/", "_")
                .Replace(":", "_");
            return now;
        }

        private string GenerateBackupFolder(string currentFolder)
        {
            string now = GetDateTimeString();
            string[] tokens = currentFolder.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            string fol = string.Empty;
            if (tokens.Length != 0)
            {
                fol = tokens[tokens.Length - 1];
            }
            return _BackupPath + fol + _SEPERATOR + now; //ex:  ...\\Folder__27_06_2016 01 AM
        }

        private FileCopierOutputItem Copy(DirInfor sourceFolder, string destinationFolder)
        {
            FileCopierOutputItem output = new FileCopierOutputItem();
            output.SourceFolder = sourceFolder.RelativeRoot;
            output.DestinationFolder = destinationFolder;
            Stack<DirInfor> stack = new Stack<DirInfor>();
            stack.Push(sourceFolder);
            while (stack.Count > 0)
            {
                DirInfor dir = stack.Pop();
                Directory.CreateDirectory(dir.RelativeRoot);
                foreach (FileInfor file in dir.Files)
                {
                    try
                    {
                        string destFile = file.FullPath.Replace(sourceFolder.RelativeRoot, destinationFolder);
                        File.Copy(file.FullPath, destFile);
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(10);
                        CopierFileErrorInfor fde = new CopierFileErrorInfor(ex.Message, file);
                        output.ErrorFiles.Add(fde);
                    }
                }
                foreach (DirInfor df in dir.SubDirectories)
                {
                    stack.Push(df);
                }
            }
            return output;
        }
        /// <summary>
        /// Copy all file from source folder to destination folder
        /// </summary>
        /// <param name="folder"></param>
        private FileCopierOutputItem Copy(string srcFolder, string destFolder)
        {
            DirInfor di = DeploymentUtility.GetDirInfor(srcFolder);
            return Copy(di, destFolder);
        }

        private void CopyBackup(string currentFolder)
        {
            string backupFolder = GenerateBackupFolder(currentFolder);
            // save backup files
            Copy(currentFolder, backupFolder);
            // store list backup folders to retrive output
            _BackupFolders.Add(backupFolder);
        }

        #endregion

        #region public methods

        /// <summary>
        /// Exclude destination folder copied
        /// </summary>
        /// <param name="dests"></param>
        public void ExcludeDestination(params string[] dests)
        {
            _ExcludeFolders = new List<string>();
            foreach (string folder in dests)
            {
                ConfigItem item = this
                    .ConfigurationItems
                    .FirstOrDefault(x => x.Name.Equals(folder, StringComparison.OrdinalIgnoreCase));
                if (item != null)
                {
                    this._ExcludeFolders.Add(item.Value);
                }
            }
        }

        public string GetFolderPath(string folder)
        {
            return this.GetConfigItem(folder).Value;
        }
        #endregion

        #region IDeployment Members

        public void Run()
        {
            if (this.SourceFolder == null)
            {
                throw new ArgumentNullException("SourceFolder", "Source folder is null, nothing is copied");
            }
            foreach (ConfigItem item in _DestinationFoldersConfig)
            {
                ConfigItem att = item.Attributes.FirstOrDefault(x => x.Name == _BACKUP_ATTRIBUTE);
                if (att != null && att.Value == "true")
                {
                    CopyBackup(item.Value); // copy backup
                }
                // copy from source to dest folder
                _CopierOutputs.Add(Copy(this.SourceFolder, item.Value));
            }
        }

        public StepOutputBase GetOutput()
        {
            FileCopierOutput o = new FileCopierOutput();
            o.OutputItems = _CopierOutputs;
            return o;
        }

        public void ApplyConfiguration()
        {
            _BackupFolders = new List<string>();
            _CopierOutputs = new List<FileCopierOutputItem>();
            _DestinationFoldersConfig = this.ConfigurationItems.FindAll(x => !this._ExcludeFolders.Contains(x.Value));
        }
        #endregion
    }
}
