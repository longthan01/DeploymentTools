﻿using SMDeployment.AppCodes;
using SMDeployment.UIModels.BuildDeploy;
using SMTools.Build.Base;
using SMTools.Build.Build;
using SMTools.Deployment.Base;
using SMTools.Utility;
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

namespace SMDeployment.UserControls.Build
{
    /// <summary>
    /// Interaction logic for ucMSBuilder.xaml
    /// </summary>
    public partial class ucMSBuilder : UserControl
    {
        private BuildConfigurator _Configurator;

        public ucMSBuilder()
        {
            InitializeComponent();
            this.grdProjectSelection.Children.Add(
                    UIHelper.CreateProjectSelectionGrid(
                        CollectionHelper.GetEnumList(typeof(Project)), OnProjectSelection));
                            
        }
        private void OnProjectSelection(object sender, SelectionChangedEventArgs e)
        {
            var cbx = e.OriginalSource as ComboBox;
            var item = cbx.SelectedItem.ToString();
            _Configurator = ConfiguratorFactory
                .GetConfigurator<BuildConfigurator>(ConfigSection.Build, item.ToProject());
            var grid = UIHelper.CreateRotateVerticalGrid(
                    new BuildDeployConfigInfor()
                    {
                        ProjectPath = _Configurator.GetProjectPath(),
                        SolutionPath = _Configurator.GetSolutionPath(),
                    }
                );
            this.scrollViewerProjectInfor.Content = grid;
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            if (_Configurator == null)
            {
                return;
            }
            imgLoading.Visibility = System.Windows.Visibility.Visible;
            ProcessBuilder builder = new ProcessBuilder(new SMTools.Build.Build.Builder(_Configurator));
            builder.OnProcessCompleted += (obj, ev) =>
            {
                ProcessUtility.StartExplorer(((BuildDeployOutput)ev.ProcessOutput).LogFile);
                UIThreadHelper.RunWorker(this, delegate
                {
                    imgLoading.Visibility = Visibility.Hidden;
                });
            };
            builder.StartAsync();
        }
    }
}
