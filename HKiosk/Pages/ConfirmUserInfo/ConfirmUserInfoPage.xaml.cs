﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HKiosk.Pages.ConfirmUserInfo
{
    /// <summary>
    /// ConfirmUserInfoPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ConfirmUserInfoPage : Page
    {
        public ConfirmUserInfoPage()
        {
            InitializeComponent();

            nameTextBox.Focus();
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
                ((ConfirmUserInfoPageViewModel)this.DataContext).BackNationNo = ((PasswordBox)sender).SecurePassword;
        }
    }
}
