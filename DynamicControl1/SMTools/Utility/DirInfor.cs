using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Utility
{
    /// <summary>
    /// Directory information
    /// </summary>
    public class DirInfor
    {
        public string RelativeRoot { get; set; }
        public DateTime ModifiedDate
        {
            get;
            set;
        }
        public List<FileInfor> Files { get; set; }
        public List<DirInfor> SubDirectories { get; set; }
        public DirInfor()
        {
            Files = new List<FileInfor>();
            SubDirectories = new List<DirInfor>();
        }
    }
}
