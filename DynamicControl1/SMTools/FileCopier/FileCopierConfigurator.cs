using SMTools.Deployment.Base;
using SMTools.Deployment.Configurator;
using SMTools.Deployment.FileCopier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTools.Extensions;
using System.Reflection;

namespace SMTools.DeploymentBase.FileCopier
{
    public class FileCopierConfigurator : ConfiguratorBase
    {
        protected const string _NEED_BACKUP = "NeedBackup";
        protected const string _BACKUP_FOLDER = "FileCopier_Backup";
        protected string BackupFolder = System.IO.Path.Combine(Assembly.GetExecutingAssembly().Location, _BACKUP_FOLDER);

        public List<string> ExcludeFolders { get; set; }

        public FileCopierConfigurator()
        {
            this.ExcludeFolders = new List<string>();
        }

        public override void ApplyConfig(ProcessBase process)
        {
            SMTools.FileCopier.FileCopier copier = process as SMTools.FileCopier.FileCopier;
            List<DestinationFolder> desFolders = new List<DestinationFolder>();
            foreach (var item in this.ConfigItems)
            {
                var attNB = this.ConfigItems.GetConfigAttribute(item.Name, _NEED_BACKUP);
                DestinationFolder f = new DestinationFolder()
                {
                    FolderName = item.Name,
                    Path = item.Value,
                    NeedBackup = attNB != null && attNB.Value.SuperEquals("true")
                };
                desFolders.Add(f);
            }
            desFolders = desFolders.FindAll(x => !this.ExcludeFolders.Contains(x.FolderName));
            copier.DestinationFolders = desFolders;
            copier.BackupFolder = BackupFolder;
        }

        public override void SaveConfiguration(ProcessBase process)
        {
            throw new NotImplementedException();
        }
    }
}
