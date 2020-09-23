using HKiosk.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKiosk.Manager.Data
{
    public class DataManager : Singleton<DataManager>
    {
        public SettingInfo SettingInfo { get; set; }
        public PatientInfo PatientInfo { get; set; }
        public List<Job> Jobs { get; set; }
        public CertInfo CertInfo { get; set; }
        public PaymentInfo PaymentInfo { get; set; }
    }
}
