using HKiosk.Base;
using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System.Windows.Input;

namespace HKiosk.Pages.Main
{
    public class MainPageViewModel : PropertyChange
    {
        public ICommand NextPageCommand { get; }

        public MainPageViewModel()
        {
            NextPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.IdentityVerification));
        }
    }
}
