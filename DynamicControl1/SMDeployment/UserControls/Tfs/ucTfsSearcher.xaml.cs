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
    public partial class ucTfsSearcher : UserControl
    {
        private TfsSearchConfigurator _SearchConfigurator;
        private TfsDownloadFileConfigurator _DownloadConfigurator;
        private List<TfsSearchOutputItem> _SearchOutputItems;
        public ucTfsSearcher()
        {
            InitializeComponent();
            this.grdProjectSelection.Children.Add(
                    UIHelper.CreateProjectSelectionGrid(
                        CollectionHelper.GetEnumList(typeof(Project)), OnProjectSelection));
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (_SearchConfigurator == null)
            {
                return;
            }
            imgLoading.Visibility = System.Windows.Visibility.Visible;
            var filter = this.ucSearchFilter.GetFilter();
            this._SearchConfigurator.Filter = filter;
            ProcessBuilder builder = new ProcessBuilder(new TfsSearcher(_SearchConfigurator));
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
                    imgLoading.Visibility = System.Windows.Visibility.Hidden;
                }));
            };
            builder.StartAsync();
        }

        private void OnProjectSelection(object sender, SelectionChangedEventArgs e)
        {
            var cbx = e.OriginalSource as ComboBox;
            var item = cbx.SelectedItem.ToString();
            _SearchConfigurator = ConfiguratorFactory
                .GetConfigurator<TfsSearchConfigurator>(AppCodes.Section.Tfs, item.ToProject());
            var grid = UIHelper.CreateRotateVerticalGrid(
                    new ProjectConfigInfor()
                    {
                        ServerUrl = _SearchConfigurator.GetServerUrl()
                    }
                );
            this.scrollViewerProjectInfor.Content = grid;
        }

        private void btnGetFiles_Click(object sender, RoutedEventArgs e)
        {
            if (_SearchOutputItems == null ||
                string.IsNullOrEmpty(this.txtDownloadOutputFolder.Text))
            {
                return;
            }
            imgLoading.Visibility = System.Windows.Visibility.Visible;
            _DownloadConfigurator = new TfsDownloadFileConfigurator()
            {
                OutputFolder = this.txtDownloadOutputFolder.Text,
                Files = _SearchOutputItems.Select(x => x.LocalPath).ToList()
            };
            ProcessBuilder builder = new ProcessBuilder(new TfsDownloadFile(_DownloadConfigurator));
            builder.OnProcessCompleted += (obj, ev) =>
            {
                UIThreadHelper.RunWorker(this, delegate
                {
                    imgLoading.Visibility = System.Windows.Visibility.Hidden;
                });
            };
            builder.StartAsync();
        }

        private void btnChooseFolder_Click(object sender, RoutedEventArgs e)
        {
            this.txtDownloadOutputFolder.Text = UIHelper.OpenChooseFolderDialog();
        }
    }
}
