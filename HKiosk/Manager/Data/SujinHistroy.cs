using HKiosk.Base;
using HKiosk.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        public ICommand PlusCommand { get; } // 매수 증가
        public ICommand MinusCommand { get; } // 매수 감소
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
            Count = "0";
            BoxItems = new List<string>();

            for (int i = 1; i < 10; i++)
            {
                BoxItems.Add(i.ToString());
            }

            PlusCommand = new Command((obj) =>
            {
                Count = (Int32.Parse(Count) + 1).ToString();
            });

            MinusCommand = new Command((obj) =>
            {
                Count = (Int32.Parse(Count) - 1).ToString();
                if (Int32.Parse(Count) < 0) Count = "0";
            });
        }

    }
}
