using SMTools.TFSTransporter.Searcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTools.Extensions;

namespace SMDeployment.AppCodes.TFS
{
    public class SearchFilter : ISearchFilter
    {
        public DateTime FromDate
        {
            get;
            set;
        }
        public DateTime ToDate
        {
            get;
            set;
        }
        public string Commiter
        {
            get;
            set;
        }
        public string Comment
        {
            get;
            set;
        }
        #region ISearchFilter Members

        public bool IsMatch(Microsoft.TeamFoundation.VersionControl.Client.Changeset changeset)
        {
            bool isMatch = changeset.Comment.IndexOf(Comment) >= 0 &&
                changeset.Committer.Contains(Commiter);
            return isMatch;
        }

        public DateTime GetFromDate()
        {
            return this.FromDate;
        }

        public DateTime GetToDate()
        {
            return this.ToDate;
        }

        #endregion
    }
}
