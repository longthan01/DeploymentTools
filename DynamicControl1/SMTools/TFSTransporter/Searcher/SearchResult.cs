using Microsoft.TeamFoundation.VersionControl.Client;
using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Tfs.Searcher
{
    public class TfsSearchOutput : DeployOutputBase
    {
        public List<SearchItemOutput> Items
        {
            get;
            set;
        }
        public TfsSearchOutput()
        {
            this.Items = new List<SearchItemOutput>();
        }
    }
    public class SearchItemOutput
    {
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
        public DateTime CheckInDate
        {
            get;
            set;
        }
        public ChangeType ChangeType
        {
            get;
            set;
        }
        public int ChangeSetId
        {
            get;
            set;
        }
        public string LocalPath
        {
            get;
            set;
        }
        public string ServerPath
        {
            get;
            set;
        }
        public static List<SearchItemOutput> Parse(Changeset changeset, Workspace wpInfor)
        {
            List<SearchItemOutput> res = new List<SearchItemOutput>();
            foreach (Change change in changeset.Changes)
            {
                SearchItemOutput result = new SearchItemOutput();
                result.ChangeSetId = changeset.ChangesetId;
                result.Commiter = changeset.Committer;
                result.Comment = changeset.Comment;
                result.ChangeType = change.ChangeType;
                result.ServerPath = change.Item.ServerItem;
                result.LocalPath = wpInfor.GetLocalItemForServerItem(change.Item.ServerItem);
                result.CheckInDate = change.Item.CheckinDate;
                res.Add(result);
            }
            return res;
        }
    }
}
