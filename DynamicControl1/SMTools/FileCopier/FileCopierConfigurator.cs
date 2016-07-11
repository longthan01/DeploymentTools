using SMTools.Deployment.Base;
using SMTools.Deployment.Configurator;
using System;
using System.Collections.Generic;
using SMTools.Extensions;
using System.Reflection;

namespace SMTools.FileCopier
{
    public class FileCopierConfigurator : ConfiguratorBase
    {
        protected const string _NEED_BACKUP = "NeedBackup";
        protected const string _BACKUP_FOLDER = "FileCopier_Backup";
        protected string BackupFolder = System.IO.Path.Combine(Assembly.GetExecutingAssembly().Location, _BACKUP_FOLDER);

        public List<string> ExcludeFolders { get; set; }
        public List<DestinationFolder> DestinationFolders { get; set; }
        public FileCopierConfigurator(string configFile, string configSection) : base(configFile, configSection)
        {
            ExcludeFolders = new List<string>();
            DestinationFolders = new List<SMTools.FileCopier.DestinationFolder>();
            foreach (var item in this.ConfigItems)
            {
                var attNB = this.ConfigItems.GetConfigAttribute(item.Name, _NEED_BACKUP);
                DestinationFolder f = new DestinationFolder()
                {
                    FolderName = item.Name,
                    Path = item.Value,
                    NeedBackup = attNB != null && attNB.Value.SuperEquals("true")
                };
                DestinationFolders.Add(f);
            }
        }

        public override void ApplyConfig(ProcessBase process)
        {
            FileCopier copier = process as FileCopier;
            copier.DestinationFolders = DestinationFolders.FindAll(x => !this.ExcludeFolders.Contains(x.FolderName));
            copier.BackupFolder = BackupFolder;
        }

        public override void SaveConfiguration(ProcessBase process)
        {
            throw new NotImplementedException();
        }
    }
}
