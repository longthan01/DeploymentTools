using SMDeployment.AppCodes;
using SMTools.Deployment.Utility;
using SMTools.DeploymentBase;
using SMTools.FileCopier;
using SMTools.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ucFileCopier.xaml
    /// </summary>
    public partial class ucFileCopier : UserControl
    {
        private List<DestinationFolderType> _ExcludeFolders = new List<DestinationFolderType>();

        #region Fields, properties
        SMTools.FileCopier.FileCopier Copier
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public ucFileCopier()
        {
            InitializeComponent();
            Copier = DeployToolFactory.GetGeneralProcess<SMTools.FileCopier.FileCopier>(SharedConfigFolder.FileCopier);
            // create checkbox based on this config items
            foreach (var item in Copier.ConfigurationItems)
            {
                CheckBox chk = new CheckBox();
                chk.Checked += chkInclude_Checked;
                chk.Unchecked += chkInclude_Checked;
                chk.MouseDoubleClick += CheckBox_MouseDoubleClick;
                chk.Content = item.Name;
                chk.ToolTip = item.Value;
                this.stckCheckBoxsFolder.Children.Add(chk);
                DestinationFolderType t = (DestinationFolderType)Enum.Parse(typeof(DestinationFolderType), item.Name);
                this._ExcludeFolders.Add(t);
            }

        }
        #endregion

        #region utilities
        private void ShowError(FileCopierOutput output)
        {
            this.sckPnlDiff.Children.Clear();
            foreach (FileCopierOutputItem op in output.OutputItems)
            {
                string srcFold = op.SourceFolder;
                string destFold = op.DestinationFolder;
                List<CopierFileErrorInfor> errs = op.ErrorFiles;
                ucFileDiff fd = new ucFileDiff(srcFold, destFold, errs);
                this.sckPnlDiff.Children.Add(fd);
            }
        } 
        #endregion
        
        #region Event handles

        private void chkInclude_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            DestinationFolderType t = (DestinationFolderType)Enum.Parse(typeof(DestinationFolderType), chk.Content.ToString());
            if (!chk.IsChecked.Value)
            {
                _ExcludeFolders.Add(t);
            }
            else
            {
                _ExcludeFolders.Remove(t);
            }
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mr = MessageBox.Show("Are you sure u want to copy all file from Right to Left?", "Confirm", MessageBoxButton.YesNo);
            if (mr == MessageBoxResult.Yes)
            {
                imgLoading.Visibility = System.Windows.Visibility.Visible;
                Copier.SourceFolder = this.txtSourceFolder.Text;
                Copier.ExcludeDestination(_ExcludeFolders.ToArrayString());
                UIThreadHelper.RunWorker(this,new Action(() =>
                {
                    DeploymentProcessBuilder Builder = new DeploymentProcessBuilder(Copier);
                    Builder.OnProcessCompleted += (o, ev) =>
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            ShowError(ev.ProcessOutput as FileCopierOutput);
                        }));
                    };
                    Builder.StartAsync().Wait();
                }), new Action(() =>
                {
                    imgLoading.Visibility = System.Windows.Visibility.Hidden;
                }));
            }
        }

        private void txtSourceFolder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var tree = UIHelper.CreateScrollTree(DeploymentUtility.GetDirInfor(this.txtSourceFolder.Text));
                UIHelper.AddChild(this.gridFromFile.Children, tree);
            }
        }

        private void CheckBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (e.RightButton == MouseButtonState.Pressed)
            {
                var item = this.Copier.GetFolderPath(chk.Content.ToString());
                if (item != null)
                {
                    ProcessUtility.StartExplorer(item);
                }
            }
        }

        #endregion
    }
}
