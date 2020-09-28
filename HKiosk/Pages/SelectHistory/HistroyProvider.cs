using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKiosk.Manager.Data;
using System.Collections.ObjectModel;

namespace HKiosk.Pages.SelectHistory
{
    class HistroyProvider
    {

        public ObservableCollection<SujinHistroy> sujinHistories;
       
        readonly DateTime dt = DateTime.Now;
        public ObservableCollection<SujinHistroy> AddHistory()
        {
            for (int i = 0; i < 60; i++)
            {
                sujinHistories.Add(new SujinHistroy()
                {
                    SujinKey = "SujinKey" + (i + 1).ToString(),
                    DeptNo = "Code" + (i + 1).ToString(),
                    DeptNe = "진료과" + (i + 1).ToString(),
                    CertDate = dt.AddDays(-i).ToString("yyyy.MM.dd"),
                    InDate = dt.AddDays(-i).ToString("yyyy.MM.dd"),
                    OutDate = dt.ToString("yyyy.MM.dd"),
                    IsChecked = false
                });
            }

            return sujinHistories;
        }

        public HistroyProvider()
        {
            sujinHistories = new ObservableCollection<SujinHistroy>();
        }
       
    }
}
