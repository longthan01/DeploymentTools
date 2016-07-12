using SMDeployment.AppCodes;
using SMDeployment.UIModels.BuildDeploy;
using SMTools.Build.Base;
using SMTools.Build.Deploy;
using SMTools.Deployment.Base;
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

namespace SMDeployment.UserControls.Build
{
    /// <summary>
    /// Interaction logic for ucDeploy.xaml
    /// </summary>
    public partial class ucDeploy : UserControl
    {
        private BuildDeployConfigurator _Configurator;

        public ucDeploy()
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
                .GetConfigurator<BuildDeployConfigurator>(AppCodes.Section.Deploy, item.ToProject());
            var grid = UIHelper.CreateRotateVerticalGrid(
                    new BuildDeployConfigInfor()
                    {
                        DeploymentOutputFolder = _Configurator.GetDeployOutFolder()
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
            ProcessBuilder builder = new ProcessBuilder(new SMTools.Build.Build.Builder(_Configurator));
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
