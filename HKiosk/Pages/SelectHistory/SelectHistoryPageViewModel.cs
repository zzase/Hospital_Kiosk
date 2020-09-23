using HKiosk.Base;
using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System.Windows.Input;
using HKiosk.Pages.SelectCert;
using System.Collections.Generic;
using System;

namespace HKiosk.Pages.SelectHistory 
{
    class SelectHistoryPageViewModel : PropertyChange
    {
        readonly HistroyProvider hp = new HistroyProvider();
      
        private List<History> histories;
        private List<History> selectedHistories;
        private Nullable<DateTime> selectFromDateTime = null;
        private Nullable<DateTime> selectToDateTime = null;
        public List<History> Histories
        {
            get { return histories; }
            set => SetProperty(ref histories, value);
        }

        public List <History> SelectedHistories
        {
            get { return selectedHistories; }
            set => SetProperty(ref selectedHistories, value);
        }
        public Nullable<DateTime> SelectFromDateTime
        {
            get
            {
                if (selectFromDateTime == null) selectFromDateTime = DateTime.Now;

                return selectFromDateTime;
            }
            set => SetProperty(ref selectFromDateTime, value);
        }

        public Nullable<DateTime> SelectToDateTime
        {
            get
            {
                if (selectToDateTime == null) selectToDateTime = DateTime.Now;

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
            get { return (this.readHistoriesCommand) ?? (this.readHistoriesCommand = new Command((obj) => { ReadHistories(); })); }
        }

        public void ReadHistories()
        {
            Histories = hp.AddHistory();
           // SelectedHistories = hp.SelectHistory();
        }

        public SelectHistoryPageViewModel()
        {
      
            MainPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Main));

            NextPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.ConfirmRequestInfo));

            PreviousPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.SelectCert));
        }
    }
}
