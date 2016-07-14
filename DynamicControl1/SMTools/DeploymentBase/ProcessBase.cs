using System;
using System.IO;
using System.Xml;

namespace SMTools.Deployment.Base
{
    public abstract class ProcessBase : IDeployProcess
    {
        #region properties, fields
        /// <summary>
        ///  External configuration
        /// </summary>
        public IDeployConfigurator Configurator { get; set; }

        protected ProcessOutputBase ProcessOutput
        {
            get;
            set;
        }
        #endregion

        #region constructors
        public ProcessBase(IDeployConfigurator configurator)
        {
            this.Configurator = configurator;
        }

        public ProcessBase()
        {
        }
        #endregion

        #region IDeployment members

        public virtual void ApplyConfiguration()
        {
            if (this.Configurator != null)
            {
                this.Configurator.ApplyConfig(this);
            }
        }

        public virtual void Run()
        {
            throw new NotImplementedException();
        }

        public virtual ProcessOutputBase GetOutput()
        {
            return ProcessOutput;
        }
        #endregion
    }
}
