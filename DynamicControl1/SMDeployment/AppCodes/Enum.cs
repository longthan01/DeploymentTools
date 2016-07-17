using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDeployment.AppCodes
{
    public enum Tfs
    {
        Tfs_US,
        Tfs_VN
    }
    public enum XmlConfigSection
    {
        Build,
        Deploy,
        Tfs_US,
        Tfs_VN,
        FileCopyBoarding,
        FileCopyFDC
    }
    public enum ProjectPath
    {
        Boarding_Dev,
        Boarding_QA,
        Boarding_US,
        FDC_Dev,
        FDC_QA, 
        FDC_US
    }
    public enum BoardingModule
    {
        FACS,
        FDS
    }
}
