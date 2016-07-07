using SMTools.Deployment.Utility;
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
    /// Interaction logic for ucTFSCheckoutResult.xaml
    /// </summary>
    public partial class ucTFSCheckoutResult : UserControl
    {
        public ucTFSCheckoutResult(List<FileInfor> affectedFiles, List<FileInfor> errorFiles)
        {
            InitializeComponent();
            this.dgrAffectedFiles.ItemsSource = affectedFiles;
            this.dgrErrorFiles.ItemsSource = errorFiles;
        }
    }
}
