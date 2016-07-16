using SMDeployment.AppCodes;
using SMTools.Deployment.Utility;
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
            var tree = UIHelper.CreateTree(
                   DeploymentUtility.GetDirInfor(_Configurator.SourceFolder),
                   Brushes.Yellow, Brushes.Black, 15);
            this.scrollViewerProjectInfor.Content = tree;
        }
    }
}
