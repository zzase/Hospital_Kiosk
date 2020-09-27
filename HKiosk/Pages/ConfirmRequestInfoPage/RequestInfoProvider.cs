using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKiosk.Util;
using HKiosk.Manager.Navigation;
using System.Collections.ObjectModel;

namespace HKiosk.Pages.ConfirmRequestInfoPage
{
    class RequestInfoProvider
    {
        public ObservableCollection<RequestInfo> requestInfoList;
        public string finalPriceToString;
        public string CalcFinalPrice()
        {

            int finalPrice = 0;

            for (int i=0; i<requestInfoList.Count; i++)
            {

                string[] issuePriceArray = requestInfoList[i].Price.Split('원');

                finalPrice += Int32.Parse(issuePriceArray[0]) * requestInfoList[i].Count;
            }

            finalPriceToString = finalPrice.ToString() + "원";

            return finalPriceToString;
        }
        public ObservableCollection<RequestInfo> AddRequestInfo()
        {
            for(int i=0; i<6; i++)
            {
                requestInfoList.Add(new RequestInfo
                {
                    CertNe = "증명서" + (i + 1).ToString(),
                    FromDate = DateTime.Now.AddDays(-i).ToString("yyyy.MM.dd"),
                    ToDate = DateTime.Now.ToString("yyyy.MM.dd"),
                    Count = 1,
                    Price = "1000원",
                    IsCancel = false
                }) ;
            }
            return requestInfoList;
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
