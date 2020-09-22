using HKiosk.Base;
using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System.Windows.Input;
using HKiosk.Pages.SelectCert;
using System.Collections.Generic;

namespace HKiosk.Pages.ConfirmUserInfo
{
    class ConfirmUserInfoPageViewModel : PropertyChange
    {
        public ICommand MainPageCommand { get; }
        public ICommand CheckUserInfoCommand { get; }

        JobFactory jobFactory = new JobFactory();

        private List<Job> certs;
        public List<Job> Certs
        {
            get => certs;
            set => SetProperty(ref certs, value);
        }

        private ICommand readCertCommand;
        public ICommand ReadCertCommand
        {
            get { return (this.readCertCommand) ?? (this.readCertCommand = new DelegateCommand(ReadCert)); }
        }

        public void ReadCert()
        {
           // Certs = jobFactory.GetAllCerts();

        }
        public ConfirmUserInfoPageViewModel()
        {
            MainPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Main));

            CheckUserInfoCommand = new Command((obj) => NavigationManager.Navigate(PageElement.SelectCert));
        }



    }
}
