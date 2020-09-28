using HKiosk.Base;
using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System.Windows.Input;
using System.Collections.Generic;
using HKiosk.Manager.Data;
using HKiosk.Util.Server;
using Newtonsoft.Json.Linq;
using HKiosk.Manager.Popup;

namespace HKiosk.Pages.SelectCert
{

    class SelectCertPageViewModel : PropertyChange
    {
        public List<Job> Jobs
        {
            get => DataManager.Instance.Jobs; 
        }

        public ICommand MainPageCommand { get; }

        public SelectCertPageViewModel()
        {
            MainPageCommand = new Command((obj) => NavigationManager.Navigate(PageElement.Main));

            if (DataManager.Instance.Jobs == null)
                InitJobs();
        }

        private async void InitJobs()
        {
            PopupManager.Popup[PopupElement.Loding].Show("발급 가능한 증명서를 가져오는 중입니다.\n잠시만 기다려주세요.");

            var data = await RequestAPI.JobListRequest_Test(1);

            PopupManager.Popup[PopupElement.Loding].Hide();

            if (data["resultCode"]?.ToString() == "200")
            {
                DataManager.Instance.Jobs = RequestAPI.JArrayToList<Job>(data["list"]?.Value<JArray>());
                OnPropertyChanged("Jobs");
            }
            else
            {
                PopupManager.Popup[PopupElement.Alert].Show(data["resultMessage"]?.ToString());
            }
        }
    }


}
