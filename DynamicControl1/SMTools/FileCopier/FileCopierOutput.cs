using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.FileCopier
{
    public class FileCopierOutput : StepOutputBase
    {
        public List<FileCopierOutputItem> OutputItems
        {
            get;
            set;
        }
    }
}
