using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Deployment.FileCopier
{
    public class DestinationFolder
    {
        public string FolderName { get; set; }
        public string Path { get; set; }
        public bool NeedBackup { get; set; }
    }
}
