using SMDeployment.AppCodes;
using SMDeployment.UIModels.TfsTransport;
using SMTools.Deployment.Utility;
using SMTools.Tfs.Checkout;
using SMTools.TFSTransporter.Checkout;
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

namespace SMDeployment.UserControls.Tfs
{
    /// <summary>
    /// Interaction logic for ucTfsCheckout.xaml
    /// </summary>
    public partial class ucTfsCheckout : UserControlBase
    {
        private TfsCheckoutConfigurator _Configurator;

        public ucTfsCheckout()
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
                .GetConfigurator<TfsCheckoutConfigurator>()
                .SetWorkspaceMapping(SessionManager.PathCollection[item])
                as TfsCheckoutConfigurator;
            var grid = UIHelper.CreateRotateVerticalGrid(
                    new ProjectConfigInfor()
                    {
                        ServerUrl = _Configurator.GetServerUrl(),
                        WorkspaceMapping = _Configurator.GetWorkspaceMapping()
                    }
                );
            this.scrollViewerProjectInfor.Content = grid;
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var txt = sender as TextBox;
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                var tree = UIHelper.CreateTree(DeploymentUtility.GetDirInfor(txt.Text), Brushes.GreenYellow, Brushes.Black, 14);
                UIHelper.AddChild(this.grdFolderInfor.Children, tree);
            }
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            this._Configurator.SetSourceFolder(this.txtSource.Text);
            var builder = this.CreateBuilder(new TfsCheckOut(_Configurator));
            builder.OnProcessCompleted += (obj, ev) =>
            {
                UIThreadHelper.RunWorker(this, delegate
                {
                    var op = ev.ProcessOutput as SMTools.Tfs.Checkout.CheckOutOutput;
                    var scroll = UIHelper.CreateScroll(new ucTfsDownloadFileResult(op));
                    UIHelper.AddChild(this.grdOutput.Children, scroll);
                });
            };
            builder.StartAsync();
        }
    }
}
