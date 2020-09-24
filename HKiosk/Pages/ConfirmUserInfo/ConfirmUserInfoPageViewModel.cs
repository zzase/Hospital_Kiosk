using HKiosk.Base;
using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System.Windows.Input;
using System.Security;
using System;
using System.Runtime.InteropServices;
using HKiosk.Util.Server;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using HKiosk.Controls.Popup;
using HKiosk.Manager.Data;

namespace HKiosk.Pages.ConfirmUserInfo
{
    class ConfirmUserInfoPageViewModel : PropertyChange
    {
        private string name;
        private string frontNationNo;
        private PopupViewModel popupViewModel = new PopupViewModel();

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

        public PopupViewModel PopupViewModel
        {
            get => popupViewModel;
            set => SetProperty(ref popupViewModel, value);
        }

        public ConfirmUserInfoPageViewModel()
        {
            MainPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Main));

            CheckUserInfoCommand = new Command(async (obj) =>
            {
                if (!CheckValidation())
                {
                    PopupViewModel.Show("정보를 입력해주세요.");

                    return;
                }

                if (await ExistPatientInfo())
                    NavigationManager.Navigate(PageElement.SelectCert);
            });
        }

        private async Task<bool> ExistPatientInfo()
        {
            var resultJson = await PatNoRequest(Name, BackNationNo);

            if (resultJson == null) return false;

            if (resultJson["resultCode"]?.ToString() != "200")
            {
                if (resultJson["resultCode"]?.ToString() == "400")
                    PopupViewModel.Show(resultJson["resultMessage"]?.ToString());

                return false;
            }

            DataManager.Instance.PatientInfo.Name = Name;
            DataManager.Instance.PatientInfo.Birth = FrontNationNo;
            DataManager.Instance.PatientInfo.PatientNo = resultJson["uPatNo"]?.ToString();

            return true;
        }

        private bool CheckValidation()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return false;

            if (string.IsNullOrWhiteSpace(FrontNationNo))
                return false;

            if (IsSecureStringNullOrEmpty(BackNationNo))
                return false;

            return true;
        }

        private bool IsSecureStringNullOrEmpty(SecureString secureString)
        {
            IntPtr unmanagedString = IntPtr.Zero;

            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return $"{Marshal.PtrToStringUni(unmanagedString)}" == string.Empty ? true : false;
            }
            catch (Exception ex)
            {
                Log.Write($"[ConfirmUserInfoPageViewModel] IsSecureStringNullOrEmpty Error : {ex}");
                return true;
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        private async Task<JObject> PatNoRequest(string Name, SecureString secureString)
        {
            IntPtr unmanagedString = IntPtr.Zero;

            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return await RequestAPI.PatNoRequest_Test(Name, $"{FrontNationNo}{Marshal.PtrToStringUni(unmanagedString)}");
            }
            catch (Exception ex)
            {
                Log.Write($"[ConfirmUserInfoPageViewModel] PatNoRequest Error : {ex}");
                return null;
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
