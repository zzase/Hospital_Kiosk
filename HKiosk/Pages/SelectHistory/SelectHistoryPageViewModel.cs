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
        private ObservableCollection<SujinHistroy> sujinHistories;

        private string count;
        private bool allChecked;

        private Nullable<DateTime> selectFromDateTime = null;
        private Nullable<DateTime> selectToDateTime = null;

        private ICommand readHistoriesCommand;
        private ICommand allCheckCommand;

        public ObservableCollection<SujinHistroy> SujinHistories
        {
            get
            {
                sujinHistories = DataManager.Instance.SujinHistroy;
                return sujinHistories;
            }
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

        public async void ReadHistories()
        {
            var data = await RequestAPI.CertSujinRequest(selectFromDateTime?.ToString("yyyyMMdd"), selectToDateTime?.ToString("yyyyMMdd"));

            if (data["resultCode"]?.ToString() == "200")
            {
                DataManager.Instance.SujinHistroy = new ObservableCollection<SujinHistroy>(RequestAPI.JArrayToList<SujinHistroy>(data["list"]?.Value<JArray>()));
                OnPropertyChanged("Jobs");
            }
            else
            {
                PopupManager.Instance[PopupElement.Alert].Show(data["resultMessage"]?.ToString());
            }

            SujinHistories = DataManager.Instance.SujinHistroy;
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
            for (int i = 0; i < DataManager.Instance.SujinHistroy.Count; i++)
            {
                if (AllChecked)
                {
                    DataManager.Instance.SujinHistroy[i].IsChecked = true;
                }
                else
                    DataManager.Instance.SujinHistroy[i].IsChecked = false;   
            }
            SujinHistories = DataManager.Instance.SujinHistroy;
        }

        public SelectHistoryPageViewModel()
        {
            SujinHistories = DataManager.Instance.SujinHistroy;

            MainPageCommand = new Command((obj) =>
            {
                DataManager.Instance.InitData();
                NavigationManager.Navigate(PageElement.Main);
            });

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
                    DataManager.Instance.SujinHistroy.Clear();
                    SujinHistories = DataManager.Instance.SujinHistroy;
                    NavigationManager.Navigate(PageElement.ConfirmRequestInfo);
                }
            });
            PreviousPageCommand = new Command((obj) =>
            {
                DataManager.Instance.SujinHistroy.Clear();
                SujinHistories = DataManager.Instance.SujinHistroy;
                NavigationManager.Navigate(PageElement.SelectCert);
            });
        }
    }
}
