using SMDeployment.AppCodes;
using SMDeployment.UIModels.FileCopier;
using SMTools.FileCopier;
using System.Windows.Controls;

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
            _Configurator = ConfiguratorFactory.GetConfigurator<FileCopierConfigurator>();
            this.dgrDestinationPaths.ItemsSource = _Configurator
                .DestinationFolder
                .Convert<DestinationFolder, DestinationFolderUI>();
        }
        private void btnRun_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            for (int i = 0; i < dgrDestinationPaths.ItemContainerGenerator.Items.Count; i++)
            {
            }
        }

        private void chkNeedBackup_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            var chkName = sender as CheckBox;
            int idx = dgrDestinationPaths.SelectedIndex;
            if (idx < 0) return;
            var lbl = UIHelper.GetDataGridRowControl("lblFolderName", this.dgrDestinationPaths, idx, 1) as Label;
            if (lbl != null)
            {
                if (!chkName.IsChecked.Value)
                {
                    this._Configurator.ExcludeDestination(lbl.Content.ToString());
                }
                else
                {
                    this._Configurator.IncludeDestination(lbl.Content.ToString());
                }
            }
        }
    }
}
