using Microsoft.TeamFoundation.VersionControl.Client;
using SMTools.Deployment.Base;
using SMTools.TFSTransporter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Tfs.Searcher
{
    public class TfsSearcher : TfsTransporter
    {
        public ISearchFilter Filter
        {
            get;
            set;
        }
       
        public TfsSearcher(IDeployConfigurator configurator)
            : base(configurator)
        {

        }
        public TfsSearcher(ISearchFilter filter, IDeployConfigurator configurator)
            : base(configurator)
        {
            Filter = filter;
        }
        private VersionSpec ParseVersionSpec(DateTime date)
        {
            try
            {
                string format = string.Format("D{0:yyy}-{0:MM}-{0:dd}T{0:HH}:{0:mm}", date);
                var res = VersionSpec.ParseSingleSpec(format, "");
                return res;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private IEnumerable<Changeset> GetHistory(Workspace wp)
        {
            var path = wp.GetServerItemForLocalItem(Path.GetDirectoryName(TfsInfor.WorkspaceMapping));
            var history = this.VersionControlServer.QueryHistory(
                path,
                VersionSpec.Latest,
                0,
                RecursionType.Full,
                null,
                ParseVersionSpec(Filter.GetFromDate()),
                ParseVersionSpec(Filter.GetToDate()),
                int.MaxValue,
                true,
                false,
                true);
            return history.Cast<Changeset>();
        }
        #region IDeployment Members

        public override void Run()
        {
            TfsSearchOutput Result = new TfsSearchOutput();
            if (this.Filter == null)
                throw new ArgumentNullException("Filter", "Filter is null, cannot query TFS history");
            Workspace wpInfo = VersionControlServer.GetWorkspace(TfsInfor.WorkspaceMapping);
            var history = GetHistory(wpInfo);
            foreach (Changeset cs in history)
            {
                if (Filter.IsMatch(cs))
                {
                    Result.AddIfNotExists(TfsSearchOutputItem.Parse(cs, wpInfo));
                }
            }
            ProcessOutput = Result;
        }

        #endregion
    }
}
