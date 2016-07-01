using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Deployment.Utility
{
    public static class DeploymentUtility
    {
        /// <summary>
        /// Get basic information of a file
        /// </summary>
        /// <param name="file">Full path to file's location</param>
        /// <returns>An object <see cref="FileDirInfor"/></returns>
        public static FileDirInfor GetInfor(string file)
        {
            DateTime modifedDate = File.GetLastWriteTime(file);
            // get short path
            string[] tks = file.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            string shortPath = tks.Length == 0 ? string.Empty : tks[tks.Length - 2] + "\\" + tks[tks.Length - 1];
            FileDirInfor fi = new FileDirInfor()
            {
                FullPath = file,
                ShortPath = shortPath,
                ModifiedDate = modifedDate,
                IsEmptyDirectory = false
            };
            return fi;
        }

        public static List<FileDirInfor> GetFileDirInfor(string folder)
        {
            List<FileDirInfor> res = new List<FileDirInfor>();
            Stack<string> dirs = new Stack<string>();
            dirs.Push(folder);
            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                string[] files = null;
                try
                {
                    files = Directory.GetFiles(currentDir);
                    if (files.Length != 0)
                    {
                        foreach (string f in files)
                        {
                            res.Add(GetInfor(f));
                        }
                    }
                    else
                    {
                        res.Add(new FileDirInfor()
                        {
                            FullPath = currentDir,
                            ModifiedDate = Directory.GetLastWriteTime(currentDir),
                            IsEmptyDirectory = true
                        });
                    }
                    string[] directories = Directory.GetDirectories(currentDir);
                    foreach (string dir in directories)
                    {
                        dirs.Push(dir);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return res;
        }
   
        public static List<string> GetAllFiles(string folder)
        {
            return GetFileDirInfor(folder).Select(x => x.FullPath).ToList();
        }
    }
}
