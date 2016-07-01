using SMDeployment.UserControls.FileCopier;
using SMTools;
using SMTools.FileCopier;
using SMTools.TFSTransporter;
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
using SMTools.MSBuilder;
using SMDeployment.UserControls.Build;
using Microsoft.TeamFoundation.VersionControl.Client;
using SMDeployment.UserControls.TFS;

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

        private void AddFileCopier()
        {
            docPnlMain.Children.Clear();
            docPnlMain.Children.Add(new ucFileCopier());
        }
        private void AddMSBuilder()
        {
            docPnlMain.Children.Clear();
            docPnlMain.Children.Add(new ucMSBuilder());
        }
        private void AddTFSTransport()
        {
            docPnlMain.Children.Clear();
            docPnlMain.Children.Add(new ucTFSTransport());
        }
        private void AddTFSCheckout()
        {
            docPnlMain.Children.Clear();
            docPnlMain.Children.Add(new ucTFSCheckout());
        }

        private void MenuItemBuild_Click(object sender, RoutedEventArgs e)
        {
            AddMSBuilder();
        }

        private void MenuItemFC_Click(object sender, RoutedEventArgs e)
        {
            AddFileCopier();
        }

        private void MenuItemGL_Click(object sender, RoutedEventArgs e)
        {
            AddTFSTransport();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            // checkout
            AddTFSCheckout();
        }
    }
}
