using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.FileCopier.Output
{
    public class FileCopierOutputItem
    {
        public string SourceFolder
        {
            get;
            set;
        }
        public string DestinationFolder
        {
            get;
            set;
        }
        public List<FileErrorInfor> ErrorFiles
        {
            get;
            set;
        }
        public FileCopierOutputItem()
        {
            this.ErrorFiles = new List<FileErrorInfor>();
        }
    }
}
