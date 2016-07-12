using Microsoft.TeamFoundation.VersionControl.Client;
using SMTools.Deployment.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using SMTools.Extensions;
namespace SMTools.Tfs.Searcher
{
    public class TfsSearchOutput : DeployOutputBase
    {
        public List<TfsSearchOutputItem> Items
        {
            get;
            set;
        }
        public TfsSearchOutput()
        {
            this.Items = new List<TfsSearchOutputItem>();
        }
        public void AddIfNotExists(List<TfsSearchOutputItem> items)
        {
            foreach (var item in items)
            {
                if (!Items.Any(x => x.LocalPath.SuperEquals(item.LocalPath)))
                {
                    this.Items.Add(item);
                }
            }
        }
    }
    public class TfsSearchOutputItem
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
        public TfsSearchOutputItem()
        {
        }
        /// <summary>
        /// Parse all item appeard in changeset to a List of TfsSearchOutputItem
        /// </summary>
        /// <param name="changeset">Source control changeset</param>
        /// <param name="wpInfor">Current workspace</param>
        /// <returns>A List of TfsSearchOutputItem</returns>
        public static List<TfsSearchOutputItem> Parse(Changeset changeset, Workspace wpInfor)
        {
            List<TfsSearchOutputItem> res = new List<TfsSearchOutputItem>();
            foreach (Change change in changeset.Changes)
            {
                TfsSearchOutputItem result = new TfsSearchOutputItem();
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
