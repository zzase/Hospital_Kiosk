using HKiosk.Base;
using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System.Windows.Input;
using HKiosk.Pages.SelectCert;
using System.Collections.Generic;
using System.Security;
using System;
using System.Runtime.InteropServices;

namespace HKiosk.Pages.ConfirmUserInfo
{
    class ConfirmUserInfoPageViewModel : PropertyChange
    {
        private string name;
        private string frontNationNo;

        public ICommand MainPageCommand { get; }
        public ICommand CheckUserInfoCommand { get; }

        public string Name 
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string FrontNationNo
        {
            get => frontNationNo;
            set => SetProperty(ref frontNationNo, value);
        }

        public SecureString BackNationNo { private get; set; }

        private List<Job> certs;
        public List<Job> Certs
        {
            get => certs;
            set => SetProperty(ref certs, value);
        }

        public ConfirmUserInfoPageViewModel()
        {
            MainPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Main));

            CheckUserInfoCommand = new Command((obj) =>
            {
                IntPtr unmanagedString = IntPtr.Zero;

                try
                {
                    unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(BackNationNo);
                    Console.WriteLine($"str : {Marshal.PtrToStringUni(unmanagedString)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex}");
                }
                finally
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
                    BackNationNo?.Dispose();
                }

                NavigationManager.Navigate(PageElement.SelectCert);
            });
        }
    }
}
