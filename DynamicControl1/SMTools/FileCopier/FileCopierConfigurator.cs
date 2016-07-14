using SMTools.Deployment.Base;
using SMTools.Deployment.Configurator;
using System;
using System.Collections.Generic;
using SMTools.Extensions;
using System.Reflection;
using SMTools.Utility;
using SMTools.Deployment.Base;

namespace SMTools.FileCopier
{
    public class FileCopierConfigurator : ConfiguratorBase
    {
        protected const string _NEED_BACKUP = "NeedBackup";
        protected const string _BACKUP_FOLDER = "FileCopier_Backup";
        protected string BackupFolder = System.IO.Path.Combine(Assembly.GetExecutingAssembly().Location, _BACKUP_FOLDER);

        public List<string> ExcludeFolders { get; set; }
        private List<DestinationFolder> _DestinationFolder
        {
            get;
            set;
        }

        public FileCopierConfigurator(ConfigItemCollection destinationConfigItems) : base (destinationConfigItems)
        {
            _DestinationFolder = new List<DestinationFolder>();
            foreach (var item in this.ConfigItems)
            {
                var attNB = this.ConfigItems.GetConfigAttribute(item.GetName(), _NEED_BACKUP);
                DestinationFolder f = new DestinationFolder()
                {
                    FolderName = item.GetName(),
                    Path = item.GetValue(),
                    NeedBackup = attNB != null && attNB.GetValue().SuperEquals("true")
                };
                _DestinationFolder.Add(f);
            }
        }

        public void ExcludeDestination(params string[] folders)
        {
            if (this.ExcludeFolders == null)
            {
                ExcludeFolders = new List<string>(folders);
            }
            else
            {
                this.ExcludeFolders.AddRange(folders);
            }
        }

        public override void ApplyConfig(ProcessBase process)
        {
            base.ApplyConfig(process);
            FileCopier copier = process as FileCopier;
            copier.DestinationFolders = _DestinationFolder.FindAll(x => !this.ExcludeFolders.Contains(x.FolderName));
            copier.BackupFolder = BackupFolder;
        }
    }
}
