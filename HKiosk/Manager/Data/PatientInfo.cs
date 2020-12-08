using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKiosk.Manager.Data
{
    public class PatientInfo
    {
        public string Name { get; set; }    // 성명
        public string Birth { get; set; }    // 생년월일
        public string PatientNo { get; set; }    // 환자번호
        public string TPID { get; set; }    // 환자 고유 키값
    }
}
