using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKiosk.Manager.Data
{
    public class CertRequestInfo
    {
        public string ReqSeq { get; set; }    // 요청 키값
        public string CertNo { get; set; }    // 증명서 일련번호
        public string Count { get; set; }    // 매수
        public bool IsCheckedForCancel { get; set; }    // 취소 체크박스 선택여부
        public Job Job { get; set; }    // 증명서 정보
        public SujinHistroy SujinHistroy { get; set; }    // 수진 이력
    }
}
