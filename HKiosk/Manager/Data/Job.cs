using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKiosk.Manager.Data
{
    public class Job
    {
        public string CertCd { get; set; }    // 증명서 코드
        public string CertNe { get; set; }    // 증명서명
        public string HosCertCd { get; set; }     // 병원연계 코드
        public string Price { get; set; }     // 수수료
        public string KorYN { get; set; }    // Y(한글), N(영문)
        public SujinHistroy SujinHistroy { get; set; }    // 수진 이력
    }
}
