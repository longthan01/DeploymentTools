using SMTools.Tfs.Checkout;
using SMTools.TFSTransporter;
using SMTools.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.TFSTransporter.Checkout
{
    public class TfsCheckoutConfigurator : TfsConfigurator
    {
        public string SourceFolder
        {
            get;
            set;
        }
        public TfsCheckoutConfigurator(ConfigItemCollection configItems)
            : base(configItems)
        {
        }

        public TfsCheckoutConfigurator SetSourceFolder(string path)
        {
            this.SourceFolder = path;
            return this;
        }
        public override void ApplyConfig(Deployment.Base.ProcessBase process)
        {
            if (!Directory.Exists(this.SourceFolder))
            {
                this.ThrowException("Source folder could not be found: " + this.SourceFolder);
            }
            base.ApplyConfig(process);
            TfsCheckOut tfs = process as TfsCheckOut;
            tfs.SourceFolder = this.SourceFolder;
        }
    }
}
