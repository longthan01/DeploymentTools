using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Deployment.Utility
{
    /// <summary>
    /// Directory information
    /// </summary>
    public class DirInfor
    {
        public string Current { get; set; }
        public bool IsDirectory { get; set; }
        public List<string> Files { get; set; }
        public List<DirInfor> SubDirectories { get; set; }
        public DirInfor()
        {
            Files = new List<string>();
            SubDirectories = new List<DirInfor>();
        }
    }
}
