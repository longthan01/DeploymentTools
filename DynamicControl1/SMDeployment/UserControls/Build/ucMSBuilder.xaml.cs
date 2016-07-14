using SMDeployment.AppCodes;
using SMDeployment.UIModels.BuildDeploy;
using SMTools.Build;
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
        private BuildConfigurator _Configurator;

        public ucMSBuilder()
        {
            InitializeComponent();
            this.grdProjectSelection.Children.Add(
                    UIHelper.CreateProjectSelectionGrid(
                        CollectionHelper.GetEnumList(typeof(ProjectPath)), OnProjectSelection));
                            
        }
        private void OnProjectSelection(object sender, SelectionChangedEventArgs e)
        {
            var cbx = e.OriginalSource as ComboBox;
            var item = cbx.SelectedItem.ToString();
            
            _Configurator = ConfiguratorFactory
                .GetConfigurator<BuildConfigurator>(XmlConfigSection.Build);
            var grid = UIHelper.CreateRotateVerticalGrid(
                    new BuildConfigInfor()
                    {
                        SolutionPath = _Configurator.GetSolutionPath()
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
            ProcessBuilder builder = new ProcessBuilder(new BuildDeployProcess(_Configurator));
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
