using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Tfs.Searcher
{
    public interface ISearchFilter
    {
        bool IsMatch(Changeset changeset);
        DateTime GetFromDate();
        DateTime GetToDate();
    }
}
