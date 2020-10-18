using HKiosk.Base;
using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System.Windows.Input;
using System.Collections.Generic;
using HKiosk.Manager.Data;
using HKiosk.Util.Server;
using Newtonsoft.Json.Linq;
using HKiosk.Manager.Popup;
using System;

namespace HKiosk.Pages.SelectCert
{
    public class SelectCertPageViewModel : PropertyChange
    {
        public ICommand MainPageCommand { get; }

        public List<Job> Jobs
        {
            get => DataManager.Instance.Jobs;
        }

        private async void InitJobs()
        {
            PopupManager.Instance[PopupElement.Loding].Show("발급 가능한 증명서를 가져오는 중입니다.\n잠시만 기다려주세요.");

            var data = await RequestAPI.JobListRequest_Test(1);

            PopupManager.Instance[PopupElement.Loding].Hide();

            if (data["resultCode"]?.ToString() == "200")
            {
                DataManager.Instance.Jobs = RequestAPI.JArrayToList<Job>(data["list"]?.Value<JArray>());
                OnPropertyChanged("Jobs");
            }
            else
            {
                PopupManager.Instance[PopupElement.Alert].Show(data["resultMessage"]?.ToString());
            }
        }

        public SelectCertPageViewModel()
        {
            MainPageCommand = new Command((obj) =>
            {
                DataManager.Instance.InitData();
                NavigationManager.Navigate(PageElement.Main);
            });

            if (DataManager.Instance.Jobs == null)
                InitJobs();
        }
    }
}
