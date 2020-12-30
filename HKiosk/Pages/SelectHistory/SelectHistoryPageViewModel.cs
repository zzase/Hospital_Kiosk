using HKiosk.Base;
using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System.Windows.Input;
using HKiosk.Pages.SelectCert;
using System.Collections.Generic;
using System;
using HKiosk.Manager.Data;
using System.Collections.ObjectModel;
using HKiosk.Manager.Popup;
using HKiosk.Util.Server;
using Newtonsoft.Json.Linq;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;

namespace HKiosk.Pages.SelectHistory
{
    public class SelectHistoryPageViewModel : PropertyChange
    {
        private ObservableCollection<SujinHistroy> sujinHistories = new ObservableCollection<SujinHistroy>();

        private string count;
        private bool allChecked;

        private Nullable<DateTime> selectFromDateTime = null;
        private Nullable<DateTime> selectToDateTime = null;

        private readonly System.Windows.Media.Brush darkRed = System.Windows.Media.Brushes.DarkRed;
        private readonly System.Windows.Media.Brush white = System.Windows.Media.Brushes.White;

        private readonly BitmapImage whiteBackground = new BitmapImage(new Uri(@"/Resources/Common/Buttons/Toggle_Lang_1.png", UriKind.Relative));
        private readonly BitmapImage redBackground = new BitmapImage(new Uri(@"/Resources/Common/Buttons/Toggle_Lang_2.png", UriKind.Relative));

        private ObservableCollection<Interval> intervals = new ObservableCollection<Interval>();
        private Interval selectedInterval;

        public ObservableCollection<SujinHistroy> SujinHistories
        {
            get => sujinHistories;
            set => SetProperty(ref sujinHistories, value);
        }

        public ObservableCollection<Interval> Intervals
        {
            get => intervals;
            set => SetProperty(ref intervals, value);
        }

        public Nullable<DateTime> SelectFromDateTime
        {
            get
            {
                if (selectFromDateTime == null) selectFromDateTime = DateTime.Now;
                if (selectFromDateTime > selectToDateTime)
                {
                    selectFromDateTime = selectToDateTime;
                    PopupManager.Instance[PopupElement.Alert]?.Show("조회시작일자는 조회종료일자보다 앞 날이거나 같은 날이어야 합니다.");
                }

                return selectFromDateTime;
            }
            set => SetProperty(ref selectFromDateTime, value);
        }

        public Nullable<DateTime> SelectToDateTime
        {
            get
            {
                if (selectToDateTime == null) selectToDateTime = DateTime.Now;
                if (selectToDateTime < selectFromDateTime)
                {
                    selectToDateTime = selectFromDateTime;
                    PopupManager.Instance[PopupElement.Alert]?.Show("조회종료일자는 조회시작일자보다 뒷 날이거나 같은 날이어야 합니다.");
                }
                return selectToDateTime;
            }
            set => SetProperty(ref selectToDateTime, value);
        }

        public string Count
        {
            get => count;
            set => SetProperty(ref count, value);
        }

        public bool AllChecked
        {
            get => allChecked;
            set => SetProperty(ref allChecked, value);
        }

        public string CertNe
        {
            get => DataManager.Instance.SelectedJob.CertNe;
        }

        public Interval SelectedInterval
        {
            get => selectedInterval;
            set
            {
                foreach (var interval in Intervals)
                {
                    interval.Foreground = darkRed;
                    interval.Background = whiteBackground;
                }

                value.Foreground = white;
                value.Background = redBackground;

                if (value.Name.Equals("1개월"))
                {
                    SelectFromDateTime = SelectToDateTime.Value.AddDays(-30);
                }

                else if (value.Name.Equals("3개월"))
                {
                    SelectFromDateTime = SelectToDateTime.Value.AddDays(-90);
                }

                else if (value.Name.Equals("6개월"))
                {
                    SelectFromDateTime = SelectToDateTime.Value.AddDays(-180);
                }

                else if (value.Name.Equals("1년"))
                {
                    SelectFromDateTime = SelectToDateTime.Value.AddDays(-365);
                }

                //ReadHistories();
                SetProperty(ref selectedInterval, value);
            }
        }

