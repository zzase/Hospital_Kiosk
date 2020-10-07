using HKiosk.Base;
using HKiosk.Manager.Navigation;
using HKiosk.Manager.Popup;
using HKiosk.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HKiosk.Pages.Payment.PhonePaymentPage
{
    class InfoInputPageViewModel : PropertyChange
    {
        public ICommand MainPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }

        public InfoInputPageViewModel()
        {
            MainPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Main));
            PreviousPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Agreement));
            NextPageCommand = new Command((obj) =>
            {
                    NavigationManager.Navigate(PageElement.ApprovalNumber);
            });
        }
    }
}
