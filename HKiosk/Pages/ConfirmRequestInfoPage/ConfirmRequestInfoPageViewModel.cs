using HKiosk.Base;
using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System.Windows.Input;
using HKiosk.Pages.SelectCert;
using System.Collections.Generic;

namespace HKiosk.Pages.ConfirmRequestInfoPage
{
    class ConfirmRequestInfoPageViewModel : PropertyChange
    {
        public ICommand MainPageCommand { get; }
        public ICommand PlusCertCommand { get; }
        public ICommand PaymentCommand { get; }

        public ConfirmRequestInfoPageViewModel()
        {

            MainPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Main));

            PlusCertCommand = new Command((obj) => NavigationManager.Navigate(PageElement.SelectCert));

            PaymentCommand = new Command((obj) => NavigationManager.Navigate(PageElement.SelectPayment));

        }
    }
}
