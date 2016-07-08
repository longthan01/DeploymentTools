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

    public class TFSTransporterBase : ProcessBase
    {
        #region properties, fields
        private const string _USERNAME = "username";
        private const string _PASSWORD = "password";
        private const string _SERVERURL = "ServerUrl";
        private const string _DOMAIN = "domain";
        private const string _PORT = "port";
        private const string _NEED_AUTHENTICATE = "NeedAuthenticate";
        private const string _WORKSPACE_MAPPING = "ProjectPath";

        protected string _UserName;
        protected string _Password;
        protected string _TfsServerUrl;
        protected string _Domain;
        protected string _Port;
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

        public string WorkspaceMapping
        {
            get;
            set;
        }
        public TFSType Type
        {
            get;
            set;
        }
        #endregion

        #region constructor

        public TFSTransporterBase(string configFile)
            : base(configFile)
        {
            this._TfsServerUrl = this.GetConfigItemValue(_SERVERURL);
            this.WorkspaceMapping = this.GetConfigItemValue(_WORKSPACE_MAPPING);
            this._UserName = this.GetConfigItemValue(_USERNAME);
            this._Password = this.GetConfigItemValue(_PASSWORD);
            this._Domain = this.GetConfigItemValue(_DOMAIN);
            this._Port = this.GetConfigItemValue(_PORT);
            bool authenticate = this.GetConfigItemValue(_NEED_AUTHENTICATE) == "true";
            if (authenticate)
            {
                NetworkCredential cred = new NetworkCredential(this._UserName, this._Password, this._Domain);
                this.TeamProjectCollection = TFSInstance.GetTfsTeamProjectCollection(_TfsServerUrl,cred);
                if (!this.TeamProjectCollection.HasAuthenticated)
                {
                    this.TeamProjectCollection.Authenticate();
                }
            }
            else
            {
                this.TeamProjectCollection = new TfsTeamProjectCollection(new Uri(_TfsServerUrl));
            }
            this.VersionControlServer = (VersionControlServer)TeamProjectCollection.GetService(typeof(VersionControlServer));
        }
        #endregion

        public string GetProjectPath()
        {
            return this.GetConfigItemValue(_WORKSPACE_MAPPING);
        }

    }
}
