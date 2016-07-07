using SMDeployment.AppCodes;
using SMDeployment.UIModels.TFSTransport;
using SMTools.Deployment.Utility;
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
        private ProjectConfigInfor ConfigInfor
        {
            get;
            set;
        }
        private TFSCheckOut TFSTransporter
        {
            get;
            set;
        }

        public ucTFSCheckout()
        {
            InitializeComponent();
            ConfigInfor = new ProjectConfigInfor();
            var lstViewItems =
                CollectionHelper.GetList(
                DeployEnvironment.Dev,
                DeployEnvironment.QA,
                DeployEnvironment.US);
            this.lstProject.ItemsSource = lstViewItems;
        }

        private void ShowMsg(string msg)
        {
            lblErrMsg.Content = msg;
            lblErrMsg.Visibility = Visibility.Visible;
        }

        private void txtFolderPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UIThreadHelper.RunWorker(this,
                    delegate
                    {
                        UIHelper.AddChild(this.stkGridDetails.Children, UIHelper.CreateScrollTree(DeploymentUtility.GetDirInfor(this.txtFolderPath.Text)));
                    });
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
            TFSTransporter.SourceFolder = this.txtFolderPath.Text;
            DeploymentProcessBuilder builder = new DeploymentProcessBuilder(TFSTransporter);
            builder.OnProcessCompleted += (obj, ev) =>
            {
                CheckOutOutput output = ev.ProcessOutput as CheckOutOutput;
                UIThreadHelper.RunWorker(this,
                   delegate
                   {
                       UIHelper.AddChild(this.stkGridDetails.Children,
                           new ucTFSCheckoutResult(output.AffectedFiles, output.ErrorFiles));
                   });
            };
            builder.Start();
        }

        private void lstProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = this.lstProject.SelectedItem;
            if (item == null)
            {
                return;
            }
            TFSTransporter = DeployToolFactory.GetProcess<TFSCheckOut>(ConfigFolder.TFS, item.ToDeployEnvironment());
            ConfigInfor.ConfigPath = TFSTransporter.ConfigurationFile;
            ConfigInfor.ProjectPath = TFSTransporter.GetProjectPath();
            UIHelper.AddChild(
                this.stkGridDetails.Children,
                    UIHelper.CreateScrollDataGrid(
                        CollectionHelper.GetList(ConfigInfor)));
        }
    }
}
