using SMTools.Tfs.Checkout;
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
    /// Interaction logic for ucTfsDownloadFileResult.xaml
    /// </summary>
    public partial class ucTfsDownloadFileResult : UserControl
    {
        public ucTfsDownloadFileResult(CheckOutOutput checkoutOutput)
        {
            InitializeComponent();
            this.dgrAffectedFiles.ItemsSource = checkoutOutput.AffectedFiles;
            this.dgrErrorFiles.ItemsSource = checkoutOutput.ErrorFiles;
            this.lblAff.Content = string.Format("{0} {1} file(s)", 
                this.lblErr.Content, checkoutOutput.AffectedFiles.Count);
            this.lblErr.Content = string.Format("{0} {1} file(s)",
                this.lblErr.Content, checkoutOutput.ErrorFiles.Count);
            dgrAffectedFiles.Visibility = checkoutOutput.AffectedFiles.Count == 0 ?
                Visibility.Hidden : Visibility.Visible;
            dgrErrorFiles.Visibility = checkoutOutput.ErrorFiles.Count == 0 ?
                Visibility.Hidden : Visibility.Visible;
        }
    }
}
