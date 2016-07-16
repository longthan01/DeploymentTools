using SMTools.TFSTransporter;
using SMTools.Tfs.Searcher;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTools.Utility;
using SMTools.Deployment.Base;
using SMTools.Models;

namespace SMTools.Tfs.Searcher
{
    public class TfsSearchConfigurator : TfsConfigurator
    {
        public TfsSearchConfigurator(ConfigItemCollection configItems) : base(configItems)
        {
        }

        public ISearchFilter Filter
        {
            get;
            set;
        }

        public TfsSearchConfigurator SetFilter(ISearchFilter filter)
        {
            this.Filter = filter;
            return this;
        }

        public override void ApplyConfig(Deployment.Base.ProcessBase process)
        {
            base.ApplyConfig(process);
            var searcher = process as TfsSearcher;
            searcher.Filter = this.Filter;
        }
    }
}
