using HKiosk.Base;
using HKiosk.Controls.NavigationBar;
using HKiosk.Manager.Popup;
using HKiosk.Manager.Timer;
using HKiosk.Pages.IdentityVerification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace HKiosk.Manager.Navigation
{
    public static class NavigationManager
    {
        public static INavigation Navigation { get; set; }

        public static void Navigate(PageElement page)
        {
            Init();
            Navigation?.Navigate(page);
        }

        private static void Init()
        {
            TimerManager.Timer.Stop();
            PopupManager.Instance[PopupElement.Alert]?.Hide();
            PopupManager.Instance[PopupElement.Confirm]?.Hide();
            PopupManager.Instance[PopupElement.Loding]?.Hide();
        }
    }

    public enum PageElement
    {
        Main, // MAIN
        IdentityVerification, // 로그인(휴대폰인증)
        ConfirmUserInfo,// 사용자 정보 확인
        SelectCert,// 증명서 선택
        SelectHistory, // 수진이력 선택
        SelectDetail, // 수진이력 선택 후 세부 옵션 선택 
        ConfirmRequestInfo, // 신청내용 확인
        SelectPayment, // 결제방법 선택
        CardPayment, // 카드 결제
        Agreement, // 핸드폰 결제 약관동의
        InfoInput, // 핸드폰 결제 정보입력
        ApprovalNumber, //핸드폰 결제 승인번호 입력 
        CashbeePayment, // 캐시비 결제
        TmoneyPayment, // 티머니 결제
        SelectIssuanceMethod, // 출력 방법 선택
        Print,// 증명서 출력 화면
        PrintSuccess,//출력 성공 화면
        Fail,
        Fax, // 팩스 전송 화면
        Mail // 메일 전송 화면 
    }
}