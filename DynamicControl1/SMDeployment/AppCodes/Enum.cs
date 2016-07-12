using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDeployment.AppCodes
{
    public enum Section
    {
        Build,
        Deploy,
        Tfs,
        FileCopier
    }
    public enum Project
    {
        Boarding_Dev,
        Boarding_QA,
        Boarding_US,
        FDC_Dev,
        FDC_QA, 
        FDC_US
    }
}
