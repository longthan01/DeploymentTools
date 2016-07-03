using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Deployment.Utility
{
    public class FileInfor
    {
        public string FullPath
        {
            get;
            set;
        }
        public string ShortPath
        {
            get;
            set;
        }
        public DateTime ModifiedDate
        {
            get;
            set;
        }
    }
}
