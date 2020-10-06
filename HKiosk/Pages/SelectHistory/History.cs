using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKiosk.Base;

namespace HKiosk.Pages.SelectHistory
{
    public class History : PropertyChange
    {
        DateTime date;
        string deptNe;
        bool isChecked;

       public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);

        }

       public string DeptNe
        {
            get => deptNe;
            set => SetProperty(ref deptNe, value);

        }

        public bool IsChecked
        {
            get => isChecked;
            set => SetProperty(ref isChecked, value);
        }
    }
}
