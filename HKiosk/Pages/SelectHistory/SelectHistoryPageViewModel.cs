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
        public List<History> Histories
        {
            get { return histories; }
            set => SetProperty(ref histories, value);
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
            Histories = hp.AddHistory();
        }

        public SelectHistoryPageViewModel()
        {

            MainPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Main));

            NextPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.ConfirmRequestInfo));

            PreviousPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.SelectCert));
        }
    }
}
