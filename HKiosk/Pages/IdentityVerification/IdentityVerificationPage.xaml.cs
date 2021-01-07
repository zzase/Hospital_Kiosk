using CefSharp;
using CefSharp.Wpf;
using HKiosk.Manager.Data;
using HKiosk.Manager.Navigation;
using System.Windows.Threading;

namespace HKiosk.Pages.IdentityVerification
{
    public partial class IdentityVerificationPage : System.Windows.Controls.Page
    {
        public IdentityVerificationPage()
        {
            InitializeComponent();

            InitBrowser("http://172.16.0.154:81/hp1.0/jsp/login/kioAuthfi.jsp");

            browser.FrameLoadEnd += async (s, e) =>
            {
                string name = await vm.GetElementById(browser, "sName");
                string birthDate = await vm.GetElementById(browser, "sBirthDate");
                string message = await vm.GetElementById(browser, "sMessage");

                if (!(string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(birthDate)))
                {
                    Dispatcher.Invoke(() =>
                    {
                        DataManager.Instance.PatientInfo.Name = name;
                        DataManager.Instance.PatientInfo.Birth = birthDate.Substring(2, 6);
                        NavigationManager.Navigate(PageElement.ConfirmUserInfo);
                    }, DispatcherPriority.Normal);
                }
            };
        }

        public void InitBrowser(string starturl)
        {
            if (!Cef.IsInitialized)
                Cef.Initialize(new CefSettings());

            browser.Address = starturl;
            browser.Height = 800;
            browser.Width = 600;
            
            browser.WpfKeyboardHandler = new CefSharp.Wpf.Experimental.WpfImeKeyboardHandler(browser);
        }
    }
}
