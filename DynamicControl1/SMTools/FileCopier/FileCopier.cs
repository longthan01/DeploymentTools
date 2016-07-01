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
    public enum DestinationFolderType
    {
        MSv200,
        MSv200sj,
        CSv200,
        US_MSv100,
        US_MSv100sj,
        US_MSv100UAT,
        US_MSv100UATsj,
        US_CSv100,
        US_CSv100UAT,
        All
    }
    /// <summary>
    /// Copy a folder to another folder in accordance with the structure of destination folder
    /// </summary>
    public class FileCopier : DeploymentProcessBase, IDeployment
    {
        #region fields, properties
        private const string _SEPERATOR = "__";
        private const string _FILE_DIR_INFOR_SEPERATOR = "___";
        private const string _FILEDIRINFOR_SEPERATOR = "|";
        private const string _TEMP_FILEDIRINFOR = "_FileCopierTemp.sm";
        private const string _BACKUP_ATT = "backup";
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

        /// <summary>
        /// Create backup file's name by current folder
        /// </summary>
        /// <returns>Backup file's name</returns>
        private string GenerateBackupFileDirInfor(string backupFolder)
        {
            return backupFolder + _FILE_DIR_INFOR_SEPERATOR + _TEMP_FILEDIRINFOR;
        }

        /// <summary>
        /// Retrieve backup file's name by folder backup
        /// </summary>
        /// <param name="folderBackup">Backup folder</param>
        /// <returns>Backup file's name</returns>
        private string RetrieveBackupFileDirInfor(string folderBackup)
        {
            return folderBackup + _FILE_DIR_INFOR_SEPERATOR + _TEMP_FILEDIRINFOR;
        }

        private FileCopierOutputItem Copy(List<FileDirInfor> files, string sourceFolder, string destinationFolder)
        {
            FileCopierOutputItem output = new FileCopierOutputItem();
            output.SourceFolder = sourceFolder;
            output.DestinationFolder = destinationFolder;
            foreach (FileDirInfor fi in files)
            {
                bool isDir = File.GetAttributes(fi.FullPath).HasFlag(FileAttributes.Directory);
                string dir = isDir ? fi.FullPath : System.IO.Path.GetDirectoryName(fi.FullPath);
                string dest = dir.Replace(sourceFolder, destinationFolder);
                try
                {
                    Directory.CreateDirectory(dest);
                    if (!fi.IsEmptyDirectory)
                    {
                        string desFileName = dest + "\\" + System.IO.Path.GetFileName(fi.FullPath);
                        if (File.Exists(desFileName))
                        {
                            FileAttributes atts = File.GetAttributes(desFileName);
                            if (atts.HasFlag(FileAttributes.ReadOnly))
                            {
                                // remove readonly from destination file
                                File.SetAttributes(desFileName, atts & ~FileAttributes.ReadOnly);
                            }
                        }
                        File.Copy(fi.FullPath, desFileName, true);
                    }
                }
                catch (Exception ex)
                {
                    Thread.Sleep(10);
                    CopierFileDirErrorInfor fde = new CopierFileDirErrorInfor(ex.Message, fi.IsEmptyDirectory, fi.FullPath, fi.ShortPath, fi.ModifiedDate);
                    output.ErrorFiles.Add(fde);
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
            List<FileDirInfor> srcs = GetFileDirInfor(srcFolder);
            return Copy(srcs, srcFolder, destFolder);
        }

        /// <summary>
        /// Save file dir information to temporary file.
        /// </summary>
        /// <param name="files">List files</param>
        /// <param name="backupFolder">Backup folder which files is saved</param>
        private void SaveFileDirInforBackup(List<FileDirInfor> files, string backupFolder)
        {
            StreamWriter sw = new StreamWriter(GenerateBackupFileDirInfor(backupFolder));
            // FullPath|ModifiedDate|IsEmptyDirectory
            files.ForEach(x => sw.WriteLine(x.FullPath + _FILEDIRINFOR_SEPERATOR + x.ModifiedDate.ToString() + _FILEDIRINFOR_SEPERATOR + x.IsEmptyDirectory));
            sw.Flush();
            sw.Close();
        }

        private List<FileDirInfor> LoadFileDirInforBackup(string file)
        {
            List<FileDirInfor> res = new List<FileDirInfor>();
            string[] lines = System.IO.File.ReadAllLines(file);
            foreach (string sr in lines)
            {
                string[] tks = sr.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                FileDirInfor fi = new FileDirInfor()
                {
                    FullPath = tks[0],
                    ModifiedDate = DateTime.Parse(tks[1]),
                    IsEmptyDirectory = tks[2] == "True" ? true : false
                };
                res.Add(fi);
            }
            return res;
        }

        private void CopyBackup(string currentFolder)
        {
            string backupFolder = GenerateBackupFolder(currentFolder);
            // save backup files
            Copy(currentFolder, backupFolder);
            List<FileDirInfor> fdinfors = GetFileDirInfor(currentFolder);
            SaveFileDirInforBackup(fdinfors, backupFolder);
            // store list backup folders to retrive output
            _BackupFolders.Add(backupFolder);
        }

        /// <summary>
        /// Get all file and directories in folder, without using recusion
        /// </summary>
        /// <param name="folder">Searching folder</param>
        /// <returns>A List of <see cref="FileDirInfor"/></returns>
        public List<FileDirInfor> GetFileDirInfor(string folder)
        {
            return DeploymentUtility.GetFileDirInfor(folder);
        }
        #endregion

        #region public methods
        
        /// <summary>
        /// Exclude destination folder copied
        /// </summary>
        /// <param name="dests"></param>
        public void ExcludeDestination(params DestinationFolderType[] dests)
        {
            _ExcludeFolders = new List<string>();
            foreach (DestinationFolderType d in dests)
            {
                ConfigItem item = this
                    .ConfigurationItems
                    .FirstOrDefault(x => x.Name.Equals(d.ToString(), StringComparison.OrdinalIgnoreCase));
                if (item != null)
                {
                    this._ExcludeFolders.Add(item.Value);
                }
            }
        }
        public string GetFolderPath(DestinationFolderType folder)
        {
            return this.GetFolderPath(folder.ToString());
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
            List<FileDirInfor> fromFiles = this.GetFileDirInfor(this.SourceFolder);
            foreach (ConfigItem item in _DestinationFoldersConfig)
            {
                ConfigItem att = item.Attributes.FirstOrDefault(x => x.Name == _BACKUP_ATT);
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
