using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Utility
{
    public static class ProcessUtility
    {
        private const string _EXPLORER = "explorer.exe";
        public static void StartExplorer(string path)
        {
            Process.Start(_EXPLORER, path);
        }
    }
}
