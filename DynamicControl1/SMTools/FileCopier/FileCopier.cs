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

        private FileCopierOutputItem Copy(string srcFolder, DestinationFolder destFolder)
        {
            // backup folder
            string backupFolder = string.Empty;
            if (destFolder.NeedBackup)
            {
                backupFolder = GenerateBackupFolder(destFolder.Path);
                _BackupFolders.Add(backupFolder);
            }
            // get source dir infor
            DirInfor sourceFolder = DeploymentUtility.GetDirInfor(srcFolder);
            // output information
            FileCopierOutputItem output = new FileCopierOutputItem();
            output.SourceFolder = sourceFolder.RelativeRoot;
            output.DestinationFolder = destFolder.Path;
            // use stack to reduce memory consuming
            Stack<DirInfor> stack = new Stack<DirInfor>();
            stack.Push(sourceFolder);

            while (stack.Count > 0)
            {
                DirInfor dir = stack.Pop();
                string bkfolder = string.Empty;
                if (destFolder.NeedBackup)
                {
                    string destFol = dir.RelativeRoot.Replace(srcFolder, destFolder.Path);
                    if (Directory.Exists(destFol)) // check if folder is existed in destination folder
                    {
                        bkfolder = dir.RelativeRoot.Replace(srcFolder, backupFolder);
                        Directory.CreateDirectory(bkfolder); // create backup folder
                    }
                }
                string destPath = dir.RelativeRoot.Replace(srcFolder, destFolder.Path);
                Directory.CreateDirectory(destPath);
                foreach (FileInfor file in dir.Files)
                {
                    try
                    {
                        string destFile = file.FullPath.Replace(srcFolder, destFolder.Path);
                        if (destFolder.NeedBackup)
                        {
                            if (File.Exists(destFile))
                            {
                                string bkFile = destFile.Replace(destFolder.Path, backupFolder);
                                File.Copy(destFile, bkFile, true); // copy dest file to backup file
                            }
                        }
                        File.Copy(file.FullPath, destFile, true); // copy source file to dest file
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
            foreach (var destFolder in DestinationFolders)
            {
                // copy from source to dest folder
                _Output.OutputItems.Add(Copy(SourceFolder, destFolder));
            }
            ProcessOutput = _Output;
        }

        public override void ConstructProperty()
        {
            _BackupFolders = new StringCollection();
            ProcessOutput = new ProcessOutputBase();
        }
        #endregion
    }
}
