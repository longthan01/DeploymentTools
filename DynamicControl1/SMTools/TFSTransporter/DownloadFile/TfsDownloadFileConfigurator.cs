using SMTools.Tfs.Searcher;
using SMTools.TFSTransporter;
using SMTools.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Tfs.DownloadFile
{
    public class TfsDownloadFileConfigurator : TfsConfigurator
    {
        public string OutputFolder
        {
            get;
            set;
        }
        public List<TfsSearchOutputItem> Files
        {
            get;
            set;
        }

        public TfsDownloadFileConfigurator(ConfigItemCollection configItems) : base(configItems)
        {

        }

        public TfsDownloadFileConfigurator SetOutputFolder(string outputFolder)
        {
            this.OutputFolder = outputFolder;
            return this;
        }

        public TfsDownloadFileConfigurator SetFileToDownload(List<TfsSearchOutputItem> files)
        {
            this.Files = files;
            return this;
        }

        public override void ApplyConfig(Deployment.Base.ProcessBase process)
        {
            base.ApplyConfig(process);
            TfsDownloadFile tfs = process as TfsDownloadFile;
            tfs.OutputFolder = this.OutputFolder;
            tfs.Files = this.Files.Select(x => x.LocalPath).ToList();
        }
    }
}
