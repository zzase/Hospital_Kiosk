using HKiosk.Base;
using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System.Windows.Input;
using HKiosk.Pages.SelectCert;
using System.Collections.Generic;
using System;
using HKiosk.Manager.Data;
using System.Collections.ObjectModel;


namespace HKiosk.Pages.SelectHistory 
{
    class SelectHistoryPageViewModel : PropertyChange
    {
        readonly HistroyProvider hp = new HistroyProvider();

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
                if (selectFromDateTime > selectToDateTime) selectFromDateTime = selectToDateTime;

                return selectFromDateTime;
            }
            set => SetProperty(ref selectFromDateTime, value);
        }

        public Nullable<DateTime> SelectToDateTime
        {
            get
            {
                if (selectToDateTime == null) selectToDateTime = DateTime.Now;
                if (selectToDateTime < selectFromDateTime) selectToDateTime = selectFromDateTime;
                return selectToDateTime;
            }
            set => SetProperty(ref selectToDateTime, value);
        }

        public ICommand MainPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }

        private ICommand readHistoriesCommand;
        public ICommand ReadHistoriesCommand
        {
            get { return (this.readHistoriesCommand) ?? (this.readHistoriesCommand = new Command((obj) => { ReadHistories();})); }
        }

        public void ReadHistories()
        {
            SujinHistories = hp.AddHistory();
            
        }

        public void SelectHistories()
        {
            for(int i=0; i<SujinHistories.Count; i++)
            {
                if (SujinHistories[i].IsChecked)
                {
                    DataManager.Instance.CertRequestInfo.SujinHistroy = SujinHistories[i];
                }
            }
        }

        public SelectHistoryPageViewModel()
        {

            MainPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Main));

            NextPageCommand = new Command((obj) => 
            {
                NavigationManager.Navigate(PageElement.ConfirmRequestInfo);
                SelectHistories();
            });

            PreviousPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.SelectCert));
        }
    }
}
