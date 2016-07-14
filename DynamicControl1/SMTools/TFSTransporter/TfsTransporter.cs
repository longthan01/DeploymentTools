using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using SMTools.Deployment.Base;
using SMTools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Tfs
{
    public abstract class TfsTransporter : ProcessBase
    {
        #region properties, fields

        public TfsInfor TfsInfor
        {
            get;
            set;
        }

        protected TfsTeamProjectCollection TeamProjectCollection
        {
            get;
            set;
        }
        protected VersionControlServer VersionControlServer
        {
            get;
            set;
        }
        protected Workspace CurrentWorkspace
        {
            get;
            set;
        }
        #endregion

        #region constructor
        public TfsTransporter(IDeployConfigurator configurator)
            : base(configurator)
        {
            this.Configurator = configurator;
        }
        public virtual void ApplyConfiguration()
        {
            if (Configurator != null)
            {
                Configurator.ApplyConfig(this);
            }
            TfsInfor = new Models.TfsInfor();
            if (TfsInfor.NeedAuthenticate)
            {
                NetworkCredential cred = new NetworkCredential(TfsInfor.UserName, TfsInfor.Password, TfsInfor.Domain);
                TeamProjectCollection = TfsInstance.GetTfsTeamProjectCollection(TfsInfor.ServerUrl, cred);
                if (!this.TeamProjectCollection.HasAuthenticated)
                {
                    TeamProjectCollection.Authenticate();
                }
            }
            else
            {
                TeamProjectCollection = new TfsTeamProjectCollection(new Uri(TfsInfor.ServerUrl));
            }
            VersionControlServer = (VersionControlServer)TeamProjectCollection.GetService(typeof(VersionControlServer));
            CurrentWorkspace = VersionControlServer.GetWorkspace(TfsInfor.WorkspaceMapping);
        }
        #endregion
    }
}
