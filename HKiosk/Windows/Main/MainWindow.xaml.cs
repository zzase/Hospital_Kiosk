using HKiosk.Base;
using HKiosk.Controls.NavigationBar;
using HKiosk.Manager.Navigation;
using HKiosk.Pages.IdentityVerification;
using HKiosk.Pages.Main;
using HKiosk.Pages.ConfirmUserInfo;
using HKiosk.Pages.SelectCert;
using HKiosk.Pages.SelectHistory;
using HKiosk.Pages.ConfirmRequestInfoPage;
using HKiosk.Util;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.ComponentModel;
using HKiosk.Pages.Payment;

namespace HKiosk.Windows.Main
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window, INavigation
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel(this);

            Navigate(PageElement.Main);
        }

        SelectCertPageViewModel svm = new SelectCertPageViewModel();


        public void Navigate(PageElement page)
        {
            var vm = this.DataContext as MainWindowViewModel;

            Page pageToNavigate = null;

            switch (page)
            {
                case PageElement.Main:
                    pageToNavigate = new MainPage();
                    vm?.MoveNavigationBar(NaviElement.Main);
                    break;

                case PageElement.IdentityVerification:
                    pageToNavigate = new IdentityVerificationPage();
                    vm?.MoveNavigationBar(NaviElement.IdentityVerification);
                    break;

                case PageElement.ConfirmUserInfo:
                    pageToNavigate = new ConfirmUserInfoPage();
                    vm?.MoveNavigationBar(NaviElement.ConfirmUserInfo);
                    break;

                case PageElement.SelectCert:
                    pageToNavigate = new SelectCertPage();

                    vm?.MoveNavigationBar(NaviElement.SelectCert);
                    break;

                case PageElement.SelectHistory:
                    pageToNavigate = new SelectHistoryPage();

                    vm?.MoveNavigationBar(NaviElement.SelectHistory);
                    break;

                case PageElement.ConfirmRequestInfo:
                    pageToNavigate = new ConfirmRequestInfoPage();
                    vm?.MoveNavigationBar(NaviElement.ConfirmRequestInfo);
                    break;

                case PageElement.SelectPayment:
                    pageToNavigate = new SelectPaymentPage();
                    vm?.MoveNavigationBar(NaviElement.Payment);
                    break;

                case PageElement.CardPayment:
                    pageToNavigate = new CardPaymentPage();
                    vm?.MoveNavigationBar(NaviElement.Payment);
                    break;

                case PageElement.PhonePayment:
                    pageToNavigate = new PhonePaymentPage();
                    vm?.MoveNavigationBar(NaviElement.Payment);
                    break;

                case PageElement.CashbeePayment:
                    pageToNavigate = new CashbeePaymentPage();
                    vm?.MoveNavigationBar(NaviElement.Payment);
                    break;

                case PageElement.TmoneyPayment:
                    pageToNavigate = new TmoneyPaymentPage();
                    vm?.MoveNavigationBar(NaviElement.Payment);
                    break;

                case PageElement.SelectIssuanceMethod:
                    break;

                case PageElement.Print:
                    break;

                case PageElement.Fax:
                    break;

                case PageElement.Mail:
                    break;
            }

            if (pageToNavigate != null)
                frame.Navigate(pageToNavigate);
        }

        private async void Panel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var touchPos = e.GetPosition(panel);
            var clickEffect = ClickEffect.Create(touchPos);

            panel.Children.Add(clickEffect);
            panel.Children.Remove(await ClickEffect.Animate(clickEffect));
        }
    }
}
