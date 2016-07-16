using SMDeployment.AppCodes;
using SMDeployment.UIModels.BuildDeploy;
using SMTools.Build;
using SMTools.Build.Base;
using SMTools.Deployment.Base;
using System.Windows;
using System.Windows.Controls;

namespace SMDeployment.UserControls.Build
{
    /// <summary>
    /// Interaction logic for ucDeploy.xaml
    /// </summary>
    public partial class ucDeploy : UserControlBase
    {
        private DeployConfigurator _Configurator;

        public ucDeploy()
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

        private void OnProjectSelection(object sender, SelectionChangedEventArgs e)
        {
            var cbx = e.OriginalSource as ComboBox;
            var item = cbx.SelectedItem.ToString();
            _Configurator = ConfiguratorFactory.GetConfigurator<DeployConfigurator>()
                .SetBuildPath(SessionManager.PathCollection[item.ToStringWithProject()]) as DeployConfigurator;
            var grid = UIHelper.CreateRotateVerticalGrid(
                    new DeployConfigInfor()
                    {
                        ProjectPath = _Configurator.GetProjectPath(),
                        DeploymentOutputFolder = _Configurator.GetDeployOutputFolder(),
                        LogFile = _Configurator.GetLogFile()
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
           var builder = this.CreateBuilder(new BuildDeployProcess(_Configurator));
            builder.OnProcessCompleted += (obj, ev) =>
            {
                UIThreadHelper.RunWorker(this, delegate
                {
                    this.grdOutput.Children.Add(
                            UIHelper.CreateScroll(
                                new TextBlock()
                                {
                                    Text = ((BuildDeployOutput)ev.ProcessOutput).BuildOutMessage
                                }));
                });
            };
            builder.StartAsync();
        }
    }
}
