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
using SMDeployment.AppCodes;
using SMTools.Deployment.Base;
using SMDeployment.UIModels.TFSTransport;
using SMTools.Deployment.Utility;

namespace SMDeployment.UserControls.TFS
{
    /// <summary>
    /// Interaction logic for ucTFSTransport.xaml
    /// </summary>
    public partial class ucTFSTransport : UserControl
    {
        private ProjectConfigInfor _ConfigInfor
        {
            get;
            set;
        }

        private TfsGetLastest _TFSTransporter
        {
            get;
            set;
        }

        public ucTFSTransport()
        {
            InitializeComponent();
            _ConfigInfor = new ProjectConfigInfor();
            var lstViewItems = Extensions.GetEnumList(typeof(ConfigFile));
            this.lstProject.ItemsSource = lstViewItems;
        }

        private void ShowMsg(string msg)
        {
            lblErrMsg.Content = msg;
            lblErrMsg.Visibility = Visibility.Visible;
        }
        
        private void txtFolderPath_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            lblErrMsg.Visibility = Visibility.Visible;
            if (string.IsNullOrEmpty(this.txtFolderPath.Text))
            {
                ShowMsg("Choose 1 project to run action.");
                return;
            }

            ProcessBuilder builder = new ProcessBuilder(_TFSTransporter);
            builder.OnProcessCompleted += (o, ev) =>
            {
                GetLastestOutput output = ev.ProcessOutput as GetLastestOutput;
                UIThreadHelper.RunWorker(this, delegate
                {
                   UIHelper.AddChild(
                       this.stckGetLastestDetails.Children,
                       UIHelper.CreateScrollDataGrid(output.Failures.Select(x => x.LocalItem)));

                });
            };
            builder.StartAsync();
        }

        private void lstProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string item = this.lstProject.SelectedItem as string;
            if (item == null)
            {
                return;
            }
            _TFSTransporter = DeployToolFactory.GetProcess<TfsGetLastest>(ConfigFolder.TFS, item.ToDeployEnvironment());
            _ConfigInfor.ConfigPath = _TFSTransporter.ConfigurationFile;
            _ConfigInfor.ProjectPath = _TFSTransporter.GetProjectPath();
            this.txtFolderPath.Text = _ConfigInfor.ProjectPath;
        }
    }
}
