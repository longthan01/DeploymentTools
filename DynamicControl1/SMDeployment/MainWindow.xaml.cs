using SMTools.Extensions;
using SMTools.FileCopier;
using System;
using System.Collections.Generic;
using System.IO;
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
using SMDeployment.AppCodes;
using SMDeployment.UserControls.Build;
using Microsoft.TeamFoundation.VersionControl.Client;
using SMDeployment.UserControls.Tfs;

namespace SMDeployment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void AddUserControl(UserControl control)
        {
            docPnlMain.Children.Clear();
            this.docPnlMain.Children.Add(control);
        }
        private void MenuItemBuild_Click(object sender, RoutedEventArgs e)
        {
            AddUserControl(new ucMSBuilder());
        }

        private void MenuItemFC_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void MenuItemGL_Click(object sender, RoutedEventArgs e)
        {
        }

        private void MenuItemCheckout_Click(object sender, RoutedEventArgs e)
        {
        }

        private void itemSearch_Click(object sender, RoutedEventArgs e)
        {
            AddUserControl(new ucTfsSearcher());
        }
        private void MenuItemBoarding_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuItemDeploy_Click(object sender, RoutedEventArgs e)
        {
            AddUserControl(new ucDeploy());
        }
    }
}
