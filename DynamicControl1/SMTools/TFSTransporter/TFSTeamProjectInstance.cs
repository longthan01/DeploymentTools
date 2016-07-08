using Microsoft.TeamFoundation.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SMTools.Extensions;

namespace SMTools.TFSTransporter
{
    /// <summary>
    /// Hold an instance of TfsTeamProjectCollection
    /// </summary>
    public static class TFSInstance
    {
        private static string _ServerUrl = string.Empty;
        private static TfsTeamProjectCollection _TfsTeamProject;
        public static TfsTeamProjectCollection GetTfsTeamProjectCollection(string serverUrl, ICredentials credentials)
        {
            if (_TfsTeamProject == null || (!_ServerUrl.SuperEquals(serverUrl)))
            {
                _ServerUrl = serverUrl;
                Uri uri = new Uri(_ServerUrl);
                _TfsTeamProject = new TfsTeamProjectCollection(uri, credentials);
            }
            return _TfsTeamProject;
        }
    }
}
