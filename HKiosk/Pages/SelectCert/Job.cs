using HKiosk.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKiosk.Pages.SelectCert
{
    class Job : PropertyChange
    {
        private string certCd;    // 증명서 코드
        private string certNe;    // 증명서명
        private string hosCertCd; // 병원연계 코드
        private string price;     // 수수료
        private string korYN;     // Y(한글),N(영문)


        public string CertCd 
        {
            get => certCd;
            set => SetProperty(ref certCd, value);
        }

        public string CertNe
        {
            get => certNe;
            set => SetProperty(ref certNe, value);
        }

        public string HostCertCd
        {
            get => hosCertCd;
            set => SetProperty(ref hosCertCd, value);
        }

        public string Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public string KorYN
        {
            get => korYN;
            set => SetProperty(ref korYN, value);
        }

      
    }
    
}
