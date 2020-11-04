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

namespace HKiosk.Pages.SelectHistory
{
    public class SelectHistoryPageViewModel : PropertyChange
    {
        private ObservableCollection<SujinHistroy> sujinHistories = new ObservableCollection<SujinHistroy>();

        private string count;
        private bool allChecked;

        private Nullable<DateTime> selectFromDateTime = null;
        private Nullable<DateTime> selectToDateTime = null;

        private ICommand readHistoriesCommand;
        private ICommand allCheckCommand;

        public ObservableCollection<SujinHistroy> SujinHistories
        {
            get => sujinHistories;
            set => SetProperty(ref sujinHistories, value);
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

        public ICommand MainPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand AllCheckCommand
        {
            get { return (this.allCheckCommand)??(this.allCheckCommand = new Command((obj)=>{ AllCheckedProcess(); })); }
        }
        public ICommand ReadHistoriesCommand
        {
            get { return (this.readHistoriesCommand) ?? (this.readHistoriesCommand = new Command((obj) => { ReadHistories(); })); }
        }

        public SelectHistoryPageViewModel()
        {
            SujinHistories.Clear();

            MainPageCommand = new Command((obj) => NavigateMainPage());

            NextPageCommand = new Command((obj) =>
            {
                if (SujinHistories == null)
                {
                    PopupManager.Instance[PopupElement.Alert]?.Show("수진이력을 검색해주세요.");
                    return;
                }
                else
                {
                    SelectHistories();
                    NavigationManager.Navigate(PageElement.ConfirmRequestInfo);
                }
            });
            PreviousPageCommand = new Command((obj) =>
            {
                NavigationManager.Navigate(PageElement.SelectCert);
            });
        }

        public async void ReadHistories()
        {
            PopupManager.Instance[PopupElement.Loding].Show("수진이력을 가져오는 중입니다.\n잠시만 기다려주세요.");

            var data = await RequestAPI.CertSujinRequest(selectFromDateTime?.ToString("yyyyMMdd"), selectToDateTime?.ToString("yyyyMMdd"));

            PopupManager.Instance[PopupElement.Loding].Hide();

            if (data == null)
            {
                PopupManager.Instance[PopupElement.Alert]?.Show("서버에 일시적인 오류가 발생했습니다. 재시도 부탁드립니다.");
                return;
            }

            if (data["resultCode"]?.ToString() == "200")
            {
                try
                {
                    SujinHistories = new ObservableCollection<SujinHistroy>(RequestAPI.JArrayToList<SujinHistroy>(data["list"]?.Value<JArray>()));
                }
                catch (Exception ex)
                {
                    PopupManager.Instance[PopupElement.Alert]?.Show("서버에 일시적인 오류가 발생했습니다. 재시도 부탁드립니다.");
                    Log.Write($"[SelectHistoryPageViewModel] ReadHistories exception : {ex}");
                    return;
                }

                if ((SujinHistories?.Count ?? 0) < 1)
                {
                    PopupManager.Instance[PopupElement.Alert].Show("수진이력이 없습니다.");
                    return;
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
            }

        }

        public void SelectHistories()
        {
            for (int i = 0; i < SujinHistories.Count; i++)
            {
                if (SujinHistories[i].IsChecked)
                {
                    CertRequestInfo certRequestInfo = new CertRequestInfo
                    {
                        Job = DataManager.Instance.SelectedJob,
                        Count = SujinHistories[i].Count,
                        SujinHistroy = SujinHistories[i],
                        IsCheckedForCancel = false
                    };
                    DataManager.Instance.CertRequestInfos.Add(certRequestInfo);
                }
            }
        }

        public void AllCheckedProcess()
        {
            for (int i = 0; i < SujinHistories.Count; i++)
            {
                if (AllChecked)
                {
                    SujinHistories[i].IsChecked = true;
                }
                else
                    SujinHistories[i].IsChecked = false;
            }
        }

        private void NavigateMainPage()
        {
            DataManager.Instance.InitData();
            NavigationManager.Navigate(PageElement.Main);
        }
    }
}
