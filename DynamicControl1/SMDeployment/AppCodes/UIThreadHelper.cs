using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDeployment.AppCodes
{
    public static class UIThreadHelper
    {
        public static void RunWorker( System.Windows.Controls.Control control,Action dowork, Action complete)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, e) =>
            {
                dowork();
            };
            worker.RunWorkerCompleted += (o1, e1) =>
            {
                control.Dispatcher.Invoke(complete);
            };
            worker.RunWorkerAsync();
        }
        public static void RunWorker(System.Windows.Controls.Control control, Action dowork)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, e) =>
            {
                control.Dispatcher.Invoke(dowork);
            };
            worker.RunWorkerAsync();
        }
    }
}
