using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Build.Base
{
    public class BuildDeployProcess : ProcessBase
    {
        public string LogFile { get; set; }

        public string ProjectPath { get; set; }

        public StringBuilder BuildCommand
        {
            get;
            set;
        }

        public BuildDeployProcess() : base()
        {
        }

        public BuildDeployProcess(IDeployConfigurator configurator) : base (configurator) { }

        public override void ApplyConfiguration()
        {
            if (this.Configurator != null)
            {
                Configurator.ApplyConfig(this);
            }
        }

        public override void Run()
        {
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "cmd.exe";
            psi.Arguments = this.BuildCommand.ToString();
            p.StartInfo = psi;
            p.Start();
            p.WaitForExit();
        }

        public override DeployOutputBase GetOutput()
        {
            BuildDeployOutput o = new BuildDeployOutput();
            StreamReader stream = new StreamReader(LogFile);
            o.BuildOutMessage = stream.ReadToEnd();
            stream.Close();
            return o;
        }
    }
}
