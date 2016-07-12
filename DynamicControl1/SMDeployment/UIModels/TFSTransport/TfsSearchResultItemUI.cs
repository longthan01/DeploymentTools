using SMTools.Tfs.Searcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDeployment.UIModels.TfsTransport
{
    public class TfsSearchResultItemUI : TfsSearchOutputItem
    {
        public bool Selected
        {
            get;
            set;
        }
        public TfsSearchResultItemUI()
        {
            Selected = true;
        }
        public TfsSearchResultItemUI(TfsSearchOutputItem parent)
        {
            this.ChangeSetId = parent.ChangeSetId;
            this.ChangeType = parent.ChangeType;
            this.CheckInDate = parent.CheckInDate;
            this.Comment = parent.Comment;
            this.Commiter = parent.Commiter;
            this.LocalPath = parent.LocalPath;
            this.ServerPath = parent.ServerPath;
            this.Selected = true;
        }
    }
}
