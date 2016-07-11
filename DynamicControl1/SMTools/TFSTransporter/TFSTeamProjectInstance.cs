using Microsoft.TeamFoundation.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SMTools.Extensions;

namespace SMTools.Tfs
{
    /// <summary>
    /// Hold an instance of TfsTeamProjectCollection
    /// </summary>
    public static class TfsInstance
    {
        private static string _ServerUrl = string.Empty;
        private static TfsTeamProjectCollection _TfsTeamProject;
        /// <summary>
        /// Get TfsTeamProjectCollection object based on Server url
        /// </summary>
        /// <param name="serverUrl">Tfs server url</param>
        /// <param name="credentials">User credential</param>
        /// <returns>If current connection url is the same as the previous connection url, return the same object TfsTeamProjectCollection
        /// otherwise return new TfsTeamProjectCollection with url and credential
        /// </returns>
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
