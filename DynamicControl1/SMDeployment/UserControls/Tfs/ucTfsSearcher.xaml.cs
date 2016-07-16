using SMDeployment.AppCodes;
using SMDeployment.UIModels.TfsTransport;
using SMTools.Deployment.Base;
using SMTools.Tfs.Searcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SMTools.TFSTransporter;
using SMTools.Tfs.DownloadFile;
using SMTools.Utility;

namespace SMDeployment.UserControls.Tfs
{
    /// <summary>
    /// Interaction logic for ucTfsSearcher.xaml
    /// </summary>
    public partial class ucTfsSearcher : UserControlBase
    {
        private TfsSearchConfigurator _SearchConfigurator;
        private TfsDownloadFileConfigurator _DownloadConfigurator;
        private List<TfsSearchOutputItem> _SearchOutputItems;

        public ucTfsSearcher()
        {
            InitializeComponent();
            this.grdProjectSelection.Children.Add(
                   UIHelper.CreateSelectionGrid(
                       "Select project:",
                       CollectionHelper.GetEnumList(typeof(ProjectPath)),
                       null,
                       null,
                       OnProjectSelection));
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (_SearchConfigurator == null)
            {
                this.Log("Must choose 1 project first.");
                return;
            }
            var filter = this.ucSearchFilter.GetFilter();
            _SearchConfigurator.Filter = filter;
            var builder = this.CreateBuilder(new TfsSearcher(_SearchConfigurator));
            builder.OnProcessCompleted += (obj, ev) =>
            {
                var output = ev.ProcessOutput as TfsSearchOutput;
                _SearchOutputItems = output.Items;
                UIThreadHelper.RunWorker(this, new Action(() =>
                {
                    UIHelper.AddChild(this.grdOutput.Children,
                            UIHelper.CreateScrollDataGrid(
                              output.Items.Convert<TfsSearchOutputItem, TfsSearchResultItemUI>()
                            ));
                }));
            };
            builder.StartAsync();
        }

        private void OnProjectSelection(object sender, SelectionChangedEventArgs e)
        {
            var cbx = e.OriginalSource as ComboBox;
            var item = cbx.SelectedItem.ToStringWithSolution();
            _SearchConfigurator = ConfiguratorFactory
                .GetConfigurator<TfsSearchConfigurator>()
                .SetWorkspaceMapping(SessionManager.PathCollection[item])
                as TfsSearchConfigurator;

            var grid = UIHelper.CreateRotateVerticalGrid(
                    new ProjectConfigInfor()
                    {
                        ServerUrl = _SearchConfigurator.GetServerUrl(),
                        WorkspaceMapping = _SearchConfigurator.GetWorkspaceMapping()
                    }
                );
            this.scrollViewerProjectInfor.Content = grid;
        }

        private void btnGetFiles_Click(object sender, RoutedEventArgs e)
        {
            _DownloadConfigurator = ConfiguratorFactory.GetConfigurator<TfsDownloadFileConfigurator>()
                .SetOutputFolder(this.txtDownloadOutputFolder.Text)
                .SetFileToDownload(_SearchOutputItems);
            this.CreateBuilder(new TfsDownloadFile(_DownloadConfigurator)).StartAsync();
        }

        private void btnChooseFolder_Click(object sender, RoutedEventArgs e)
        {
            this.txtDownloadOutputFolder.Text = UIHelper.OpenChooseFolderDialog();
        }
    }
}
