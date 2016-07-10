using SMDeployment.AppCodes;
using SMDeployment.UIModels.MSBuilder;
using SMTools.Deployment.Base;
using SMTools.MSBuilder;
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
    /// Interaction logic for ucMSBuilder.xaml
    /// </summary>
    public partial class ucMSBuilder : UserControl
    {
        private BuildProcess MSBuilder
        {
            get;
            set;
        }

        public ucMSBuilder()
        {
            InitializeComponent();
        }
        
        private void btnBuild_Click(object sender, RoutedEventArgs e)
        {
            if (this.MSBuilder == null)
            {
                this.lblErrorMsg.Content = Properties.Resources.PROJECT_NOT_FOUND;
                return;
            }
            ProcessBuilder Builder = new ProcessBuilder(new Builder(MSBuilder));
            Builder.OnProcessCompleted += (o, ev) =>
            {
                var op = ev.ProcessOutput as BuildOutput;
                UIThreadHelper.RunWorker(this,delegate{this.grdBuildInfor.ItemsSource = CollectionHelper.GetList(op);});
            };
            Builder.StartAsync();
        }

        private void btnDeploy_Click(object sender, RoutedEventArgs e)
        {
            if (this.MSBuilder == null)
            {
                this.lblErrorMsg.Content = Properties.Resources.PROJECT_NOT_FOUND;
                return;
            }
            ProcessBuilder Builder = new ProcessBuilder(new Deploymenter(MSBuilder));
            Builder.OnProcessCompleted += (o, ev) =>
            {
                var op = ev.ProcessOutput as BuildOutput;
                UIThreadHelper.RunWorker(this, delegate
                {
                    this.grdBuildInfor.ItemsSource = CollectionHelper.GetList(op);
                });
            };
            Builder.StartAsync();
        }

        private void lstViewSolutions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = this.lstViewSolutions.SelectedItem as ListViewItem;
            MSBuilder = DeployToolFactory.GetProcess<BuildProcess>(ConfigFolder.MSBuild, item.Content.ToDeployEnvironment());
            grdBuildInfor.ItemsSource = CollectionHelper.GetList(new BuildConfigInfor()
            {
                SolutionPath = MSBuilder.GetSolutionPath(),
                ProjectPath = MSBuilder.GetProjectPath(),
                DeploymentOutputFolder = MSBuilder.GetDeploymentOutputFolder()
            });
        }
    }
}
