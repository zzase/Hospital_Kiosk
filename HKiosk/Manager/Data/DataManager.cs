using HKiosk.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace HKiosk.Manager.Data
{
    public class DataManager : Singleton<DataManager>
    {
        public SettingInfo SettingInfo { get; set; }
        public PatientInfo PatientInfo { get; set; }
        public List<Job> Jobs { get; set; }
        public ObservableCollection<CertRequestInfo> CertRequestInfos { get; set; }
        public CertRequestInfo CertRequestInfo { get; set; }
        public PaymentInfo PaymentInfo { get; set; }
        public string FinalPrice { get; set; }

        public DataManager()
        {
            InitData();
        }

        public void InitData()
        {
            SettingInfo = new SettingInfo();
            PatientInfo = new PatientInfo();
            Jobs = null;
            CertRequestInfos = new ObservableCollection<CertRequestInfo>();
            CertRequestInfo = new CertRequestInfo();
            PaymentInfo = new PaymentInfo();
            FinalPrice = null;
        }
    }
}
