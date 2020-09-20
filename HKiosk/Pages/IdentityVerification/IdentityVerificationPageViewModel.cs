
using HKiosk.Base;
using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System.Windows.Input;
namespace HKiosk.Pages.IdentityVerification
{
    public class IdentityVerificationPageViewModel : PropertyChange
    {
        public ICommand NextPageCommand { get; }

        public IdentityVerificationPageViewModel()
        {
            NextPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.ConfirmUserInfo));
        }
    }
}
