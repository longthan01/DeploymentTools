﻿using SMTools.DeploymentBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.DeploymentBase
{
    public interface IDeployment
    {
        void ApplyConfiguration();
        void Run();
        StepOutputBase GetOutput();
    }
}
