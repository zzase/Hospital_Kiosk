using HKiosk.Base;
using HKiosk.Manager.Data;
using HKiosk.Manager.Navigation;
using HKiosk.Manager.Popup;
using HKiosk.Util;
using HKiosk.Util.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private string approvalNum;
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
        public string ApprovalNum
        {
            get => approvalNum;
            set => SetProperty(ref approvalNum, value);
        }
        public ICommand MainPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }

        private void DtTicker(object sender, EventArgs e)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(limit);
            limit--;
            LimitTime = "제한시간 : " + timeSpan.Minutes.ToString() + "분" + timeSpan.Seconds.ToString()+"초";
            if (limit <= -1)
            {
                timer.Stop();
                DataManager.Instance.FailText = "[핸드폰] 결제에 실패하였습니다.";
                DataManager.Instance.FailReason = "승인번호 입력시간 초과";
                NavigationManager.Navigate(PageElement.Fail);
            }
        }

        public ApprovalNumberPageViewModel()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(this.DtTicker);
            timer.Start();

            FinalPrice = "승인번호를 입력하시면,\n" + DataManager.Instance.FinalPrice + "이 결제됩니다.";
            ApprovalNum = null;

            MainPageCommand = new Command((obj) =>
            {
                timer.Stop();
                DataManager.Instance.InitData();
                NavigationManager.Navigate(PageElement.Main);
            });

            PreviousPageCommand = new Command((obj) =>
            {
                timer.Stop();
                NavigationManager.Navigate(PageElement.InfoInput);
            });

            NextPageCommand = new Command(async (obj) =>
            {
                var result = await KioskAgent.AuthPhone(ApprovalNum);
                result = result.Replace(Environment.NewLine, "");

                if (ApprovalNum == null)
                {
                    PopupManager.Instance[PopupElement.Alert]?.Show("승인번호를 입력해주세요.");
                }
                else if (ApprovalNum.Length != 6)
                {
                    PopupManager.Instance[PopupElement.Alert]?.Show("승인번호는 6자리 입니다.");
                }
                else if(!result.Contains("승인내역오류"))
                {
                    DataManager.Instance.PaymentInfo.ResultCode = Regex.Replace(result, @"\D", "");

                    var payResult = await RequestAPI.PayRequest();

                    timer.Stop();
                    NavigationManager.Navigate(PageElement.Print);
                }
                else
                {
                    DataManager.Instance.FailText = "[핸드폰] 결제에 실패하였습니다.";
                    DataManager.Instance.FailReason = "승인번호 불일치";
                    NavigationManager.Navigate(PageElement.Fail);
                }
            });
        }
    }
}
