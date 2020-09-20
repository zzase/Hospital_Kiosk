using HKiosk.Base;
using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System.Windows.Input;
using HKiosk.Pages.SelectCert;
using System.Collections.Generic;

namespace HKiosk.Pages.SelectCert 
{

    class SelectCertPageViewModel : PropertyChange
    {
        JobFactory jobFactory = new JobFactory();

        private List<Job> certs;
        public List<Job> Certs
        {
            get => certs;
            set => SetProperty(ref certs, value);
        }

        public int certCount;
        public int Certcount
        {
            get => certCount;
            set => SetProperty(ref certCount, value);
        }

        private Job selectedJob;

        public Job SelectedJob
        {
            get { return selectedJob; }
            set => SetProperty(ref selectedJob, value);
        }

        

        private ICommand readCertCommand;
        public ICommand ReadCertCommand
        {
            get { return (this.readCertCommand) ?? (this.readCertCommand = new DelegateCommand(ReadCert)); }
        }

        private ICommand readCertCountCommand;
        public ICommand ReadCertCountCommand
        {
            get { return (this.readCertCountCommand) ?? (this.readCertCountCommand = new DelegateCommand(getCertsLength)); }
        }

        public ICommand MainPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }

        public void ReadCert()
        {
            Certs = jobFactory.GetAllCerts();

        }

        public void getCertsLength()
        {
            certCount = certs.Count*30;
        }


        public SelectCertPageViewModel()
        {
            MainPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Main));

            NextPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.SelectHistory));

            PreviousPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.ConfirmUserInfo));

        }
    }

   
}
