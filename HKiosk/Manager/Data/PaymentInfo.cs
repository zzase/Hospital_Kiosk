using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKiosk.Manager.Data
{
    public class PaymentInfo
    {
        public string BuyerName { get; set; }    // 신청자 성명
        public string BuyerTel { get; set; }    // 신청자 휴대폰번호
        public string BuyerEmail { get; set; }    // 신청자 이메일
        public string OrderNo { get; set; }    // 결제 주문번호
        public string PayMethod { get; set; }    // 결제수단
        public string ResultCode { get; set; }    // 결제 결과 코드
        public string GoodsName { get; set; }    // 결제 상품명
        public string Amt { get; set; }    // 결제 금액
    }
}
