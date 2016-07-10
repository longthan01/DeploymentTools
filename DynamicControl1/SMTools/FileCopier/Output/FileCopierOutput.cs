using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.FileCopier.Output
{
    public class FileCopierOutput : DeployOutputBase
    {
        public FileCopierOutput() { this.OutputItems = new List<FileCopierOutputItem>(); }

        public List<FileCopierOutputItem> OutputItems
        {
            get;
            set;
        }
    }
}
