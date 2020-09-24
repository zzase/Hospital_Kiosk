using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKiosk.Pages.SelectHistory
{
    class HistroyProvider
    {

        public List<History> histories;
       
        readonly DateTime dt = DateTime.Now;
        public List<History> AddHistory()
        {
            for (int i = 0; i < 60; i++)
            {
                histories.Add(new History()
                {
                    Date = dt.AddDays(-(i + 1)),
                    DeptNe = "진료과" + (i + 1).ToString(),
                    IsChecked = false
                }) ;
            }

            return histories;
        }

        public HistroyProvider()
        {
            histories = new List<History>();
        }
       
    }
}
