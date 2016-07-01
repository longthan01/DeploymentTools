using SMTools.DeploymentBase;
using SMTools.Interfaces;
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
        private const string _PublishProfile = "/p:PublishProfile=";
        private const string _PublishUrl = "publishUrl";
        private const string _ProjectPath = "ProjectPath";
        private const string _LogFile = "/flp:LogFile=";

        public StringBuilder BuildCommand
        {
            get;
            set;
        }

        public MSBuilder(string configFile)
            : base(configFile)
        {
        }
        
        public string GetDeploymentPath()
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
            return this.GetConfigItem(_ProjectPath).Value;
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
