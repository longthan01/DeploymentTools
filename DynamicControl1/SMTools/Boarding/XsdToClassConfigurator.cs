using SMTools.Deployment.Configurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTools.Deployment.Base;
using SMTools.Utility;
using SMTools.Extensions;
using System.IO;

namespace SMTools.Boarding
{
    public class XsdToClassConfigurator : ConfiguratorBase
    {
        protected string XsdFile { get; set; }
        public XsdToClassConfigurator(ConfigItemCollection configItems) : base(configItems) { }
        public override void ApplyConfig(ProcessBase process)
        {
            base.ApplyConfig(process);
            this.XsdFile = ConfigItems[ConstantString.BOARDING_XSD_LOCATION];
            var proc = process as XsdToClassGenerator;
            foreach (var item in this.ConfigItems)
            {
                if (item.GetName().SuperEquals(ConstantString.BOARDING_XSD_LOCATION))
                {
                    proc.Command
                    .Append(item.GetValue())
                    .Append(" ");
                }
                else {
                    proc.Command
                        .Append(item.GetName())
                        .Append(item.GetValue())
                        .Append(" ");
                }
            }
            // remove old .cs file
            if (!File.Exists(XsdFile))
                this.ThrowException("Could not found xsd file");
            string xsdFile = Path.GetFileNameWithoutExtension(XsdFile);
            string xsdFolder = Path.GetDirectoryName(XsdFile);
            string csFile = Path.Combine(xsdFolder, xsdFile + ".cs");
            if (File.Exists(csFile))
            {
               // File.Delete(csFile);
            }
            proc.XsdFile = this.XsdFile;
        }
        public override void SaveConfiguration(ProcessBase process)
        {
            base.SaveConfiguration(process);
        }
    }
}
