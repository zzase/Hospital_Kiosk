using HKiosk.Base;
using HKiosk.Manager.Data;
using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace HKiosk.Pages.Print
{
    class PrintSuccessPageViewModel : PropertyChange
    {
        public DispatcherTimer timer;
        private string limitLogOut;
        private int limit = 59;
        public string LimitLogOut
        {
            get => limitLogOut;
            set => SetProperty(ref limitLogOut, value);
        }

        public ICommand LogOutCommand { get; }

        private void DtTicker(object sender, EventArgs e)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(limit);
            limit--;
            LimitLogOut = timeSpan.Seconds.ToString() + " 초  후 자동 로그아웃 됩니다.";
            if(limit <= -1)
            {
                timer.Stop();
                DataManager.Instance.InitData();
                NavigationManager.Navigate(PageElement.Main);
            }
        }

        public PrintSuccessPageViewModel()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(this.DtTicker);
            timer.Start();

            LogOutCommand = new Command((obj) =>
            {
                timer.Stop();
                DataManager.Instance.InitData();
                NavigationManager.Navigate(PageElement.Main);
            });
        }
    }
}
