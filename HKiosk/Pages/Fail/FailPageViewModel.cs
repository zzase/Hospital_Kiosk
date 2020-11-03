using HKiosk.Base;
using HKiosk.Manager.Data;
using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HKiosk.Pages.Fail
{
    class FailPageViewModel : PropertyChange
    {
        private string failText;
        private string failReason;

        public ICommand MainPageCommand { get; }
        public ICommand ChangePaymentCommand { get; }
        public ICommand RePaymentCommand { get; }

        public string FailText
        {
            get => failText;
            set => SetProperty(ref failText, value);
        }
        public string FailReason
        {
            get => failReason;
            set => SetProperty(ref failReason, value);
        }

        public FailPageViewModel()
        {
            FailText = DataManager.Instance.FailText;
            FailReason = DataManager.Instance.FailReason;

            MainPageCommand = new Command((obj) =>
            {
                DataManager.Instance.InitData();
                NavigationManager.Navigate(PageElement.Main);
            });

            ChangePaymentCommand = new Command((obj) =>
            {
                NavigationManager.Navigate(PageElement.SelectPayment);
            });

            RePaymentCommand = new Command((obj) =>
            {
                NavigationManager.Navigate(PageElement.InfoInput);
            });
        }
    }
}
