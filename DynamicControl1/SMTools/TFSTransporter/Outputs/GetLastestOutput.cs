﻿using Microsoft.TeamFoundation.VersionControl.Client;
using SMTools.DeploymentBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.TFSTransporter
{
    public class GetLastestOutput : StepOutputBase
    {
        public List<Failure> Failures
        {
            get;
            set;
        }
        public GetLastestOutput()
        {
            Failures = new List<Failure>();
        }
    }
}