using HKiosk.Base;
using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System.Windows;
using System.Windows.Input;
using HKiosk.Pages.SelectCert;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using HKiosk.Manager.Data;
using System.Globalization;

namespace HKiosk.Pages.ConfirmRequestInfoPage
{
    class ConfirmRequestInfoPageViewModel : PropertyChange
    {
        private readonly RequestInfoProvider requestInfoProvider;
        private ObservableCollection<CertRequestInfo> certRequestInfos;
        public ObservableCollection<CertRequestInfo> CertRequestInfos
        {
            get 
            {
                return certRequestInfos; 
            }

            set => SetProperty(ref certRequestInfos, value);
        }

        private string finalPrice;
        
        public string FinalPrice
        {
            get
            {
                finalPrice = DataManager.Instance.FinalPrice;
                return finalPrice;
            }
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
            for(int i=0; i<DataManager.Instance.CertRequestInfos.Count; i++)
            {
                if (DataManager.Instance.CertRequestInfos[i].IsCheckedForCancel)
                {
                    DataManager.Instance.CertRequestInfos.RemoveAt(i);
                }
            }
            DataManager.Instance.FinalPrice = requestInfoProvider.CalcFinalPrice();
            FinalPrice = DataManager.Instance.FinalPrice;
        }

        public ConfirmRequestInfoPageViewModel()
        {
            requestInfoProvider = new RequestInfoProvider();

            DataManager.Instance.CertRequestInfos.Add(DataManager.Instance.CertRequestInfo);
            DataManager.Instance.FinalPrice = requestInfoProvider.CalcFinalPrice();
            CertRequestInfos = DataManager.Instance.CertRequestInfos;
            FinalPrice = DataManager.Instance.FinalPrice;

            MainPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Main));

            PlusCertCommand = new Command((obj) => NavigationManager.Navigate(PageElement.SelectCert));

            PaymentCommand = new Command((obj) => NavigationManager.Navigate(PageElement.SelectPayment));

        }
    }
}
