using SMTools.TFSTransporter;
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
        public List<string> Files
        {
            get;
            set;
        }
        public TfsDownloadFileConfigurator()
        {

        }
        public override void ApplyConfig(Deployment.Base.ProcessBase process)
        {
            base.ApplyConfig(process);
            TfsDownloadFile tfs = process as TfsDownloadFile;
            tfs.OutputFolder = this.OutputFolder;
            tfs.Files = this.Files;
        }
    }
}
