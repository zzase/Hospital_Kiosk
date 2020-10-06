using HKiosk.Manager.Navigation;
using HKiosk.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HKiosk.Manager.Data
{
    public class Job
    {
        public string CertCd { get; set; }    // 증명서 코드
        public string CertNe { get; set; }    // 증명서명
        public string HosCertCd { get; set; }     // 병원연계 코드
        public string Price { get; set; }     // 수수료
        public string KorYN { get; set; }    // Y(한글), N(영문)
        public ICommand SelectCommand { get; }    // 선택 커맨드

        public Job()
        {
            SelectCommand = new Command((obj) =>
            {
                DataManager.Instance.SelectedJob = this;
                NavigationManager.Navigate(PageElement.SelectHistory);
            });
        }
    }
}
