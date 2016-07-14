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


        private void AddUserControl<T>() where T : UserControl, new()
        {
            if (this.docPnlMain.Children.Count > 0)
            {
                UIElement current = null;
                foreach (UIElement elem in this.docPnlMain.Children)
                {
                    if (elem.GetType() != typeof(T))
                    {
                        elem.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        current = elem;
                    }
                }
                if (current != null)
                {
                    current.Visibility = Visibility.Visible;
                }
            }
            else
            {
                T c = new T();
                this.docPnlMain.Children.Add(c);
            }
        }

        private void MenuItemBuild_Click(object sender, RoutedEventArgs e)
        {
            AddUserControl<ucMSBuilder>();
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
            AddUserControl<ucTfsSearcher>();
        }

        private void MenuItemBoarding_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemDeploy_Click(object sender, RoutedEventArgs e)
        {
            AddUserControl<ucDeploy>();
        }
    }
}
