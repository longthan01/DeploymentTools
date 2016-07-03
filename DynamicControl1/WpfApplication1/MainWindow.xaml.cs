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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TreeView CreateTree(DirInfor dir)
        {
            TreeView tree = new TreeView();
            TreeViewItem root = new TreeViewItem();
            root.Header = dir.RelativeRoot;
            foreach (DirInfor df in dir.SubDirectories)
            {
                TreeViewItem ditem = new TreeViewItem();
                ditem.Header = df.RelativeRoot;
                ditem.Items.Add(CreateTree(df));
                root.Items.Add(ditem);
            }
            foreach (FileInfor ff in dir.Files)
            {
                TreeViewItem fitem = new TreeViewItem();
                fitem.Header = ff.FileName;
                root.Items.Add(fitem);
            }
            tree.Items.Add(root);
            return tree;
        }

        public MainWindow()
        {
            InitializeComponent();
            TreeView tree = CreateTree(DeploymentUtility.GetDirInfor(@"E:\test"));
            this.grdMain.Children.Add(tree);
        }
    }
}
