using SMTools.Deployment.Base;
using SMTools.Deployment.Configurator;
using System;
using System.Linq;
using System.Collections.Generic;
using SMTools.Extensions;
using System.Reflection;
using SMTools.Utility;
using SMTools.Deployment.Base;
using System.IO;

namespace SMTools.FileCopier
{
    public class FileCopierConfigurator : ConfiguratorBase
    {
        protected const string _BACKUP_FOLDER = "FileCopier_Backup_";
        protected string BackupFolder = System.IO.Path.Combine(
            System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            _BACKUP_FOLDER);

        public string SourceFolder { get; set; }
        public List<string> ExcludeFolders { get; set; }
        public List<DestinationFolder> DestinationFolder
        {
            get;
            set;
        }

        public FileCopierConfigurator(ConfigItemCollection destinationConfigItems) : base(destinationConfigItems)
        {
            DestinationFolder = new List<DestinationFolder>();
            ExcludeFolders = new List<string>();
            foreach (var item in this.ConfigItems)
            {
                var attNB = this.ConfigItems.GetConfigAttribute(item.GetName(), ConstantString.FILECOPY_NEED_BACKUP);
                DestinationFolder f = new DestinationFolder()
                {
                    FolderName = item.GetName(),
                    Path = item.GetValue(),
                    NeedBackup = attNB != null && attNB.GetValue().SuperEquals("true")
                };
                DestinationFolder.Add(f);
            }
        }

        public void SetSourceFolder(string folder)
        {
            this.SourceFolder = folder;
        }

        public void SetBackup(string folderName, bool needBackup)
        {
            var fol = this.DestinationFolder.FirstOrDefault(x => x.FolderName.SuperEquals(folderName));
            if (fol != null)
            {
                fol.NeedBackup = needBackup;
            }
        }

        /// <summary>
        /// Exclude destinaton folder which will be copied
        /// </summary>
        /// <param name="folderName">Destination folder name</param>
        public void ExcludeDestination(params string[] folderName)
        {
            this.ExcludeFolders.AddRange(folderName);
        }
        /// <summary>
        /// Exclude destinaton folder which will be copied
        /// </summary>
        /// <param name="folderName">Destination folder name</param>
        public void IncludeDestination(params string[] folderName)
        {
            if (this.ExcludeFolders != null)
            {
                foreach (string folder in folderName)
                {
                    var item = this.ExcludeFolders.FirstOrDefault(x => x.SuperEquals(folder));
                    if (!string.IsNullOrEmpty(item))
                    {
                        this.ExcludeFolders.Remove(item);
                    }
                }
            }
        }

        public override void ApplyConfig(ProcessBase process)
        {
            if (!Directory.Exists(this.SourceFolder)) this.ThrowException("Source folder is not exists: " + this.SourceFolder);
            base.ApplyConfig(process);
            FileCopier copier = process as FileCopier;
            copier.SourceFolder = this.SourceFolder;
            ExcludeFolders = ExcludeFolders.Distinct().ToList();
            copier.DestinationFolders = DestinationFolder.FindAll(x => !this.ExcludeFolders.Contains(x.FolderName));
            copier.BackupFolder = BackupFolder;
        }
    }
}
