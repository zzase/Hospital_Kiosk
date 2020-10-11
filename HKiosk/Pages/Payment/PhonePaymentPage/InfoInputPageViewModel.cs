using HKiosk.Base;
using HKiosk.Manager.Navigation;
using HKiosk.Manager.Popup;
using HKiosk.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HKiosk.Pages.Payment.PhonePaymentPage
{
    class InfoInputPageViewModel : PropertyChange
    {
        public ICommand MainPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand SKTCommand { get; }
        public ICommand KTCommand { get; }
        public ICommand LGTCommand { get; }
        public ICommand HMCommand { get; }
        public ICommand TelinkCommand { get; }
        public ICommand KCTCommand { get; }


        private System.Windows.Media.Brush brushKT;
        private System.Windows.Media.Brush brushSKT;
        private System.Windows.Media.Brush brushLGT;
        private System.Windows.Media.Brush brushHM;
        private System.Windows.Media.Brush brushTelink;
        private System.Windows.Media.Brush brushKCT;

        private bool checkSKT = false;
        private bool checkKT = false;
        private bool checkLGT = false;
        private bool checkHM = false;
        private bool checkTelink = false;
        private bool checkKCT = false;

        private string frontPhoneNum;
        private string centerPhoneNum;
        private string backPhoneNum;

        private string frontJumin;
        private string backOne;

        public System.Windows.Media.Brush BrushSKT
        {
            get => brushSKT;
            set => SetProperty(ref brushSKT, value);
        }

        public System.Windows.Media.Brush BrushKT
        {
            get => brushKT;
            set => SetProperty(ref brushKT, value);
        }

        public System.Windows.Media.Brush BrushLGT
        {
            get => brushLGT;
            set => SetProperty(ref brushLGT, value);
        }

        public System.Windows.Media.Brush BrushHM
        {
            get => brushHM;
            set => SetProperty(ref brushHM, value);
        }

        public System.Windows.Media.Brush BrushTelink
        {
            get => brushTelink;
            set => SetProperty(ref brushTelink, value);
        }

        public System.Windows.Media.Brush BrushKCT
        {
            get => brushKCT;
            set => SetProperty(ref brushKCT, value);
        }

        public bool CheckSKT
        {
            get => checkSKT;
            set => SetProperty(ref checkSKT, value);
        }

        public bool CheckKT
        {
            get => checkKT;
            set => SetProperty(ref checkKT, value);
        }

        public bool CheckLGT
        {
            get => checkLGT;
            set => SetProperty(ref checkLGT, value);
        }

        public bool CheckHM
        {
            get => checkHM;
            set => SetProperty(ref checkHM, value);
        }

        public bool CheckTelink
        {
            get => checkTelink;
            set => SetProperty(ref checkTelink, value);
        }

        public bool CheckKCT
        {
            get => checkKCT;
            set => SetProperty(ref checkKCT, value);
        }

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

        public InfoInputPageViewModel()
        {
            BrushSKT = System.Windows.Media.Brushes.DarkGray;
            BrushKT = System.Windows.Media.Brushes.DarkGray;
            BrushLGT = System.Windows.Media.Brushes.DarkGray;
            BrushHM = System.Windows.Media.Brushes.DarkGray;
            BrushTelink = System.Windows.Media.Brushes.DarkGray;
            BrushKCT = System.Windows.Media.Brushes.DarkGray;

            FrontPhoneNum = null;
            CenterPhoneNum = null;
            BackPhoneNum = null;

            FrontJumin = "주민등록번호 앞 6자리/뒤 1자리";
            BackOne = null;

            MainPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Main));
            PreviousPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Agreement));
            NextPageCommand = new Command((obj) =>
            {
                if ((BrushSKT == System.Windows.Media.Brushes.DarkRed || BrushKT == System.Windows.Media.Brushes.DarkRed || BrushLGT == System.Windows.Media.Brushes.DarkRed || BrushHM == System.Windows.Media.Brushes.DarkRed || BrushTelink == System.Windows.Media.Brushes.DarkRed || BrushKCT == System.Windows.Media.Brushes.DarkRed)
                    && (FrontPhoneNum != null && CenterPhoneNum != null && BackPhoneNum != null)
                    && (FrontJumin != null && BackOne != null))
                {
                    NavigationManager.Navigate(PageElement.ApprovalNumber);
                }
                else if (!(BrushSKT == System.Windows.Media.Brushes.DarkRed || BrushKT == System.Windows.Media.Brushes.DarkRed || BrushLGT == System.Windows.Media.Brushes.DarkRed || BrushHM == System.Windows.Media.Brushes.DarkRed || BrushTelink == System.Windows.Media.Brushes.DarkRed || BrushKCT == System.Windows.Media.Brushes.DarkRed))
                    PopupManager.Instance[PopupElement.Alert]?.Show("이동통신사를 선택해주세요.");

                else if (!(FrontPhoneNum != null && CenterPhoneNum != null && BackPhoneNum != null))
                    PopupManager.Instance[PopupElement.Alert]?.Show("핸드폰번호를\n모두 입력해주세요.");

                else if (!(FrontJumin != null && BackOne != null))
                    PopupManager.Instance[PopupElement.Alert]?.Show("주민등록번호를\n모두 입력해주세요.");
            });

            SKTCommand = new Command((obj) =>
            {
                CheckSKT = !CheckSKT;

                if (CheckSKT)
                {
                    BrushSKT = System.Windows.Media.Brushes.DarkRed;
                    BrushKT = System.Windows.Media.Brushes.DarkGray;
                    BrushLGT = System.Windows.Media.Brushes.DarkGray;
                    BrushHM = System.Windows.Media.Brushes.DarkGray;
                    BrushTelink = System.Windows.Media.Brushes.DarkGray;
                    BrushKCT = System.Windows.Media.Brushes.DarkGray;
                }
                else BrushSKT = System.Windows.Media.Brushes.DarkGray;
            });

            KTCommand = new Command((obj) =>
            {
                CheckKT = !CheckKT;

                if (CheckKT)
                {
                    BrushSKT = System.Windows.Media.Brushes.DarkGray;
                    BrushKT = System.Windows.Media.Brushes.DarkRed;
                    BrushLGT = System.Windows.Media.Brushes.DarkGray;
                    BrushHM = System.Windows.Media.Brushes.DarkGray;
                    BrushTelink = System.Windows.Media.Brushes.DarkGray;
                    BrushKCT = System.Windows.Media.Brushes.DarkGray;
                }
                else BrushKT = System.Windows.Media.Brushes.DarkGray;
            });

            LGTCommand = new Command((obj) =>
            {
                CheckLGT = !CheckLGT;

                if (CheckLGT)
                {
                    BrushSKT = System.Windows.Media.Brushes.DarkGray;
                    BrushKT = System.Windows.Media.Brushes.DarkGray;
                    BrushLGT = System.Windows.Media.Brushes.DarkRed;
                    BrushHM = System.Windows.Media.Brushes.DarkGray;
                    BrushTelink = System.Windows.Media.Brushes.DarkGray;
                    BrushKCT = System.Windows.Media.Brushes.DarkGray;
                }
                else BrushLGT = System.Windows.Media.Brushes.DarkGray;
            });

            HMCommand = new Command((obj) =>
            {
                CheckHM = !CheckHM;

                if (CheckHM)
                {
                    BrushSKT = System.Windows.Media.Brushes.DarkGray;
                    BrushKT = System.Windows.Media.Brushes.DarkGray;
                    BrushLGT = System.Windows.Media.Brushes.DarkGray;
                    BrushHM = System.Windows.Media.Brushes.DarkRed;
                    BrushTelink = System.Windows.Media.Brushes.DarkGray;
                    BrushKCT = System.Windows.Media.Brushes.DarkGray;
                }
                else BrushHM = System.Windows.Media.Brushes.DarkGray;
            });

            TelinkCommand = new Command((obj) =>
            {
                CheckTelink = !CheckTelink;

                if (CheckTelink)
                {
                    BrushSKT = System.Windows.Media.Brushes.DarkGray;
                    BrushKT = System.Windows.Media.Brushes.DarkGray;
                    BrushLGT = System.Windows.Media.Brushes.DarkGray;
                    BrushHM = System.Windows.Media.Brushes.DarkGray;
                    BrushTelink = System.Windows.Media.Brushes.DarkRed;
                    BrushKCT = System.Windows.Media.Brushes.DarkGray;
                }
                else BrushTelink = System.Windows.Media.Brushes.DarkGray;
            });

            KCTCommand = new Command((obj) =>
            {
                CheckKCT = !CheckKCT;

                if (CheckKCT)
                {
                    BrushSKT = System.Windows.Media.Brushes.DarkGray;
                    BrushKT = System.Windows.Media.Brushes.DarkGray;
                    BrushLGT = System.Windows.Media.Brushes.DarkGray;
                    BrushHM = System.Windows.Media.Brushes.DarkGray;
                    BrushTelink = System.Windows.Media.Brushes.DarkGray;
                    BrushKCT = System.Windows.Media.Brushes.DarkRed;
                }
                else BrushKCT = System.Windows.Media.Brushes.DarkGray;
            });
        }
    }
}
