using SMTools.Build.Base;
using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Boarding
{
    public class XsdToClassGenerator : CommandLineProcess
    {
        public string XsdFile { get; set; }
        public XsdToClassGenerator(IDeployConfigurator configurator) : base(configurator) { }
        public override ProcessOutputBase GetOutput()
        {
            string fileName = Path.GetFileNameWithoutExtension(XsdFile);
            string xsdFolder = Path.GetDirectoryName(XsdFile);
            string csFile = Path.Combine(xsdFolder, fileName + ".cs");
            if (File.Exists(csFile))
            {
                this.ProcessOutput.Message = "Success";
            }
            else
            {
                this.ProcessOutput.Message = "Failed";
            }
            return this.ProcessOutput;
        }
    }
}
