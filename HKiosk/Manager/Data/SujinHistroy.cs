using HKiosk.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKiosk.Manager.Data
{
    public class SujinHistroy : PropertyChange
    {
        private bool isChecked;
        private string count;
        private List<string> boxItems;

        public string SujinKey { get; set; }    // 제증명발행Key
        public string DeptNo { get; set; }    // 진료과코드
        public string DeptNe { get; set; }    // 진료과명
        public string CertDate { get; set; }    // 작성일자
        public string InDate { get; set; }    // 입원일자
        public string OutDate { get; set; }    // 퇴원일자
        public string Count // 매수 
        {  
            get => count;
            set => SetProperty(ref count, value); 
        }    
        public bool IsChecked    // 선택 여부
        {
            get => isChecked; 
            set => SetProperty(ref isChecked, value); 
        }

        public List<string> BoxItems
        {
            get => boxItems;
            set => SetProperty(ref boxItems, value);
        }

        public SujinHistroy()
        {
            BoxItems = new List<string>();

            for (int i = 1; i < 10; i++)
            {
                BoxItems.Add(i.ToString());
            }
        }

    }
}
