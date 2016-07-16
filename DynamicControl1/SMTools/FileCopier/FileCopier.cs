using SMTools.Deployment.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Collections.Specialized;
using SMTools.Deployment.Base;
using SMTools.FileCopier.Output;
using SMTools.Utility;

namespace SMTools.FileCopier
{
    /// <summary>
    /// Copy a folder to another folder in accordance with the structure of destination folder
    /// </summary>
    public class FileCopier : ProcessBase
    {
        #region fields, properties
        private const string _SEPERATOR = "__";
        private StringCollection _BackupFolders;
        

        /// <summary>
        /// The path to backup folder
        /// </summary>
        public string BackupFolder { get; set; }
        /// <summary>
        /// Source folder to copy, can not be empty
        /// </summary>
        public string SourceFolder
        {
            get;
            set;
        }
        /// <summary>
        /// Destination folder which process will copy file to
        /// </summary>
        public List<DestinationFolder> DestinationFolders
        {
            get; set;
        }
        #endregion

        #region constructor
        public FileCopier(IDeployConfigurator configurator)
            : base(configurator)
        {
            
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
                        FileErrorInfor fde = new FileErrorInfor(ex.Message, file);
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

        #region override

        public override void Run()
        {
            FileCopierOutput _Output = new FileCopierOutput();
            if (this.SourceFolder == null)
            {
                throw new ArgumentNullException("SourceFolder", "Source folder is null, nothing is copied");
            }
            if (this.Configurator != null)
            {
                this.Configurator.ApplyConfig(this); // to update user input 
            }
            foreach (var folder in DestinationFolders)
            {
                if (folder.NeedBackup)
                {
                    CopyBackup(folder.Path); // copy backup
                }
                // copy from source to dest folder
                _Output.OutputItems.Add(Copy(this.SourceFolder, folder.Path));
            }
            ProcessOutput = _Output;
        }

        public override void ConstructProperty()
        {
            _BackupFolders = new StringCollection();
        }
        #endregion
    }
}
