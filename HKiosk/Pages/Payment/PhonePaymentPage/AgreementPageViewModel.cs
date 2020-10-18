using HKiosk.Base;
using HKiosk.Manager.Data;
using HKiosk.Manager.Navigation;
using HKiosk.Manager.Popup;
using HKiosk.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HKiosk.Pages.Payment
{
    class AgreementPageViewModel : PropertyChange
    {
        private bool isChacked1;
        private bool isChacked2;
        private bool isChacked3;

        public ICommand MainPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand AgreeBtn1Command { get; }
        public ICommand AgreeBtn2Command { get; }
        public ICommand AgreeBtn3Command { get; }
        public bool IsChacked1
        {
            get => isChacked1;
            set => SetProperty(ref isChacked1, value);
        }
        public bool IsChacked2
        {
            get => isChacked2;
            set => SetProperty(ref isChacked2, value);
        }
        public bool IsChacked3
        {
            get => isChacked3;
            set => SetProperty(ref isChacked3, value);
        }

        public AgreementPageViewModel()
        {
            IsChacked1 = false;
            IsChacked2 = false;
            IsChacked3 = false;

            MainPageCommand = new Command((obj) =>
            {
                DataManager.Instance.InitData();
                NavigationManager.Navigate(PageElement.Main);
            });
            PreviousPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.SelectPayment));
            NextPageCommand = new Command((obj) =>
            {
                if(IsChacked1 && IsChacked2 && IsChacked3)
                    NavigationManager.Navigate(PageElement.InfoInput);
                else
                    PopupManager.Instance[PopupElement.Alert]?.Show("모든 약관에 동의해주세요.");
            });
            AgreeBtn1Command = new Command((obj) =>
            {
                IsChacked1 = !IsChacked1;
            });
            AgreeBtn2Command = new Command((obj) =>
            {
                IsChacked2 = !IsChacked2;
            });
            AgreeBtn3Command = new Command((obj) =>
            {
                IsChacked3 = !IsChacked3;
            });
        }
    }

}
