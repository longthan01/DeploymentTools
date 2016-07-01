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
using SMTools.DeploymentBase;
using SMDeployment.UIModels.TFSTransport;

namespace SMDeployment.UserControls.TFS
{
    /// <summary>
    /// Interaction logic for ucTFSTransport.xaml
    /// </summary>
    public partial class ucTFSTransport : UserControl
    {
        public ProjectConfigInfor ConfigInfor
        {
            get;
            set;
        }
        public ucTFSTransport()
        {
            InitializeComponent();
            ConfigInfor = new ProjectConfigInfor();
            List<string> lstViewItems = new List<string>()
            {
                ConfigKey.DevTFSConfig.ToString(),
                ConfigKey.QATFSConfig.ToString(),
                ConfigKey.USTFSConfig.ToString()
            };
            this.lstProject.ItemsSource = lstViewItems;
        }

        private void ShowMsg(string msg)
        {
            lblErrMsg.Content = msg;
            lblErrMsg.Visibility = Visibility.Visible;
        }
        private void SetConfigInfor()
        {
            string item = this.lstProject.SelectedItem as string;
            if (item == null)
            {
                return;
            }
            ConfigInfor.ConfigPath = DeploymentConfiguration.GetAppSettingValue(item.ToConfigKey());
            ConfigInfor.ProjectPath = DeploymentConfiguration.GetProjectPath(item.ToConfigKey());
        }

        private void txtFolderPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.grdFilesDetails.ItemsSource = DeploymentUtility.GetFileDirInfor(this.txtFolderPath.Text);
            }
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            lblErrMsg.Visibility = Visibility.Visible;
            if (string.IsNullOrEmpty(this.txtFolderPath.Text))
            {
                ShowMsg("Choose 1 project to run action.");
                return;
            }

            DeploymentProcessBuilder builder = new DeploymentProcessBuilder(new TFSGetLastest(ConfigInfor.ConfigPath));
            builder.OnProcessCompleted += (o, ev) =>
            {
                if (ev.ProcessOutput is CheckOutOutput)
                {
                    CheckOutOutput output = ev.ProcessOutput as CheckOutOutput;
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        this.grdFilesDetails.ItemsSource = output.ErrorFiles;
                    }));
                }
                if (ev.ProcessOutput is GetLastestOutput)
                {
                    GetLastestOutput output = ev.ProcessOutput as GetLastestOutput;
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        this.grdFilesDetails.ItemsSource = output.Failures.Select(x => x.LocalItem);
                    }));
                }
            };
            builder.StartAsync();
        }

        private void lstProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetConfigInfor();
            this.txtFolderPath.Text = ConfigInfor.ProjectPath;
        }
    }
}
