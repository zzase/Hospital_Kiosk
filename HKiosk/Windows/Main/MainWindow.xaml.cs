﻿using HKiosk.Base;
using HKiosk.Controls.NavigationBar;
using HKiosk.Manager.Navigation;
using HKiosk.Pages.IdentityVerification;
using HKiosk.Pages.Main;
using HKiosk.Pages.ConfirmUserInfo;
using HKiosk.Pages.SelectCert;
using HKiosk.Pages.SelectHistory;
using HKiosk.Pages.SelectDetail;
using HKiosk.Pages.ConfirmRequestInfoPage;
using HKiosk.Pages.Print;
using HKiosk.Pages.Payment;
using HKiosk.Pages.Payment.PhonePaymentPage;
using HKiosk.Pages.Fail;
using HKiosk.Util;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HKiosk.Manager.Timer;

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
                    TimerManager.Timer.Start(300);
                    break;

                case PageElement.SelectCert:
                    pageToNavigate = new SelectCertPage();
                    vm?.MoveNavigationBar(NaviElement.SelectCert);
                    TimerManager.Timer.Start(300);
                    break;

                case PageElement.SelectHistory:
                    pageToNavigate = new SelectHistoryPage();
                    vm?.MoveNavigationBar(NaviElement.SelectHistory);
                    TimerManager.Timer.Start(300);
                    break;

                case PageElement.SelectDetail:
                    pageToNavigate = new SelectDetailPage();
                    vm?.MoveNavigationBar(NaviElement.SelectDetail);
                    TimerManager.Timer.Start(300);
                    break;

                case PageElement.ConfirmRequestInfo:
                    pageToNavigate = new ConfirmRequestInfoPage();
                    vm?.MoveNavigationBar(NaviElement.ConfirmRequestInfo);
                    break;

                case PageElement.SelectPayment:
                    pageToNavigate = new SelectPaymentPage();
                    vm?.MoveNavigationBar(NaviElement.Payment);
                    TimerManager.Timer.Start(300);
                    break;

                case PageElement.CardPayment:
                    pageToNavigate = new CardPaymentPage();
                    vm?.MoveNavigationBar(NaviElement.Payment);
                    break;

                case PageElement.Agreement:
                    pageToNavigate = new AgreementPage();
                    vm?.MoveNavigationBar(NaviElement.Payment);
                    break;

                case PageElement.InfoInput:
                    pageToNavigate = new InfoInputPage();
                    vm?.MoveNavigationBar(NaviElement.Payment);
                    break;

                case PageElement.ApprovalNumber:
                    pageToNavigate = new ApprovalNumberPage();
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
                    pageToNavigate = new PrintPage();
                    vm?.MoveNavigationBar(NaviElement.Print);
                    break;

                case PageElement.PrintSuccess:
                    pageToNavigate = new PrintSuccessPage();
                    vm?.MoveNavigationBar(NaviElement.Print);
                    TimerManager.Timer.Start(300);
                    break;

                case PageElement.Fail:
                    pageToNavigate = new FailPage();
                    vm?.MoveNavigationBar(NaviElement.Payment);
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
