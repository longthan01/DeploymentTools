using SMTools.Deployment.Base;
using SMTools.Deployment.Configurator;
using System;
using System.Linq;
using System.Collections.Generic;
using SMTools.Extensions;
using System.Reflection;
using SMTools.Utility;
using SMTools.Deployment.Base;

namespace SMTools.FileCopier
{
    public class FileCopierConfigurator : ConfiguratorBase
    {
        protected const string _BACKUP_FOLDER = "FileCopier_Backup";
        protected string BackupFolder = System.IO.Path.Combine(Assembly.GetExecutingAssembly().Location, _BACKUP_FOLDER);

        public List<string> ExcludeFolders { get; set; }
        public List<DestinationFolder> DestinationFolder
        {
            get;
            set;
        }
       
        public FileCopierConfigurator(ConfigItemCollection destinationConfigItems) : base (destinationConfigItems)
        {
            DestinationFolder = new List<DestinationFolder>();
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
        /// <summary>
        /// Exclude destinaton folder which will be copied
        /// </summary>
        /// <param name="folderName">Destination folder name</param>
        public void ExcludeDestination(params string[] folderName)
        {
            if (this.ExcludeFolders == null)
            {
                ExcludeFolders = new List<string>(folderName);
            }
            else
            {
                this.ExcludeFolders.AddRange(folderName);
            }
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
            base.ApplyConfig(process);
            FileCopier copier = process as FileCopier;
            ExcludeFolders = ExcludeFolders.Distinct().ToList();
            copier.DestinationFolders = DestinationFolder.FindAll(x => !this.ExcludeFolders.Contains(x.FolderName));
            copier.BackupFolder = BackupFolder;
        }
    }
}
