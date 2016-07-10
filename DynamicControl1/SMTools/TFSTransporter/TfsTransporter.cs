using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.TFSTransporter
{
    public enum TFSType
    {
        Unknown,
        Checkout,
        Checkin,
        GetLastest
    }

    public abstract class TfsTransporter : ProcessBase
    {
        #region properties, fields

        public string UserName { get; set; }
        public string Password { get; set; }
        public string ServerUrl { get; set; }
        public string Domain { get; set; }
        public string WorkspaceMapping
        {
            get;
            set;
        }
        public bool NeedAuthenticate { get; set; }
        public TFSType Type
        {
            get;
            set;
        }
        public TfsTeamProjectCollection TeamProjectCollection
        {
            get;
            set;
        }
        public VersionControlServer VersionControlServer
        {
            get;
            set;
        }


        #endregion

        #region constructor
        public TfsTransporter() { }
        public TfsTransporter(IDeployConfigurator configurator)
            : base(configurator)
        {
            Configurator.ApplyConfig(this);
            if (NeedAuthenticate)
            {
                NetworkCredential cred = new NetworkCredential(this.UserName, this.Password, this.Domain);
                TeamProjectCollection = TfsInstance.GetTfsTeamProjectCollection(ServerUrl, cred);
                if (!this.TeamProjectCollection.HasAuthenticated)
                {
                    TeamProjectCollection.Authenticate();
                }
            }
            else
            {
                this.TeamProjectCollection = new TfsTeamProjectCollection(new Uri(ServerUrl));
            }
            VersionControlServer = (VersionControlServer)TeamProjectCollection.GetService(typeof(VersionControlServer));
        }
        #endregion
    }
}
