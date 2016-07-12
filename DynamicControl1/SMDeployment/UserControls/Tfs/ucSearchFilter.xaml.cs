using SMDeployment.AppCodes.TFS;
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

namespace SMDeployment.UserControls.Tfs
{
    /// <summary>
    /// Interaction logic for ucSearchFilter.xaml
    /// </summary>
    public partial class ucSearchFilter : UserControl
    {
        public string Comitter
        {
            get
            {
                return this.txtCommiter.Text;
            }
            set
            {
                this.txtCommiter.Text = value;
            }
        }
        public ucSearchFilter()
        {
            InitializeComponent();
            this.dpickerFrom.SelectedDate = DateTime.Now.Subtract(new TimeSpan(3, 0, 0, 0));
            this.dpickerTo.SelectedDate = DateTime.Now;
        }
        public SearchFilter GetFilter()
        {
            SearchFilter filter = new SearchFilter()
            {
                FromDate = this.dpickerFrom.SelectedDate??this.dpickerFrom.SelectedDate.Value, 
                ToDate = this.dpickerTo.SelectedDate??this.dpickerTo.SelectedDate.Value, 
                Commiter = this.txtCommiter.Text, 
                Comment = this.txtComment.Text
            };
            return filter;
        }
        
    }
}
