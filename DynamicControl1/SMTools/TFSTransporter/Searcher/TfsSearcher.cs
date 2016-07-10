using Microsoft.TeamFoundation.VersionControl.Client;
using SMTools.Deployment.Base;
using SMTools.DeploymentBase.TFSTransporter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTools.Tfs.Searcher
{
    public class TfsSearcher : TfsTransporter, IDeployment
    {
        public ISearchFilter Filter
        {
            get;
            set;
        }

        private TfsSearchOutput _Result;
       
        public TfsSearcher(IDeployConfigurator configurator)
            : base(configurator)
        {

        }
        public TfsSearcher(ISearchFilter filter, IDeployConfigurator configurator)
            : base(configurator)
        {
            this.Filter = filter;
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
            var path = wp.GetServerItemForLocalItem(Path.GetDirectoryName(this.WorkspaceMapping));
            var history = this.VersionControlServer.QueryHistory(
                path,
                VersionSpec.Latest,
                0,
                RecursionType.Full,
                null, null, null,
                //ParseVersionSpec(Filter.GetFromDate()),
                //ParseVersionSpec(Filter.GetToDate()),
                int.MaxValue,
                true,
                false,
                true);
            return history.Cast<Changeset>();
        }
        #region IDeployment Members

        public override void ApplyConfiguration()
        {
            _Result = new TfsSearchOutput();
        }

        public override void Run()
        {
            Workspace wpInfo = VersionControlServer.GetWorkspace(WorkspaceMapping);
            var history = GetHistory(wpInfo);
            foreach (Changeset cs in history)
            {
                if (Filter.IsMatch(cs))
                {
                    _Result.Items.AddRange(SearchItemOutput.Parse(cs, wpInfo));
                }
            }
        }

        public override DeployOutputBase GetOutput()
        {
            return _Result;
        }

        #endregion
    }
}
