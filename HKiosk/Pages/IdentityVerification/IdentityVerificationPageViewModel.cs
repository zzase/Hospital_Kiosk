using CefSharp;
using CefSharp.Wpf;
using HKiosk.Base;
using HKiosk.Manager.Data;
using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HKiosk.Pages.IdentityVerification
{
    public class IdentityVerificationPageViewModel : PropertyChange
    {
        public ICommand NextPageCommand { get; }

        public IdentityVerificationPageViewModel()
        {
            NextPageCommand = new Command((obj) =>
            {
                DataManager.Instance.PatientInfo.Name = "김종길";
                DataManager.Instance.PatientInfo.Birth = "770120";
                NavigationManager.Navigate(PageElement.ConfirmUserInfo);
            });
        }

        public Task<string> GetElementById(ChromiumWebBrowser browser, string id)
        {
            string script = $"document.getElementById('{id}').innerHTML;";

            return browser.EvaluateScriptAsync(script).ContinueWith(x =>
            {
                var response = x.Result;

                if (response.Success && response.Result != null)
                {
                    var startDate = response.Result;
                    return response.Result.ToString();
                }
                else
                {
                    return string.Empty;
                }
            });
        }
    }
}
