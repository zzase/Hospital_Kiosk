using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKiosk.Pages.SelectCert;

namespace HKiosk.Pages.SelectCert
{
    class JobFactory
    {
        List<Job> certs = new List<Job>();
        
        public List<Job> GetAllCerts()
        {
            certs.Add(new Job() { CertCd = "0001", CertNe = "진단서 사본", HostCertCd = "CA006", Price = "1000원", KorYN = "Y" });
            certs.Add(new Job() { CertCd = "0002", CertNe = "소견서 사본", HostCertCd = "CA006", Price = "1000원", KorYN = "Y" });
            certs.Add(new Job() { CertCd = "0003", CertNe = "입퇴원확인서", HostCertCd = "CA006", Price = "1000원", KorYN = "Y" });
            certs.Add(new Job() { CertCd = "0004", CertNe = "세부내역서(입원,외래,응급)", HostCertCd = "CA006", Price = "1000원", KorYN = "Y" });
            certs.Add(new Job() { CertCd = "0005", CertNe = "진료비 영수증(입원,외래,응급)", HostCertCd = "CA006", Price = "1000원", KorYN = "Y" });
            certs.Add(new Job() { CertCd = "0006", CertNe = "납입증명서(연말정산용)", HostCertCd = "CA006", Price = "1000원", KorYN = "Y" });

            return certs;
        }
    }
}
