using SMDeployment.AppCodes;
using SMDeployment.UIModels.FileCopier;
using SMTools.Deployment.Utility;
using SMTools.FileCopier;
using SMTools.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using SMTools.FileCopier.Output;
namespace SMDeployment.UserControls.FileCopier
{
    /// <summary>
    /// Interaction logic for ucFileCopier.xaml
    /// </summary>
    public partial class ucFileCopier : UserControlBase
    {
        private FileCopierConfigurator _Configurator;

        public ucFileCopier()
        {
            InitializeComponent();
            UIHelper.AddChild(this.grdProjectSelection.Children,
                   UIHelper.CreateSelectionGrid(
                       "Select project: ",
                       new List<string>() { "FDC", "Boarding" },
                       null, null,
                       OnProjectSelection
                       ));
        }

        private void OnProjectSelection(object sender, SelectionChangedEventArgs e)
        {
            var item = "FileCopy" + (sender as ComboBox).SelectedItem.ToString();
            _Configurator = ConfiguratorFactory.GetConfigurator<FileCopierConfigurator>(item.ToXmlConfigSection());
            if (_Configurator == null)
            {
                this.LogError("Could not found " + item + " section in config file.");
                return;
            }
            this.dgrDestinationPaths.ItemsSource = _Configurator
                .DestinationFolder
                .Convert<DestinationFolder, DestinationFolderUI>();
        }

        private void btnRun_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_Configurator == null)
            {
                this.LogError("Could not found destination folder");
                return;
            }
            if (this.txtSource.Text.IsEmpty())
            {
                this.LogError("Could not found source folder.");
                return;
            }
            for (int i = 0; i < this.dgrDestinationPaths.Items.Count; i++)
            {
                var chkBackup = UIHelper.GetDataGridRowControl("chkNeedBackup", this.dgrDestinationPaths, i, 3) as CheckBox;
                var chkSelect = UIHelper.GetDataGridRowControl("chkSelected", this.dgrDestinationPaths, i, 0) as CheckBox;
                var lbl = UIHelper.GetDataGridRowControl("lblFolderName", this.dgrDestinationPaths, i, 1) as Label;
                _Configurator.SetBackup(lbl.Content.ToString(), chkBackup.IsChecked.Value);
                if (!chkSelect.IsChecked.Value)
                {
                    this._Configurator.ExcludeDestination(lbl.Content.ToString());
                }
                else
                {
                    this._Configurator.IncludeDestination(lbl.Content.ToString());
                }
            }
            var builder = this.CreateBuilder((new SMTools.FileCopier.FileCopier(_Configurator)));
            _Configurator.SetSourceFolder(this.txtSource.Text);
            builder.OnProcessCompleted += (obj, ev) =>
            {
                UIThreadHelper.RunWorker(this, delegate
                {
                    var op = ev.ProcessOutput as FileCopierOutput;
                    StackPanel stk = new StackPanel();
                    foreach (var item in op.OutputItems)
                    {
                        stk.Children.Add(new ucFileCopierError(item));
                    }
                    var scroll = UIHelper.CreateScroll(stk);
                    UIHelper.AddChild(this.grdOutput.Children, scroll);
                });
            };
            builder.StartAsync();
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var txt = sender as TextBox;
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                var tree = UIHelper.CreateTree(DeploymentUtility.GetDirInfor(txt.Text), Brushes.GreenYellow, Brushes.Black, 14);
                UIHelper.AddChild(this.grdFolderInfor.Children, tree);
            }
        }
    }
}
