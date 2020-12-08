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

        public string Name
        {
            get => DataManager.Instance.PatientInfo.Name;
        }
        public string PatientNo
        {
            get => DataManager.Instance.PatientInfo.PatientNo;
        }

        private async void InitJobs()
        {
            PopupManager.Instance[PopupElement.Loding].Show("발급 가능한 증명서를 가져오는 중입니다.\n잠시만 기다려주세요.");

            var data = await RequestAPI.JobListRequest();

            PopupManager.Instance[PopupElement.Loding].Hide();

            if (data == null)
            {
                PopupManager.Instance[PopupElement.Alert]?.Show("서버에 일시적인 오류가 발생했습니다. 재시도 부탁드립니다.", NavigateMainPage);
                return;
            }

            if (data["resultCode"]?.ToString() == "200")
            {
                try
                {
                    DataManager.Instance.Jobs = RequestAPI.JArrayToList<Job>(data["list"]?.Value<JArray>());
                }
                catch (Exception ex)
                {
                    PopupManager.Instance[PopupElement.Alert]?.Show("서버에 일시적인 오류가 발생했습니다. 재시도 부탁드립니다.", NavigateMainPage);
                    Log.Write($"[SelectCertPageViewModel] InitJobs exception : {ex}");
                    return;
                }

                if ((DataManager.Instance.Jobs?.Count ?? 0) < 1)
                {
                    PopupManager.Instance[PopupElement.Alert].Show("발급 가능한 증명서가 없습니다.", NavigateMainPage);
                    return;
                }

                OnPropertyChanged("Jobs");
            }
            else
            {
                var resultMessage = data["resultMessage"]?.ToString();

                if (string.IsNullOrWhiteSpace(resultMessage))
                {
                    resultMessage = "서버에 일시적인 오류가 발생했습니다. 재시도 부탁드립니다.";
                }

                PopupManager.Instance[PopupElement.Alert].Show(resultMessage, NavigateMainPage);
            }
        }

        public SelectCertPageViewModel()
        {
            MainPageCommand = new Command((obj) => NavigateMainPage());

            if (DataManager.Instance.Jobs == null)
                InitJobs();
        }

        private void NavigateMainPage()
        {
            DataManager.Instance.InitData();
            NavigationManager.Navigate(PageElement.Main);
        }
    }
}
