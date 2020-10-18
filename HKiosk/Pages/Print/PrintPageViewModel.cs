using HKiosk.Base;
using HKiosk.Manager.Data;
using HKiosk.Manager.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace HKiosk.Pages.Print
{
    class PrintPageViewModel : PropertyChange
    {
        DispatcherTimer timer;
        private double printProgress;
        private string printText;

        private int countPrintProgressInit = 0;
        public double PrintProgress
        {
            get => printProgress;
            set => SetProperty(ref printProgress, value);
        }

        public string PrintText
        {
            get => printText;
            set => SetProperty(ref printText, value);
        }

        private void dtTicker(object sender, EventArgs e)
        {

            PrintProgress += 1.0;
            if (PrintProgress >= 0.0)
            {
                PrintText = DataManager.Instance.CertRequestInfos[countPrintProgressInit].Job.CertNe + "\n[출력 준비중]";
            }

            if (PrintProgress >= 100.0)
            {

                PrintText = "출력중";
            }
            if (PrintProgress >= 280.0)
            {
                PrintText = "출력완료";
            }
            if (PrintProgress >= 300.0)
            {
                PrintProgress = 0.0;
                countPrintProgressInit++;
                if (countPrintProgressInit >= DataManager.Instance.CertRequestInfos.Count)
                {
                    timer.Stop();
                    NavigationManager.Navigate(PageElement.PrintSuccess);
                }
            }

            //if (i >= DataManager.Instance.CertRequestInfos.Count - 1)
            //{
            //    NavigationManager.Navigate(PageElement.Main);
            //}


        }

        private void ProgressProcess()
        {
            PrintProgress = 0.0;
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(this.dtTicker);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 15);
            timer.Start();
        }

        public PrintPageViewModel()
        {
            ProgressProcess();
        }
    }
}
