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

namespace HKiosk.Pages.Payment.PhonePaymentPage
{
    class InfoInputPageViewModel : PropertyChange
    {
        private readonly string whiteBackground;
        private readonly string redBackground;

        private readonly System.Windows.Media.Brush darkGray;
        private readonly System.Windows.Media.Brush white;

        private readonly bool checkSKT;
        private readonly bool checkKT;
        private readonly bool checkLGT;
        private readonly bool checkHM;
        private readonly bool checkTelink;
        private readonly bool checkKCT;

        private readonly string imageSourceSKT;
        private readonly string imageSourceKT;
        private readonly string imageSourceLGT;
        private readonly string imageSourceHM;
        private readonly string imageSourceTelink;
        private readonly string imageSourceKCT;

        private readonly System.Windows.Media.Brush brushKT;
        private readonly System.Windows.Media.Brush brushSKT;
        private readonly System.Windows.Media.Brush brushLGT;
        private readonly System.Windows.Media.Brush brushHM;
        private readonly System.Windows.Media.Brush brushTelink;
        private readonly System.Windows.Media.Brush brushKCT;

        private string frontPhoneNum;
        private string centerPhoneNum;
        private string backPhoneNum;

        private string frontJumin;
        private string backOne;

        private ObservableCollection<bool> checkList;
        private ObservableCollection<System.Windows.Media.Brush> brushes;
        private ObservableCollection<string> imageSources;

        private ICommand sktCommand;
        private ICommand ktCommand;
        private ICommand lgtCommand;
        private ICommand hmCommand;
        private ICommand telinkCommand;
        private ICommand kctCommand;

        public ICommand MainPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand SKTCommand
        {
            get
            {
                return (this.sktCommand) ?? (this.sktCommand = new Command((obj) =>
          {
              for (int i = 0; i < CheckList.Count; i++)
              {
                  CheckList[i] = false;
              }

              CheckList[0] = true;
              CheckProcess();
          }));
            }
        }
        public ICommand KTCommand
        {
            get
            {
                return (this.ktCommand) ?? (this.ktCommand = new Command((obj) =>
                {
                    for (int i = 0; i < CheckList.Count; i++)
                    {
                        CheckList[i] = false;
                    }

                    CheckList[1] = true;
                    CheckProcess();
                }));
            }
        }
        public ICommand LGTCommand
        {
            get
            {
                return (this.lgtCommand) ?? (this.lgtCommand = new Command((obj) =>
                {
                    for (int i = 0; i < CheckList.Count; i++)
                    {
                        CheckList[i] = false;
                    }

                    CheckList[2] = true;
                    CheckProcess();
                }));
            }
        }
        public ICommand HMCommand
        {
            get
            {
                return (this.hmCommand) ?? (this.hmCommand = new Command((obj) =>
                {
                    for (int i = 0; i < CheckList.Count; i++)
                    {
                        CheckList[i] = false;
                    }

                    CheckList[3] = true;
                    CheckProcess();
                }));
            }
        }
        public ICommand TelinkCommand
        {
            get
            {
                return (this.telinkCommand) ?? (this.telinkCommand = new Command((obj) =>
                {
                    for (int i = 0; i < CheckList.Count; i++)
                    {
                        CheckList[i] = false;
                    }

                    CheckList[4] = true;
                    CheckProcess();
                }));
            }
        }
        public ICommand KCTCommand
        {
            get
            {
                return (this.kctCommand) ?? (this.kctCommand = new Command((obj) =>
                {
                    for (int i = 0; i < CheckList.Count; i++)
                    {
                        CheckList[i] = false;
                    }

                    CheckList[5] = true;
                    CheckProcess();
                }));
            }
        }

        public ObservableCollection<bool> CheckList
        {
            get => checkList;
            set => SetProperty(ref checkList, value);
        }

        public ObservableCollection<System.Windows.Media.Brush> Brushes
        {
            get => brushes;
            set => SetProperty(ref brushes, value);
        }
        public ObservableCollection<string> ImageSources
        {
            get => imageSources;
            set => SetProperty(ref imageSources, value);
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


        private void CheckProcess()
        {

            for (int i = 0; i < CheckList.Count; i++)
            {
                if (CheckList[i])
                {
                    Brushes[i] = white;
                    ImageSources[i] = redBackground;
                }
                else
                {
                    Brushes[i] = darkGray;
                    ImageSources[i] = whiteBackground;
                }
            }
        }

        private void SetCollections()
        {
            CheckList.Add(checkSKT);
            CheckList.Add(checkKT);
            CheckList.Add(checkLGT);
            CheckList.Add(checkHM);
            CheckList.Add(checkTelink);
            CheckList.Add(checkKCT);

            Brushes.Add(brushSKT);
            Brushes.Add(brushKT);
            Brushes.Add(brushLGT);
            Brushes.Add(brushHM);
            Brushes.Add(brushTelink);
            Brushes.Add(brushKCT);

            ImageSources.Add(imageSourceSKT);
            ImageSources.Add(imageSourceKT);
            ImageSources.Add(imageSourceLGT);
            ImageSources.Add(imageSourceHM);
            ImageSources.Add(imageSourceTelink);
            ImageSources.Add(imageSourceKCT);

            for (int i = 0; i < 6; i++)
            {
                CheckList[i] = false;
                ImageSources[i] = whiteBackground;
                Brushes[i] = darkGray;
            }
        }

        public InfoInputPageViewModel()
        {
            CheckList = new ObservableCollection<bool>();
            Brushes = new ObservableCollection<System.Windows.Media.Brush>();
            ImageSources = new ObservableCollection<string>();

            whiteBackground = "pack://application:,,,/Resources/Pages/Payment/Toggle_Phone_1.png";
            redBackground = "pack://application:,,,/Resources/Pages/Payment/Toggle_Phone_2.png";

            darkGray = System.Windows.Media.Brushes.DarkGray;
            white = System.Windows.Media.Brushes.White;

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
            NextPageCommand = new Command((obj) =>
                {

                    if (!((Brushes[0] == white || Brushes[1] == white || Brushes[2] == white || Brushes[3] == white || Brushes[4] == white || Brushes[5] == white)))
                        PopupManager.Instance[PopupElement.Alert]?.Show("이동통신사를 선택해주세요.");

                    else if ((FrontPhoneNum != "" && CenterPhoneNum != "" && BackPhoneNum != "") && !(FrontPhoneNum.Length == 3 && CenterPhoneNum.Length == 4 && BackPhoneNum.Length == 4))
                        PopupManager.Instance[PopupElement.Alert]?.Show("핸드폰번호를\n정확히 입력해주세요.");

                    else if (!(FrontJumin != "" && BackOne != "" && FrontJumin.Length == 6 && BackOne.Length == 1))
                        PopupManager.Instance[PopupElement.Alert]?.Show("주민등록번호를\n정확히 입력해주세요.");
                    else
                        NavigationManager.Navigate(PageElement.ApprovalNumber);
                });

            SetCollections();
        }
    }
}
