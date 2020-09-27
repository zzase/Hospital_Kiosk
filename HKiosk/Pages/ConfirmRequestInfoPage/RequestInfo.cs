using HKiosk.Base;
using System.Windows.Input;

namespace HKiosk.Pages.ConfirmRequestInfoPage
{
    class RequestInfo : PropertyChange
    {
        private string certNe;      // 증명서 종류
        private string fromDate;    // 입원(발급)일자
        private string toDate;      // 퇴원일자
        private int count;          // 매수
        private string price; // 대행 수수료
        private bool isCancel;      // 취소
        
        public string CertNe
        {
            get => certNe;
            set => SetProperty(ref certNe, value);
        }

        public string FromDate
        {
            get => fromDate;
            set => SetProperty(ref fromDate, value);
        }

        public string ToDate
        {
            get => toDate;
            set => SetProperty(ref toDate, value);
        }

        public int Count
        {
            get => count;
            set => SetProperty(ref count, value);
        }

        public string Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public bool IsCancel
        {
            get => isCancel;
            set => SetProperty(ref isCancel, value);
        }
    }
}
