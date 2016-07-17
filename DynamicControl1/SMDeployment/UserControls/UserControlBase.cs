using SMDeployment.AppCodes;
using SMTools.Deployment.Base;
using System;
using System.Linq;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;

namespace SMDeployment.UserControls
{
    public class UserControlBase : System.Windows.Controls.UserControl
    {
        private ListView _ListLog;
        private ucLoading _LoadingImg;
        private void ShowLoading(bool show)
        {
            if (_LoadingImg != null)
            {
                Panel.SetZIndex(_LoadingImg, 100);
                _LoadingImg.Visibility = show ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            }
        }
        private string GetRootExeption(Exception ex)
        {
            if (ex == null)
            {
                return string.Empty;
            }
            if (ex.InnerException == null)
            {
                return ex.Message;
            }
            else
            {
                return GetRootExeption(ex.InnerException);
            }
        }
        /// <summary>
        /// Create process builder with default event
        /// </summary>
        /// <returns>An object of ProcessBuilder</returns>
        protected ProcessBuilder CreateBuilder(IDeployProcess process)
        {
            ProcessBuilder builder = new ProcessBuilder(process);
            builder.OnProcessBegining += (obj, ev) =>
            {
                UIThreadHelper.RunWorker(this, delegate
                {
                    ShowLoading(true);
                });
            };
            builder.OnProcessCompleted += (obj, ev) =>
            {
                UIThreadHelper.RunWorker(this, delegate
                {
                    ShowLoading(false);
                    Log(ev.ProcessOutput.Message);
                });
            };
            builder.OnProcessFailed += (obj, ev) =>
            {
                UIThreadHelper.RunWorker(this, delegate
                {
                    ShowLoading(false);
                    LogError(GetRootExeption(ev.Error.Exception)/* + Environment.NewLine + ev.Error.Exception.StackTrace*/);
                });
            };
            return builder;
        }
        protected void LogError(string log)
        {
            if (_ListLog != null)
            {
                _ListLog.Items.Insert(0, new Label()
                {
                    Content = "Error: " + DateTime.Now.ToString() + " - " + log,
                    Margin = new System.Windows.Thickness(10,0,10,0),
                    Foreground = Brushes.Red
                });
            }
        }
        protected void Log(string log)
        {
            if (_ListLog != null)
            {
                _ListLog.Items.Insert(0, new Label()
                {
                    Content = "Infor: " + DateTime.Now.ToString() + " - " + log,
                    Margin = new System.Windows.Thickness(10, 0, 10, 0),
                    Foreground = Brushes.Green
                });
            }
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            _LoadingImg = UIHelper.FindControl<ucLoading>(this, "imgLoading");
            _ListLog = UIHelper.FindControl<ListView>(this, "lstLog");
        }
    }
}
