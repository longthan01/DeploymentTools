using SMTools.FileCopier.Output;
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

namespace SMDeployment.UserControls.FileCopier
{
    /// <summary>
    /// Interaction logic for ucFileCopierError.xaml
    /// </summary>
    public partial class ucFileCopierError : UserControl
    {
        public ucFileCopierError(FileCopierOutputItem source)
        {
            InitializeComponent();
            this.txtSourceFolder.Text = source.SourceFolder;
            this.txtDestFolder.Text = source.DestinationFolder;
            this.dgrErrorFiles.ItemsSource = source.ErrorFiles;
            this.txtErrorFiles.Text = string.Format("{0} file(s).", source.ErrorFiles.Count);
            this.dgrErrorFiles.Visibility = source.ErrorFiles.Count == 0 ? Visibility.Hidden : Visibility.Visible;
        }
    }
}
