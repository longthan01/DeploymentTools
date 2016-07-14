using SMTools.TFSTransporter;
using SMTools.Utility;

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
