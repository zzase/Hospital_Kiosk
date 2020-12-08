using HKiosk.Util;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace HKiosk.Manager.Data
{
    public class DataManager : Singleton<DataManager>
    {
        public SettingInfo SettingInfo { get; set; }
        public PatientInfo PatientInfo { get; set; }
        public List<Job> Jobs { get; set; }
        public Job SelectedJob { get; set; }
        public ObservableCollection<CertRequestInfo> CertRequestInfos { get; set; }
        public PaymentInfo PaymentInfo { get; set; }
        public CancellationTokenSource CancellationTokenSource { get; private set; }
        public string FinalPrice { get; set; }
        public string FailText { get; set; }
        public string FailReason { get; set; }

        public void InitData()
        {
            CancellationTokenSource?.Cancel();

            SettingInfo = new SettingInfo();
            PatientInfo = new PatientInfo();
            Jobs = null;
            SelectedJob = null;
            CertRequestInfos = new ObservableCollection<CertRequestInfo>();
            PaymentInfo = new PaymentInfo();
            CancellationTokenSource = new CancellationTokenSource();
            FinalPrice = "";
            PaymentInfo.OrderNo = "";
            SettingInfo.GiwanNo = "HG0002";
        }

        public DataManager()
        {
            InitData();
        }
    }
}
