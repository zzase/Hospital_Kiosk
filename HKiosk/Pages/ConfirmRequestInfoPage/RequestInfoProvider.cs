using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKiosk.Util;
using HKiosk.Manager.Navigation;
using System.Collections.ObjectModel;
using HKiosk.Manager.Data;
using System.Globalization;

namespace HKiosk.Pages.ConfirmRequestInfoPage
{
    public class RequestInfoProvider
    {
        public ObservableCollection<RequestInfo> requestInfoList;

        public string CalcFinalPrice()
        {
            string finalPriceToString;
            int finalPrice = 0;

            for (int i = 0; i < DataManager.Instance.CertRequestInfos.Count; i++)
            {
                string[] PriceArray = DataManager.Instance.CertRequestInfos[i].Job.Price.Split('원');

                finalPrice += Int32.Parse(PriceArray[0], NumberStyles.AllowThousands) * Int32.Parse(DataManager.Instance.CertRequestInfos[i].Count ?? "0");

            }
            finalPriceToString = String.Format("{0:#,0}", finalPrice) + "원";
            return finalPriceToString;
        }


        public ObservableCollection<RequestInfo> DeleteRequestInfo()
        {
            for (int i = 0; i < requestInfoList.Count; i++)
            {
                if (requestInfoList[i].IsCancel)
                {
                    requestInfoList.RemoveAt(i);
                }
            }
            return requestInfoList;
        }

        public RequestInfoProvider()
        {
            requestInfoList = new ObservableCollection<RequestInfo>();
        }
    }
}
