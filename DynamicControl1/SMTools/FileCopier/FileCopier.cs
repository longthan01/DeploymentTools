using SMTools.Deployment.Utility;
using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Collections.Specialized;

namespace SMTools.FileCopier
{
    /// <summary>
    /// Copy a folder to another folder in accordance with the structure of destination folder
    /// </summary>
    public class FileCopier : ProcessBase, IDeployment
    {
        #region fields, properties
        private const string _SEPERATOR = "__";
        private StringCollection _BackupFolders;
        private List<FileCopierOutputItem> _CopierOutputs;

        public bool NeedBackup { get; set; }
        /// <summary>
        /// The path to backup folder
        /// </summary>
        public string BackupFolder { get; set; }
        /// <summary>
        /// Destination folder which process will copy file to
        /// </summary>
        public StringCollection DestinationFolders
        {
            get; set;
        }
        /// <summary>
        /// Exclude one or many folder configured in xml
        /// </summary>
        public List<string> DestinationExcludedFolders { get; set; }
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
            return BackupFolder + fol + _SEPERATOR + now; //ex:  ...\\Folder__27_06_2016 01 AM
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
            DestinationExcludedFolders = new List<string>();
            foreach (string folder in dests)
            {
                ConfigItem item = this
                    .ConfigurationItems
                    .FirstOrDefault(x => x.Name.Equals(folder, StringComparison.OrdinalIgnoreCase));
                if (item != null)
                {
                    this.DestinationExcludedFolders.Add(item.Value);
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
            foreach (string folder in DestinationFolders)
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

        public StepOutput GetOutput()
        {
            FileCopierOutput o = new FileCopierOutput();
            o.OutputItems = _CopierOutputs;
            return o;
        }

        public void ApplyConfiguration()
        {
            _BackupFolders = new List<string>();
            _CopierOutputs = new List<FileCopierOutputItem>();
            DestinationFolders = this.ConfigurationItems.FindAll(x => !this.DestinationExcludedFolders.Contains(x.Value));
        }
        #endregion
    }
}
