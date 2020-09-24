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
        private readonly RequestInfoProvider requestInfoProvider;

        public List<RequestInfo> RequestInfoList
        {
            get { return requestInfoProvider.RequestInfoList; }
        }
        public ICommand MainPageCommand { get; }
        public ICommand PlusCertCommand { get; }
        public ICommand PaymentCommand { get; }

        public ConfirmRequestInfoPageViewModel()
        {
            requestInfoProvider = new RequestInfoProvider();

            MainPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Main));

            PlusCertCommand = new Command((obj) => NavigationManager.Navigate(PageElement.SelectCert));

            PaymentCommand = new Command((obj) => NavigationManager.Navigate(PageElement.SelectPayment));

        }
    }
}
