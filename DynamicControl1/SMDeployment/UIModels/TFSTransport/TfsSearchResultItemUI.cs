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
    }
}
