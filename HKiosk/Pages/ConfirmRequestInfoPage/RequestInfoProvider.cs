using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKiosk.Util;
using HKiosk.Manager.Navigation;

namespace HKiosk.Pages.ConfirmRequestInfoPage
{
    class RequestInfoProvider
    {
        public List<RequestInfo> RequestInfoList
        {
            get;
            private set;
        }
        private string CalcFinalPrice()
        {
            string finalPriceToString = "";
            int finalCount = 0;

            int finalAgencyPrice = 0;
            int finalIssuePrice = 0;
            int finalPrice = 0;

            for (int i=0; i<RequestInfoList.Count; i++)
            {
                finalCount += RequestInfoList[i].Count;

                string[] agencyPriceArray = RequestInfoList[i].AgencyPrice.Split('원');
                string[] issuePriceArray = RequestInfoList[i].IssuePrice.Split('원');

                finalAgencyPrice += Int32.Parse(agencyPriceArray[0]);
                finalIssuePrice += Int32.Parse(issuePriceArray[0]);
            }
            finalPrice = (finalAgencyPrice + finalIssuePrice) * finalCount;

            finalPriceToString = finalPrice.ToString() + "원";

            return finalPriceToString;
        }
        private void AddRequestInfo()
        {
            for(int i=0; i<6; i++)
            {
                RequestInfoList.Add(new RequestInfo
                {
                    CertNe = "증명서" + (i + 1).ToString(),
                    FromDate = DateTime.Now.AddDays(-i).ToString("yyyy.MM.dd"),
                    ToDate = DateTime.Now.ToString("yyyy.MM.dd"),
                    Count = 1,
                    IssuePrice = "0원",
                    AgencyPrice = "1000원",
                    IsCancel = false,
                    FinalPrice = CalcFinalPrice()
                }) ;
            }
        }

        public RequestInfoProvider()
        {
            RequestInfoList = new List<RequestInfo>();
            AddRequestInfo();
        }
    }
}
