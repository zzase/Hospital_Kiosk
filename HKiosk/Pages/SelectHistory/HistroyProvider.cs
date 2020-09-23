using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKiosk.Pages.SelectHistory
{
    class HistroyProvider
    {
       // readonly SelectHistoryPage shp = new SelectHistoryPage();
        
        public List<History> histories;
        public List<History> selectedHistories;

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

        //public List<History> SelectHistory()
        //{
        //    DateTime toDateTime = shp.ToDate.DisplayDateStart.Value.Date;
        //    DateTime fromDateTime = shp.FromDate.DisplayDateStart.Value.Date;

        //    Console.WriteLine(toDateTime.ToString());
        //    Console.WriteLine(fromDateTime.ToString());

        //    foreach (var p in histories)
        //    {
        //        if ((DateTime.Compare(p.Date, fromDateTime) > 0 || DateTime.Compare(p.Date, fromDateTime) == 0) && (DateTime.Compare(p.Date, toDateTime) < 0 || DateTime.Compare(p.Date, toDateTime) == 0))
        //        {
        //            selectedHistories.Add(new History()
        //            {
        //                Date = p.Date,
        //                DeptNe = p.DeptNe,
        //                IsChecked = false
        //            });
        //        }
        //    }

        //    return selectedHistories;
        //}


        public HistroyProvider()
        {
            histories = new List<History>();
           // selectedHistories = new List<History>();
        }
       
    }
}
