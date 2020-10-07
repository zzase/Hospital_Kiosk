using HKiosk.Base;
using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HKiosk.Pages.Payment
{
    public class SelectPaymentPageViewModel : PropertyChange
    {
        public ICommand NavigateCardPaymentPageCommand { get; }
        public ICommand NavigatePhonePaymentPageCommand { get; }
        public ICommand NavigateCashbeePaymentPageCommand { get; }
        public ICommand NavigateTmoneyPaymentPageCommand { get; }
        public ICommand MainPageCommand { get; }
        public ICommand PreviousPageCommand { get; }

        public SelectPaymentPageViewModel()
        {
            NavigateCardPaymentPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.CardPayment));
            NavigatePhonePaymentPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Agreement));
            NavigateCashbeePaymentPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.CashbeePayment));
            NavigateTmoneyPaymentPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.TmoneyPayment));
            MainPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Main));
            PreviousPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.ConfirmRequestInfo));
        }
    }
}
