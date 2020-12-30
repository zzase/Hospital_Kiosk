using HKiosk.Base;
using HKiosk.Manager.Navigation;
using HKiosk.Manager.Popup;
using HKiosk.Util;
using HKiosk.Util.Server;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HKiosk.Manager.Data
{
    public class Job : PropertyChange
    {
        private ObservableCollection<SujinHistroy> sujinHistories = new ObservableCollection<SujinHistroy>();

        private bool needSujinHistory;
        public string GiwanNo { get; set; }
        public string CertCd { get; set; }    // 증명서 코드
        public string CertNe { get; set; }    // 증명서명
        public string HosCertCd { get; set; }     // 병원연계 코드
        public string Price { get; set; }     // 수수료
        public string KorYN { get; set; }    // Y(한글), N(영문)
        public bool NeedSujinHistory
        {
            get => needSujinHistory;
            set => SetProperty(ref needSujinHistory, value);
        }
        public ICommand SelectCommand { get; }    // 선택 커맨드
   
        public ObservableCollection<SujinHistroy> SujinHistories
        {
            get => sujinHistories;
            set => SetProperty(ref sujinHistories, value);
        }
        public Job()
        {
            SelectCommand = new Command(async (obj) =>
            {
                DataManager.Instance.SelectedJob = this;
                //NavigationManager.Navigate(PageElement.SelectHistory);
                if(await ReadHistories())
                {
                    NavigationManager.Navigate(PageElement.SelectHistory);
                }
                else
                    NavigationManager.Navigate(PageElement.SelectDetail);
            });
        }

        public async Task<bool> ReadHistories()
        {

            PopupManager.Instance[PopupElement.Loding].Show("수진이력을 확인하고 있습니다.\n잠시만 기다려주세요.");

            var data = await RequestAPI.CheckSujinHistory(GiwanNo, CertCd);

            PopupManager.Instance[PopupElement.Loding].Hide();

            if (data == null)
            {
                PopupManager.Instance[PopupElement.Alert]?.Show("서버에 일시적인 오류가 발생했습니다. 재시도 부탁드립니다.");
                return false;
            }

            if (false) //if (data["resultCode"]?.ToString() != "200")
            {
                var resultMessage = data["resultMessage"]?.ToString();

                DataManager.Instance.SujinHistroys = SujinHistories;
                if (string.IsNullOrWhiteSpace(resultMessage))
                {
                    resultMessage = "서버에 일시적인 오류가 발생했습니다. 재시도 부탁드립니다.";
                }

                PopupManager.Instance[PopupElement.Alert]?.Show(resultMessage);

                return false;
            }
            else
            {
                if (data["needForm"].ToString().Equals("true"))
                {
                    DataManager.Instance.SelectedJob.NeedSujinHistory = true;
                    return true;
                }
                else
                {
                    var date = await RequestAPI.CertSujinRequest();
                    try
                    {
                        SujinHistories = new ObservableCollection<SujinHistroy>(RequestAPI.JArrayToList<SujinHistroy>(date["list"]?.Value<JArray>()));
                        for (int i = 0; i < SujinHistories.Count; i++)
                        {
                            SujinHistories[i].InDate = FormatStringToDate(SujinHistories[i].InDate);
                            SujinHistories[i].OutDate = FormatStringToDate(SujinHistories[i].OutDate);
                        }
                        DataManager.Instance.SujinHistroys = SujinHistories;
                        DataManager.Instance.SelectedJob.NeedSujinHistory = false;
                    }
                    catch (Exception ex)
                    {
                        PopupManager.Instance[PopupElement.Alert]?.Show("서버에 일시적인 오류가 발생했습니다. 재시도 부탁드립니다.");
                        Log.Write($"[SelectHistoryPageViewModel] ReadHistories exception : {ex}");
                    }

                    if ((SujinHistories?.Count ?? 0) < 1)
                    {
                        PopupManager.Instance[PopupElement.Alert].Show("수진이력이 없습니다.");
                    }

                    return false;
                }

            }
        }
    
        
        public static string FormatStringToDate(string tdrDate)
        {
            if (tdrDate != null)
            {
                if (tdrDate.Length > 4)
                {
                    return DateTime.ParseExact(tdrDate, "yyyyMMdd", null).ToString("yyyy.MM.dd");
                }
                else if (tdrDate.Length == 4)
                {
                    return tdrDate + "년";
                }
                else
                    return tdrDate;

            }
            else return tdrDate;


        }
    }
}
