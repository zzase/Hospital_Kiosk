using HKiosk.Base;
using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System.Windows;
using System.Windows.Input;
using HKiosk.Pages.SelectCert;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;

namespace HKiosk.Pages.ConfirmRequestInfoPage
{
    class ConfirmRequestInfoPageViewModel : PropertyChange
    {
        private readonly RequestInfoProvider requestInfoProvider;
        private ObservableCollection<RequestInfo> requestInfoList;
        public ObservableCollection<RequestInfo> RequestInfoList
        {
            get { requestInfoList = requestInfoProvider.requestInfoList; return requestInfoList; }
            set => SetProperty(ref requestInfoList, value);
        }
        private string finalPrice;
        
        public string FinalPrice
        {
            get { finalPrice = requestInfoProvider.finalPriceToString; return finalPrice; }
            set => SetProperty(ref finalPrice, value);
        }
        public ICommand MainPageCommand { get; }
        public ICommand PlusCertCommand { get; }
        public ICommand PaymentCommand { get; }

        private ICommand isCancelCommand;
        public ICommand IsCancelCommand
        {
            get { return (this.isCancelCommand) ?? (this.isCancelCommand = new Command((obj) => { CheckProcess(); })); }
        }


        private void CheckProcess()
        {
            RequestInfoList = requestInfoProvider.DeleteRequestInfo();
            FinalPrice = requestInfoProvider.CalcFinalPrice();
        }

        public ConfirmRequestInfoPageViewModel()
        {
            requestInfoProvider = new RequestInfoProvider();
            RequestInfoList = requestInfoProvider.AddRequestInfo();
            FinalPrice = requestInfoProvider.CalcFinalPrice();
            
            MainPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Main));

            PlusCertCommand = new Command((obj) => NavigationManager.Navigate(PageElement.SelectCert));

            PaymentCommand = new Command((obj) => NavigationManager.Navigate(PageElement.SelectPayment));

        }
    }
}
