using SMTools.FileCopier;
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
    /// Error files in copy process
    /// </summary>
    public partial class ucFileDiff : UserControl
    {
        public ucFileDiff(string fromFolder, string toFolder, List<CopierFileDirErrorInfor> diffFiles)
        {
            InitializeComponent();
            this.txtFromFolder.Content = fromFolder;
            this.txtToFolder.Content = toFolder;
            if (diffFiles.Count != 0)
            {
                this.grdDiffFiles.ItemsSource = diffFiles.ToArray();
                this.grdDiffFiles.UpdateLayout();
            }
            else
            {
                this.grdDiffFiles.Visibility = System.Windows.Visibility.Hidden;
            }
        }
    }
}
