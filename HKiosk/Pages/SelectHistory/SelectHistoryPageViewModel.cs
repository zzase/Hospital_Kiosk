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

namespace HKiosk.Pages.SelectHistory
{
    public class SelectHistoryPageViewModel : PropertyChange
    {
        readonly HistroyProvider hp = new HistroyProvider();

        private string count;

        private ObservableCollection<SujinHistroy> sujinHistories;
        public ObservableCollection<SujinHistroy> SujinHistories
        {
            get { return sujinHistories; }
            set => SetProperty(ref sujinHistories, value);
        }
        private Nullable<DateTime> selectFromDateTime = null;
        private Nullable<DateTime> selectToDateTime = null;

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

        public ICommand MainPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }

        private ICommand readHistoriesCommand;
        public ICommand ReadHistoriesCommand
        {
            get { return (this.readHistoriesCommand) ?? (this.readHistoriesCommand = new Command((obj) => { ReadHistories(); })); }
        }

        public void ReadHistories()
        {
            SujinHistories = hp.AddHistory();

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
                        SujinHistroy = SujinHistories[i],
                        IsCheckedForCancel = false
                    };
                    DataManager.Instance.CertRequestInfos.Add(certRequestInfo);
                }
            }
        }

        public SelectHistoryPageViewModel()
        {

            MainPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Main));

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

            PreviousPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.SelectCert));
        }
    }
}
