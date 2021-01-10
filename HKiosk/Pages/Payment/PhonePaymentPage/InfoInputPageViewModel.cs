using HKiosk.Base;
using HKiosk.Manager.Data;
using HKiosk.Manager.Navigation;
using HKiosk.Manager.Popup;
using HKiosk.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace HKiosk.Pages.Payment.PhonePaymentPage
{
    class InfoInputPageViewModel : PropertyChange
    {
        private readonly System.Windows.Media.Brush darkGray = System.Windows.Media.Brushes.DarkGray;
        private readonly System.Windows.Media.Brush white = System.Windows.Media.Brushes.White;

        private readonly BitmapImage whiteBackground = new BitmapImage(new Uri(@"/Resources/Pages/Payment/Toggle_Phone_1.png", UriKind.Relative));
        private readonly BitmapImage redBackground = new BitmapImage(new Uri(@"/Resources/Pages/Payment/Toggle_Phone_2.png", UriKind.Relative));

        private string frontPhoneNum;
        private string centerPhoneNum;
        private string backPhoneNum;

        private string frontJumin;
        private string backOne;

        private ObservableCollection<Agency> agencies = new ObservableCollection<Agency>();
        private Agency selectedAgency;

        public ICommand MainPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }

        public string FrontPhoneNum
        {
            get => frontPhoneNum;
            set => SetProperty(ref frontPhoneNum, value);
        }

        public string CenterPhoneNum
        {
            get => centerPhoneNum;
            set => SetProperty(ref centerPhoneNum, value);
        }

        public string BackPhoneNum
        {
            get => backPhoneNum;
            set => SetProperty(ref backPhoneNum, value);
        }

        public string FrontJumin
        {
            get => frontJumin;
            set => SetProperty(ref frontJumin, value);
        }

        public string BackOne
        {
            get => backOne;
            set => SetProperty(ref backOne, value);
        }


        public ObservableCollection<Agency> Agencies
        {
            get => agencies;
            set => SetProperty(ref agencies, value);
        }

        public Agency SelectedAgency
        {
            get => selectedAgency;
            set
            {
                foreach (var agency in Agencies)
                {
                    agency.Foreground = darkGray;
                    agency.Background = whiteBackground;
                }

                value.Foreground = white;
                value.Background = redBackground;

                SetProperty(ref selectedAgency, value);
            }
        }

        public InfoInputPageViewModel()
        {
            FrontPhoneNum = "";
            CenterPhoneNum = "";
            BackPhoneNum = "";

            FrontJumin = "주민등록번호 앞 6자리/뒤 1자리";
            BackOne = "";

            MainPageCommand = new Command((obj) =>
            {
                DataManager.Instance.InitData();
                NavigationManager.Navigate(PageElement.Main);
            });

            PreviousPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Agreement));

            NextPageCommand = new Command(async (obj) =>
            {
                if (SelectedAgency == null)
                    PopupManager.Instance[PopupElement.Alert]?.Show("이동통신사를 선택해주세요.");

                else if ((FrontPhoneNum == "" && CenterPhoneNum == "" && BackPhoneNum == "") && !(FrontPhoneNum.Length == 3 && CenterPhoneNum.Length == 4 && BackPhoneNum.Length == 4))
                    PopupManager.Instance[PopupElement.Alert]?.Show("핸드폰번호를\n정확히 입력해주세요.");

                else if (!(FrontJumin != "" && BackOne != "" && FrontJumin.Length == 6 && BackOne.Length == 1))
                    PopupManager.Instance[PopupElement.Alert]?.Show("주민등록번호를\n정확히 입력해주세요.");
                else
                {
                    var result = await PayPhone();

                    if (result.Contains("SUCCESS"))
                    {
                        NavigationManager.Navigate(PageElement.ApprovalNumber);
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(result))
                            result = "오류가 발생했습니다. 다시 시도 부탁드립니다.";

                        PopupManager.Instance[PopupElement.Alert]?.Show(result);
                        KioskAgent.KillPrevProcess();
                    }
                }
            });

            SetCollections();
        }

        private void SetCollections()
        {
            Agencies.Add(new Agency() { Name = "SKT", ParamName = "sk", Background = whiteBackground, Foreground = darkGray });
            Agencies.Add(new Agency() { Name = "KT", ParamName = "kt", Background = whiteBackground, Foreground = darkGray });
            Agencies.Add(new Agency() { Name = "LGT", ParamName = "lg", Background = whiteBackground, Foreground = darkGray });
            Agencies.Add(new Agency() { Name = "헬로\n모바일", ParamName = "cjh", Background = whiteBackground, Foreground = darkGray });
            Agencies.Add(new Agency() { Name = "SK\nTelink", ParamName = "skl", Background = whiteBackground, Foreground = darkGray });
            Agencies.Add(new Agency() { Name = "KCT", ParamName = "kct", Background = whiteBackground, Foreground = darkGray });
        }

        private async Task<string> PayPhone()
        {
            var finalPrice = DataManager.Instance.FinalPrice.Replace(",","").Replace("원","");
            var certNe = $"{DataManager.Instance.CertRequestInfos[0].Job.CertNe}" +
                $"{(DataManager.Instance.CertRequestInfos.Count - 1 > 0 ? $"외{DataManager.Instance.CertRequestInfos.Count - 1}건" : "")}";
            var hppNo = $"{FrontPhoneNum}{CenterPhoneNum}{BackPhoneNum}";
            var agency = SelectedAgency.ParamName;
            var juminNo = $"{FrontJumin}{BackOne}";
            var orderNo = DataManager.Instance.PaymentInfo.OrderNo;
            var giwanNe = "디지털존";

            DataManager.Instance.PaymentInfo.BuyerName = "";
            DataManager.Instance.PaymentInfo.BuyerTel = hppNo;
            DataManager.Instance.PaymentInfo.Amt = finalPrice;
            DataManager.Instance.PaymentInfo.GoodsName = certNe;

            return await KioskAgent.PayPhone("", "", finalPrice, certNe,
                   hppNo, agency, juminNo, orderNo, giwanNe);
        }
    }
}
