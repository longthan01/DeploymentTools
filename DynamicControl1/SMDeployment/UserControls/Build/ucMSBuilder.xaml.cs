using SMDeployment.AppCodes;
using SMDeployment.UIModels.MSBuilder;
using SMTools.DeploymentBase;
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
        public MSBuilder MSBuilder
        {
            get;
            set;
        }
        public ucMSBuilder()
        {
            InitializeComponent();
        }
        private List<BuildConfigInfor> GetList(params BuildConfigInfor[] items)
        {
            return new List<BuildConfigInfor>(items);
        }

        private void DevBuilderConfig_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.MSBuilder == null)
            {
                this.lblErrorMsg.Content = Properties.Resources.PROJECT_NOT_FOUND;
                return;
            }
            DeploymentProcessBuilder Builder = new DeploymentProcessBuilder(MSBuilder);
            Builder.OnProcessCompleted += (o, ev) =>
            {
                var op = ev.ProcessOutput as BuildOutput;
                MessageBox.Show(op.BuildOutMessage);
            };
            Builder.StartAsync();
        }

        private void lstViewSolutions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewItem item = this.lstViewSolutions.SelectedItem as ListViewItem;
            ConfigKey key = (ConfigKey)Enum.Parse(typeof(ConfigKey), item.Name);
            this.MSBuilder = new MSBuilder(DeploymentConfiguration.GetAppSettingValue(key));

            this.grdBuildInfor.ItemsSource = GetList(new BuildConfigInfor()
            {
                DeploymentOutput = MSBuilder.GetDeploymentPath(),
                PathToProject = MSBuilder.GetProjectPath()
            });
        }
    }
}
