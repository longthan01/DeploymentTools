using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDeployment.AppCodes
{
    public enum DestinationFolderType
    {
        MSv200,
        MSv200sj,
        CSv200,
        US_MSv100,
        US_MSv100sj,
        US_MSv100UAT,
        US_MSv100UATsj,
        US_CSv100,
        US_CSv100UAT,
        All
    }
    /// <summary>
    /// Config folder name which config file is located
    /// </summary>
    public enum ConfigFolder
    {
        MSBuild,
        FileCopier,
        TFS
    }
    /// <summary>
    /// Config folder name which config file is located
    /// </summary>
    public enum SharedConfigFolder
    {
        FileCopier
    }
    /// <summary>
    /// Config file name (xml)
    /// </summary>
    public enum ConfigFile
    {
        Dev,
        QA,
        US
    }
}
