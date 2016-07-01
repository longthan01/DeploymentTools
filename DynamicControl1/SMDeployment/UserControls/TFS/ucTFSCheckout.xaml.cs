using SMDeployment.AppCodes;
using SMDeployment.UIModels.TFSTransport;
using SMTools.DeploymentBase;
using SMTools.TFSTransporter;
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

namespace SMDeployment.UserControls.TFS
{
    /// <summary>
    /// Interaction logic for ucTFSCheckout.xaml
    /// </summary>
    public partial class ucTFSCheckout : UserControl
    {
        public ProjectConfigInfor ConfigInfor
        {
            get;
            set;
        }
        public ucTFSCheckout()
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
        private DataGrid CreateGrid()
        {
            DataGrid grid = new DataGrid()
            {
                CanUserAddRows = false,
                ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star),
                Background = Brushes.Transparent
            };
            return grid;
        }
        private void CreateStackGrid(params List<FileDirInfor>[] sources)
        {
            this.stkGridDetails.Children.Clear();
            foreach (List<FileDirInfor> item in sources)
            {
                var grid = CreateGrid();
                grid.ItemsSource = item;
                this.stkGridDetails.Children.Add(grid);
            }
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
                var list = DeploymentUtility.GetFileDirInfor(this.txtFolderPath.Text);
                CreateStackGrid(list);
            }
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ConfigInfor.ConfigPath))
            {
                ShowMsg("Choose 1 project to checkout.");
                return;
            }
            if (string.IsNullOrEmpty(ConfigInfor.ProjectPath))
            {
                ShowMsg("Locate to folder first.");
                return;
            }
            DeploymentProcessBuilder builder =
                new DeploymentProcessBuilder(
                    new TFSCheckOut(ConfigInfor.ConfigPath, this.txtFolderPath.Text));
            builder.OnProcessCompleted += (obj, ev) =>
            {
                CheckOutOutput output = ev.ProcessOutput as CheckOutOutput;
                this.Dispatcher.Invoke(new Action(() =>
                {
                    CreateStackGrid(output.AffectedFiles, output.ErrorFiles);
                }));
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
