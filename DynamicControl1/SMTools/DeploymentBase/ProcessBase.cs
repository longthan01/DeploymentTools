using System;
using System.IO;
using System.Xml;

namespace SMTools.Deployment.Base
{
    public abstract class ProcessBase : IDeployProcess
    {
        #region properties, fields
        /// <summary>
        ///  Extension configuration
        /// </summary>
        public IDeployConfigurator Configurator { get; set; }

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

        #region public methods
        /// <summary>
        /// Manually load configuration
        /// </summary>
        /// <summary>
        /// Save configuration back to xml
        /// </summary>
        public void SaveConfiguration()
        {
            Configurator.SaveConfiguration(this);
        }
        #endregion

        #region IDeployment members

        public virtual void ApplyConfiguration()
        {
            throw new NotImplementedException();
        }

        public virtual void Run()
        {
            throw new NotImplementedException();
        }

        public virtual DeployOutputBase GetOutput()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
