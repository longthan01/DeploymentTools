using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.MSBuilder
{
    public class MSBuilder : DeploymentProcessBase, IDeployment
    {
        protected const string _PublishProfile = "/p:PublishProfile=";
        protected const string _PublishUrl = "publishUrl";
        protected const string _ProjectPath = "ProjectPath";
        protected const string _LogFile = "/flp:LogFile=";
        protected const string _SolutionPath = "SolutionPath";

        public StringBuilder BuildCommand
        {
            get;
            set;
        }

        public MSBuilder()
        {
        }

        public MSBuilder(string configFile)
            : base(configFile)
        {
        }
        
        public string GetDeploymentOutputFolder()
        {
            ConfigItem item = this.GetConfigItem(_PublishProfile);
            string res = string.Empty;
            if (item != null)
            {
                res = XmlLoader.GetValueIteration(_PublishUrl, item.Value.Trim('\"'));
            }
            return res;
        }

        public string GetProjectPath()
        {
            return this.GetConfigItem(_ProjectPath).Value.Trim('\"');
        }

        public string GetSolutionPath()
        {
            return this.GetConfigItem(_SolutionPath).Value.Trim('\"');
        }
        #region IDeployment Members
        public void ApplyConfiguration()
        {
            this.BuildCommand = new StringBuilder();
            foreach (ConfigItem item in this.ConfigurationItems)
            {
                this.BuildCommand.Append(item.Name);
                this.BuildCommand.Append(item.Value + " ");
            }
            // remove "ProjectPath" in command line
            var prj = GetConfigItemValue(_ProjectPath);
            if (prj != null)
            {
                BuildCommand.Replace(_ProjectPath, string.Empty);
            }
            // remove "SolutionPath" in command line
            var sln = GetConfigItemValue(_SolutionPath);
            if (sln != null)
            {
                BuildCommand.Replace(_SolutionPath, string.Empty);
            }
        }

        public void Run()
        {
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "cmd.exe";
            psi.Arguments = this.BuildCommand.ToString();
            p.StartInfo = psi;
            p.Start();
            p.WaitForExit();
        }

        public StepOutputBase GetOutput()
        {
            string path = this.GetConfigItemValue(_LogFile);
            BuildOutput o = new BuildOutput();
            StreamReader stream = new StreamReader(path);
            o.BuildOutMessage = stream.ReadToEnd();
            stream.Close();
            return o;
        }

        #endregion
    }
}
