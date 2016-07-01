using SMTools.DeploymentBase;
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

namespace SMDeployment.UserControls
{
    /// <summary>
    /// Interaction logic for ucTree.xaml
    /// </summary>
    public partial class ucTree : UserControl
    {
        public ucTree()
        {
            InitializeComponent();
           this.tvTree.ItemsSource =  DeploymentUtility.GetAllFiles(@"D:\Software\unikey32");
        }
    }
}
