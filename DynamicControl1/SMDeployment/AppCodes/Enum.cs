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

    public enum ConfigFolder
    {
        MSBuild,
        FileCopier,
        TFS
    }
    public enum SharedConfigFolder
    {
        FileCopier
    }

    public enum DeployEnvironment
    {
        Dev,
        QA,
        US
    }
}
