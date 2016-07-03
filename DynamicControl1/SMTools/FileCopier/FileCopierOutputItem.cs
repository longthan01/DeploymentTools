using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.FileCopier
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
        public List<CopierFileErrorInfor> ErrorFiles
        {
            get;
            set;
        }
        public FileCopierOutputItem()
        {
            this.ErrorFiles = new List<CopierFileErrorInfor>();
        }
    }
}
