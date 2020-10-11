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

namespace HKiosk.Pages.Payment.PhonePaymentPage
{
    class ApprovalNumberPageViewModel : PropertyChange
    {
        public DispatcherTimer timer;

        int limit = 180;

        private string limitTime;

        private string finalPrice;
        public string FinalPrice
        {
            get => finalPrice;
            set => SetProperty(ref finalPrice, value);
        }
        public string LimitTime
        {
            get => limitTime;
            set => SetProperty(ref limitTime, value);
        }
        public ICommand MainPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }

        private void dtTicker(object sender, EventArgs e)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(limit);
            limit--;
            LimitTime = "제한시간 : " + timeSpan.Minutes.ToString() + "분" + timeSpan.Seconds.ToString()+"초";
        }

        public ApprovalNumberPageViewModel()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(this.dtTicker);
            timer.Start();

            FinalPrice = "승인번호를 입력하시면,\n" + DataManager.Instance.FinalPrice + "이 결제됩니다.";

            MainPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Main));
            PreviousPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.InfoInput));
        }
    }
}
