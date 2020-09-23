using HKiosk.Base;
using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System.Windows.Input;
using HKiosk.Pages.SelectCert;
using System.Collections.Generic;

namespace HKiosk.Pages.SelectHistory 
{
    class SelecyHistoryPageViewModel : PropertyChange
    {
        public ICommand MainPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }

        public SelecyHistoryPageViewModel()
        {
            MainPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Main));

            NextPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.ConfirmRequestInfo));

            PreviousPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.SelectCert));
        }
    }
}
