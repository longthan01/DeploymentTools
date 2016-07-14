using SMDeployment.AppCodes;
using SMTools.Deployment.Base;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace SMDeployment.UserControls
{
    public class UserControlBase : System.Windows.Controls.UserControl
    {
        private ProcessBuilder _Builder = null;

        public ProcessBuilder Builder
        {
            get
            {
                if (_Builder == null)
                {
                    _Builder = new ProcessBuilder();
                }
                return _Builder;
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            Builder.OnProcessBegining += (obj, ev) =>
            {
                UIThreadHelper.RunWorker(this, delegate
                {
                    ucLoading uc = UIHelper.FindControl<ucLoading>(this, "imgLoading");
                    uc.Visibility = System.Windows.Visibility.Visible;
                });
            };
            Builder.OnProcessCompleted += (obj, ev) =>
            {
                UIThreadHelper.RunWorker(this, delegate
                {
                    ucLoading uc = UIHelper.FindControl<ucLoading>(this, "imgLoading");
                    uc.Visibility = System.Windows.Visibility.Hidden;
                });
            };
            Builder.OnProcessFailed += (obj, ev) =>
            {
                UIThreadHelper.RunWorker(this, delegate
                {
                    ucLoading uc = UIHelper.FindControl<ucLoading>(this, "imgLoading");
                    uc.Visibility = System.Windows.Visibility.Hidden;
                    ListView lst = UIHelper.FindControl<ListView>(this, "lstLog");
                    lst.Items.Insert(0,new Label() {
                        Content = "Error: " +  DateTime.Now.ToString() + " - " +  ev.Error.ErrorMessage,
                        Foreground = Brushes.Red
                    });
                });
            };
        }

        public void Log(string message)
        {
            ListView lst = UIHelper.FindControl<ListView>(this, "lstLog");
            if (lst != null)
            {
                lst.Items.Insert(0,new Label()
                {
                    Content = "Error: " + DateTime.Now.ToString() + " - " + message,
                    Foreground = Brushes.Red
                });
            }
        }
    }
}
