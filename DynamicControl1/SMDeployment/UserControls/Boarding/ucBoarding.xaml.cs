using SMDeployment.AppCodes;
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

namespace SMDeployment.UserControls.Boarding
{
    /// <summary>
    /// Interaction logic for ucBoarding.xaml
    /// </summary>
    public partial class ucBoarding : UserControl
    {
        public ucBoarding()
        {
            InitializeComponent();
            cbxBoardingModule.ItemsSource = CollectionHelper.GetEnumList(typeof(BoardingModule));
            cbxBoardingModule.SelectionChanged += CbxBoardingModule_SelectionChanged;
        }

        private void CbxBoardingModule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
