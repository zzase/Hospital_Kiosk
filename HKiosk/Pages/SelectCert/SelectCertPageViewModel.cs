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
        private readonly JobFactory jobFactory;

       
        public List<Job> Jobs
        {
            get { return jobFactory.Certs; }
        }

        public ICommand MainPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }


       

        public SelectCertPageViewModel()
        {
            jobFactory = new JobFactory();
 
            MainPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Main));

            NextPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.SelectHistory));

            PreviousPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.ConfirmUserInfo));

        }
    }


}