        public ICommand MainPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
       
        public SelectHistoryPageViewModel()
        {
            SujinHistories.Clear();
            SetIntervals();

            MainPageCommand = new Command((obj) => NavigateMainPage());

            NextPageCommand = new Command(async (obj) =>
            {
                if (SujinHistories == null)
                {
                    PopupManager.Instance[PopupElement.Alert]?.Show("수진이력을 검색해주세요.");
                    return;
                }
                else
                {
                    if (await ReadHistories())
                        NavigationManager.Navigate(PageElement.SelectDetail);
                }
            });
            PreviousPageCommand = new Command((obj) =>
            {

                NavigationManager.Navigate(PageElement.SelectCert);
            });
        }

        public async Task<bool>  ReadHistories()
        {
            PopupManager.Instance[PopupElement.Loding].Show("수진이력을 가져오는 중입니다.\n잠시만 기다려주세요.");

            var data = await RequestAPI.CertSujinRequest(selectFromDateTime?.ToString("yyyyMMdd"), selectToDateTime?.ToString("yyyyMMdd"));

            PopupManager.Instance[PopupElement.Loding].Hide();

            if (data == null)
            {
                PopupManager.Instance[PopupElement.Alert]?.Show("서버에 일시적인 오류가 발생했습니다. 재시도 부탁드립니다.");
                return false;
            }

            if (data["resultCode"]?.ToString() == "200")
            {
                try
                {
                    SujinHistories = new ObservableCollection<SujinHistroy>(RequestAPI.JArrayToList<SujinHistroy>(data["list"]?.Value<JArray>()));
                    for(int i=0; i<SujinHistories.Count; i++)
                    {
                        SujinHistories[i].InDate = FormatStringToDate(SujinHistories[i].InDate);
                        SujinHistories[i].OutDate = FormatStringToDate(SujinHistories[i].OutDate);
                    }
                    DataManager.Instance.SujinHistroys = SujinHistories;
                }
                catch (Exception ex)
                {
                    PopupManager.Instance[PopupElement.Alert]?.Show("서버에 일시적인 오류가 발생했습니다. 재시도 부탁드립니다.");
                    Log.Write($"[SelectHistoryPageViewModel] ReadHistories exception : {ex}");
                    return false;
                }

                if ((SujinHistories?.Count ?? 0) < 1)
                {
                    PopupManager.Instance[PopupElement.Alert].Show("수진이력이 없습니다.");
                    return false;
                }
            }
            else
            {
                var resultMessage = data["resultMessage"]?.ToString();

                if (string.IsNullOrWhiteSpace(resultMessage))
                {
                    resultMessage = "서버에 일시적인 오류가 발생했습니다. 재시도 부탁드립니다.";
                }

                PopupManager.Instance[PopupElement.Alert].Show(resultMessage);
                return false;
            }

            return true;
        }

        private void SetIntervals()
        {
            Intervals.Add(new Interval { Name = "1개월", Background = whiteBackground, Foreground = darkRed });
            Intervals.Add(new Interval { Name = "3개월", Background = whiteBackground, Foreground = darkRed });
            Intervals.Add(new Interval { Name = "6개월", Background = whiteBackground, Foreground = darkRed });
            Intervals.Add(new Interval { Name = "1년", Background = whiteBackground, Foreground = darkRed });
        }

        private void NavigateMainPage()
        {
            DataManager.Instance.InitData();
            NavigationManager.Navigate(PageElement.Main);
        }

        public static string FormatStringToDate(string tdrDate)
        {
            if (tdrDate != null)
            {
                if (tdrDate.Length > 4)
                {
                    return DateTime.ParseExact(tdrDate, "yyyyMMdd", null).ToString("yyyy.MM.dd");
                }
                else if (tdrDate.Length == 4)
                {
                    return tdrDate + "년";
                }
                else
                    return tdrDate;
                    
            }
            else return tdrDate;
            
            
        }
    }
}
