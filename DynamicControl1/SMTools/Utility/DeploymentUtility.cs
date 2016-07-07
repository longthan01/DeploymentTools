using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Deployment.Utility
{
    /// <summary>
    /// Utility class, provide methods to get File, Dir information...
    /// </summary>
    public static class DeploymentUtility
    {
        ///// <summary>
        ///// Get basic information of a file
        ///// </summary>
        ///// <param name="file">Full path to file's location</param>
        ///// <returns>An object <see cref="FileDirInfor"/></returns>
        public static FileInfor GetFileInfor(string file)
        {
            DateTime modifedDate = File.GetLastWriteTime(file);
            // get short path
            string[] tks = file.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            string shortPath = tks.Length == 0 ? string.Empty : tks[tks.Length - 2] + "\\" + tks[tks.Length - 1];
            FileInfor fi = new FileInfor()
            {
                FileName = Path.GetFileName(file),
                FullPath = file,
                ShortPath = shortPath,
                ModifiedDate = modifedDate,
            };
            return fi;
        }

        public static DirInfor GetDirInfor(string folder)
        {
            DirInfor res = new DirInfor();
            res.RelativeRoot = folder;
            try
            {
                res.ModifiedDate = Directory.GetLastWriteTime(folder);
                string[] files = Directory.GetFiles(res.RelativeRoot);
                string[] directories = Directory.GetDirectories(res.RelativeRoot);
                foreach (string file in files)
                {
                    res.Files.Add(GetFileInfor(file));
                }
                foreach (string dir in directories)
                {
                    res.SubDirectories.Add(GetDirInfor(dir));
                }
            }
            catch (Exception)
            {

            }
            return res;
        }

        public static string[] GetAllFiles(string folder)
        {
            return Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories);
        }
    }
}
