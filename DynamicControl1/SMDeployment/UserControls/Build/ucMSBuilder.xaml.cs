using SMDeployment.AppCodes;
using SMDeployment.UIModels.BuildDeploy;
using SMTools.Build.Base;
using SMTools.Deployment.Base;
using SMTools.Utility;
using System.Windows;
using System.Windows.Controls;

namespace SMDeployment.UserControls.Build
{
    /// <summary>
    /// Interaction logic for ucMSBuilder.xaml
    /// </summary>
    public partial class ucMSBuilder : UserControl
    {
        private BuildDeployConfigurator _Configurator;

        public ucMSBuilder()
        {
            InitializeComponent();
            this.grdProjectSelection.Children.Add(
                    UIHelper.CreateProjectSelectionGrid(
                        CollectionHelper.GetEnumList(typeof(Project)), OnProjectSelection));
                            
        }
        private void OnProjectSelection(object sender, SelectionChangedEventArgs e)
        {
            var cbx = e.OriginalSource as ComboBox;
            var item = cbx.SelectedItem.ToString();
            _Configurator = ConfiguratorFactory
                .GetConfigurator<BuildDeployConfigurator>(AppCodes.Section.Build, item.ToProject());
            var grid = UIHelper.CreateRotateVerticalGrid(
                    new BuildDeployConfigInfor()
                    {
                    }
                );
            this.scrollViewerProjectInfor.Content = grid;
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            if (_Configurator == null)
            {
                return;
            }
            imgLoading.Visibility = System.Windows.Visibility.Visible;
            ProcessBuilder builder = new ProcessBuilder(new SMTools.Build.Build.Builder(_Configurator));
            builder.OnProcessCompleted += (obj, ev) =>
            {
                ProcessUtility.StartExplorer(((BuildDeployOutput)ev.ProcessOutput).LogFile);
                UIThreadHelper.RunWorker(this, delegate
                {
                    imgLoading.Visibility = Visibility.Hidden;
                });
            };
            builder.StartAsync();
        }
    }
}
