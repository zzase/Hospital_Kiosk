using HKiosk.Base;

namespace HKiosk.Manager.Data
{
    public class CertRequestInfo : PropertyChange
    {
        private string state;
        private string requestDetail;

        public string CertNo { get; set; }    // 증명서 일련번호
        public string Count { get; set; }    // 매수
        public bool IsCheckedForCancel { get; set; }    // 취소 체크박스 선택여부
        public Job Job { get; set; }    // 증명서 정보
        public SujinHistroy SujinHistroy { get; set; }    // 수진 이력
        public string StateCode { get; set; }    // 상태 코드
        public string State    // 상태
        { 
            get => state; 
            set => SetProperty(ref state, value); 
        }
        public string RequestDetail
        {
            get => requestDetail;
            set => SetProperty(ref requestDetail, value);
        }
    }
}
