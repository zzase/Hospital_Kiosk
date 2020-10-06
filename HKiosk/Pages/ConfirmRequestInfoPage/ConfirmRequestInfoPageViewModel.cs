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
using HKiosk.Manager.Popup;

namespace HKiosk.Pages.ConfirmRequestInfoPage
{
    public class ConfirmRequestInfoPageViewModel : PropertyChange
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
            for (int i = 0; i < DataManager.Instance.CertRequestInfos.Count;)
            {
                if (DataManager.Instance.CertRequestInfos[i].IsCheckedForCancel)
                {
                    DataManager.Instance.CertRequestInfos.RemoveAt(i);
                }
                else i++;
            }
            DataManager.Instance.FinalPrice = requestInfoProvider.CalcFinalPrice();
            FinalPrice = DataManager.Instance.FinalPrice;
        }

        public ConfirmRequestInfoPageViewModel()
        {
            requestInfoProvider = new RequestInfoProvider();
            CertRequestInfos = DataManager.Instance.CertRequestInfos;

            DataManager.Instance.FinalPrice = requestInfoProvider.CalcFinalPrice();
            FinalPrice = DataManager.Instance.FinalPrice;

            MainPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Main));

            PlusCertCommand = new Command((obj) => NavigationManager.Navigate(PageElement.SelectCert));

            PaymentCommand = new Command((obj) =>
            {
                if (DataManager.Instance.CertRequestInfos.Count == 0)
                    PopupManager.Instance[PopupElement.Alert]?.Show("발급 가능한 증명서 목록이 없습니다.\n증명서를 선택해주세요");
                else
                {
                    if (DataManager.Instance.FinalPrice.Equals("0원"))
                    {
                        NavigationManager.Navigate(PageElement.Print);
                    }
                    else
                        NavigationManager.Navigate(PageElement.SelectPayment);
                }
            });
        }
    }
}
