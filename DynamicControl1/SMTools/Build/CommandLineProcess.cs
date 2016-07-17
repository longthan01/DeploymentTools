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
    public class CommandLineProcess : ProcessBase
    {
        public string LogFile { get; set; }

        public StringBuilder Command
        {
            get;
            set;
        }

        public CommandLineProcess(IDeployConfigurator configurator) : base(configurator)
        {
        }

        public override void ConstructProperty()
        {
            Command = new StringBuilder();
        }

        public override void Run()
        {
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "cmd.exe";
            psi.Arguments = this.Command.ToString();
            p.StartInfo = psi;
            p.Start();
            p.WaitForExit();
        }

        public override ProcessOutputBase GetOutput()
        {
            if (string.IsNullOrEmpty(LogFile) || !File.Exists(LogFile))
            {
                this.ThrowException("Command is failed: " + Environment.NewLine + this.Command.ToString());
            }
            BuildDeployOutput o = new BuildDeployOutput(LogFile);
            StreamReader stream = new StreamReader(LogFile);
            o.BuildOutMessage = stream.ReadToEnd();
            stream.Close();
            return o;
        }
    }
}
