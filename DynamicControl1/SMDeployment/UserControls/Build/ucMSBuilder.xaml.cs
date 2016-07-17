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
    public partial class ucMSBuilder : UserControlBase
    {
        private BuildConfigurator _Configurator;

        public ucMSBuilder()
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
            var item = cbx.SelectedItem.ToStringWithSolution();
            _Configurator = ConfiguratorFactory
                .GetConfigurator<BuildConfigurator>()
                .SetBuildPath(SessionManager.PathCollection[item]) as BuildConfigurator;
            var grid = UIHelper.CreateRotateVerticalGrid(
                    new BuildConfigInfor()
                    {
                        SolutionPath = _Configurator.GetSolutionPath(),
                        LogFile = _Configurator.GetLogFile()
                    }
                );
            this.scrollViewerProjectInfor.Content = grid;
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            var builder = this.CreateBuilder(new CommandLineProcess(_Configurator));
            builder.OnProcessCompleted += (obj, ev) =>
            {
                ProcessUtility.StartExplorer(((BuildDeployOutput)ev.ProcessOutput).LogFile);
            };
            builder.StartAsync();
        }
    }
}
