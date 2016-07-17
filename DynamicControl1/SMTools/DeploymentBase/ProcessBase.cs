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

        protected virtual void ThrowException(string message)
        {
            throw new Exception(this.GetType().Name + " - " + message);
        }

        #region constructors
        public ProcessBase(IDeployConfigurator configurator)
        {
            this.Configurator = configurator;
            this.ConstructProperty();
        }

        public ProcessBase()
        {
            this.ConstructProperty();
        }
        #endregion

        #region IDeployment members
        public virtual void ConstructProperty()
        {
            ProcessOutput = new ProcessOutputBase();
        }
        public virtual void ApplyConfiguration()
        {
            if (this.Configurator != null)
            {
                this.Configurator.ApplyConfig(this);
            }
        }
        public abstract void Run();
        public virtual ProcessOutputBase GetOutput()
        {
            this.ProcessOutput.Message = DateTime.Now.ToString() + " : " + this.GetType() + " completed"; 
            return this.ProcessOutput;
        }
        
        #endregion
    }
}
