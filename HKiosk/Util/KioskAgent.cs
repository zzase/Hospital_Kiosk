using System.Diagnostics;
using System.Text;
using System.IO;
using System;
using System.Threading.Tasks;
using Random = System.Random;
using System.Threading;

namespace HKiosk.Util
{
    public static class KioskAgent
    {
        // 에이전트 프로세스 객체
        private static Process _agentProcess;

        // 프로세스 시작 정보가 담긴 객체
        private static ProcessStartInfo _processInfo = new ProcessStartInfo()
        {
            // 에이전트 실행 경로
            FileName = $"{AppDomain.CurrentDomain.BaseDirectory}/KioskAgent/KioskAgent.exe",
            // 윈도우 스타일 (Hidden)
            WindowStyle = ProcessWindowStyle.Hidden,
            UseShellExecute = false,
            // Input 스트림 사용 
            RedirectStandardInput = true,
            // Output 스트림 사용
            RedirectStandardOutput = true,
            // 윈도우 생성 여부 (true)
            CreateNoWindow = true,
            // Error 다이어로그 사용 여부
            ErrorDialog = true,
            // OutPut 스트림 인코딩
            StandardOutputEncoding = Encoding.GetEncoding("euc-kr")
        };

        /// <summary>
        /// 프로세스 생성
        /// </summary>
        /// <param name="arg">어큐먼트</param>
        /// <returns></returns>
        private static Process CreateProcess(string arg)
        {
            try
            {
                _processInfo.Arguments = arg;
                return Process.Start(_processInfo);
            }
            catch (Exception e)
            {
                Log.Write($"[KioskAgent - CreateProcess()] 예외 {e.ToString()}");
                return null;
            }
        }

        public static void KillPrevProcess()
        {
            KillProcess(_agentProcess);
        }

        /// <summary>
        /// 에이전트 프로세스 삭제
        /// </summary>
        /// <param name="process"></param>
        public static void KillProcess(Process process)
        {
            try
            {
                if (!process?.HasExited ?? false)
                {
                    process.Kill();
                }
            }
            catch (Exception e)
            {
                Log.Write($"[KioskAgent - KillProcess(Process process)] 예외 {e.ToString()}");
            }
            finally
            {
                process = null;
            }
        }

        /// <summary>
        /// 프로세스 삭제
        /// </summary>
        /// <param name="name"></param>
        public static Task<string> KillProcessByName(string name)
        {
            string arg = $"predup \"{name}\" \"{name}\"";

            return StartProcess(arg);
        }

        /// <summary>
        /// 실행된 프로세스 최상위로 올리기 
        /// </summary>
        /// <param name="name"></param>
        public static Task<string> ProcessUp(string name)
        {
            string arg = $"processup {name}";

            return StartProcess(arg);
        }

        /// <summary>
        /// 시리얼 단말기 통신 오픈
        /// </summary>
        /// <param name="nfcport">NFC 학생증 단말기 COM 포트</param>
        /// <param name="qrport">QR 리더기 COM 포트</param>
        /// <returns></returns>
        public static Task<string> SerialDevice(string nfcport, string qrport)
        {
            // --------------------------------------------------
            // return DummySerialValue();
            // --------------------------------------------------

            string arg = $"serialopen \"{nfcport}\" \"{qrport}\"";

            return StartProcess(arg);
        }

        private static string DummySerialValue()
        {
            Thread.Sleep(2000);

            Random rnd = new Random();
            int rndNumber = rnd.Next(3);

            string ret = "";
            if (rndNumber == 0)
            {
                ret = "dz:kisok:nfc:<STX>,317 150303223920     <DLE>8705191047632<DC3>x??<SOH><NUL><ETX>";
            }
            else if (rndNumber == 1)
            {
                ret = "dz:kisok:qr:<STX>15D53037D5E12FA25739037AFA1321C1755BD6B78EF26CE0CC732C6A65CF13CE<ETX>";
            }
            else
            {
                ret = "dz:kisok:nfc:<STX>,318 212016311530     <DLE>9802220000000<DC3>x??<SOH><NUL><ETX>";
            }

            return ret.Trim();
        }

        /// <summary>
        /// Aria 암호화 모듈 사용
        /// </summary>
        /// <param name="Akey">암호화 키</param>
        /// <param name="ASource">암호화 할 문자</param>
        /// <returns></returns>
        public static Task<string> UseEncAria(string Akey, string ASource)
        {
            string arg = $"encaria \"{Akey}\" \"{ASource}\"";

            return StartProcess(arg);
        }

        /// <summary>
        /// Aria 복호화 모듈 사용
        /// </summary>
        /// <param name="Akey">복호화 키</param>
        /// <param name="ASource">복호화 할 문자</param>
        /// <returns></returns>
        public static Task<string> UseDecAria(string Akey, string ASource)
        {
            string arg = $"decaria \"{Akey}\" \"{ASource}\"";

            return StartProcess(arg);
        }

        /// <summary>
        /// 증명서 출력 모듈 사용
        /// </summary>
        /// <param name="ATpid">TPID</param>
        /// <param name="AMinNo">민번호</param>
        /// <param name="AJobNe">증명서종류</param>
        /// <param name="AGiwanNo">기관번호</param>
        /// <param name="iMinCnt">개수</param>
        /// <returns></returns>
        public static Task<string> UseCertPrint(string ATpid, string AMinNo, string AJobNe, string AGiwanNo, string iMinCnt)
        {
            string arg = string.Empty;

            //arg = $"print \"{ATpid}\" \"{AMinNo}\" \"{AJobNe}\" \"{AGiwanNo}\" \"{iMinCnt}\" " +
            //    $"\"{KioskManager.Instance._mSettingInfo.ReportXURLCheck}\" " +
            //    $"\"{KioskManager.Instance._mSettingInfo.ReportXURLFile}\" " +
            //    $"\"{KioskManager.Instance._mSettingInfo.ReportXURLPost}\"";

            return StartProcess(arg);
        }

        /// <summary>
        /// 프로세스찾기
        /// </summary>
        /// <param name="processName">찾을 프로세스 이름</param>
        /// <returns></returns
        public static Task<string> CheckProcess(string processName)
        {
            string arg = $"checkprocess \"{processName}\"";
            return StartProcess(arg);
        }

        #region Print상태 얻어오는 모듈
        /// <summary>
        /// 프린트 출력상태 가져오기
        /// </summary>
        /// <param name="deviceName">프린트 모델명</param>
        /// <param name="printerUrl">프린트 관리자 페이지 url</param>
        /// <param name="order">상태 체크 타입</param>
        /// <returns></returns>
        public static Task<string> GetPrintStatus(string deviceName, string printerUrl, string order)
        {
            // order : errorimg / error / status / expendable
            string arg = $"checkprint \"{deviceName}\" \"{printerUrl}\" \"{order}\"";
            return StartProcess(arg);
        }

        /// <summary>
        /// 프린트 스풀 개수 가져오기
        /// </summary>
        /// <returns></returns>
        public static Task<string> GetPrintSpoolCount()
        {
            string arg = $"checkprintspool";
            return StartProcess(arg);
        }
        #endregion

        /// <summary>
        /// 휴대폰 결제 진행
        /// </summary>
        /// <param name="sMrchid">MRCHID</param>
        /// <param name="sSvcId">SVCID</param>
        /// <param name="APrice">가격</param>
        /// <param name="ACertNe">증명서명</param>
        /// <param name="AHPPNo">핸드폰번호</param>
        /// <param name="ACompany">회사명</param>
        /// <param name="AJuminNo">주민번호(7자리)</param>
        /// <param name="AOrderNo">주문번호</param>
        /// <param name="AGiwanNe">기관명</param>
        /// <returns></returns>
        public static Task<string> PayPhone(string sMrchid, string sSvcId, string APrice, string ACertNe, string AHPPNo, string ACompany, string AJuminNo, string AOrderNo, string AGiwanNe)
        {
            string arg = $"phonepay \"{sMrchid}\" \"{sSvcId}\" \"{APrice}\" \"{ACertNe}\" \"{AHPPNo}\" \"{ACompany}\" \"{AJuminNo}\" \"{AOrderNo}\" \"{AGiwanNe}\"";

            return StartProcess(arg, false);
        }

        /// <summary>
        /// 휴대폰 인증 코드 입력 
        /// </summary>
        /// <param name="AAuthCode">인증코드(6자리)</param>
        /// <returns></returns>
        public static Task<string> AuthPhone(string AAuthCode)
        {
            string arg = $"{AAuthCode}";

            return GetProcessResult(arg);
        }

        /// <summary>
        /// 티머니 결제
        /// </summary>
        /// <param name="iComPort">단말기 포트번호</param>
        /// <param name="sPrice">가격</param>
        /// <param name="sTid">Tid</param>
        /// <param name="iTimeOut">시간제한</param>
        /// <returns></returns>
        public static Task<string> PayTmoney(string iComPort, string sPrice, string sTid, string iTimeOut)
        {
            string arg = $"tmoney \"{iComPort}\" \"{sPrice}\" \"{sTid}\" \"{iTimeOut}\" \"티머니\"";

            return StartProcess(arg);
        }

        /// <summary>
        /// 캐시비 결제
        /// </summary>
        /// <param name="iComPort">단말기 포트번호</param>
        /// <param name="sPrice">가격</param>
        /// <param name="sTid">Tid</param>
        /// <param name="iTimeOut">시간제한</param>
        /// <returns></returns>
        public static Task<string> PayCashbee(string iComPort, string sPrice, string sTid, string iTimeOut)
        {
            string arg = $"cashbee \"{iComPort}\" \"{sPrice}\" \"{sTid}\" \"{iTimeOut}\" \"캐시비\"";

            return StartProcess(arg);
        }

        /// <summary>
        /// 신용카드 결제
        /// </summary>
        /// <param name="price">가격</param>
        /// <param name="timeOut">시간제한</param>
        /// <returns></returns>
        public static Task<string> PayCard(string price, string timeOut)
        {
            var arg = $"cardpay \"{price}\" \"{timeOut}\" \"결제진행\"";

            return StartProcess(arg);
        }

        /// <summary>
        /// 카드 결제 취소 모듈 사용 
        /// </summary>
        /// <param name="AuthNo"></param>
        /// <param name="AuthDate"></param>
        /// <param name="Price"></param>
        /// <param name="TimeOut"></param>
        /// <returns></returns>
        public static Task<string> CancelPayCard(string AuthNo, string AuthDate, string Price, string TimeOut)
        {
            string arg = $"cardcancel \"{AuthNo}\" \"{AuthDate}\" \"{Price}\" \"{TimeOut}\" \"결제취소\"";

            return StartProcess(arg);
        }

        /// <summary>
        /// 프로세스 결과 값 리턴 줄임
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private static Task<string> StartProcess(string arg, bool isKill = true)
        {
            Process process = CreateProcess(arg);
            _agentProcess = isKill ? _agentProcess : process;

            return GetProcessResult(process, isKill);
        }

        /// <summary>
        /// 프로세스 결과 값 리턴
        /// </summary>
        /// <param name="process">프로세스 GetProcess()</param>
        /// <param name="isKill">프로세스 삭제 여부</param>
        /// <returns></returns>
        private static async Task<string> GetProcessResult(Process process, bool isKill)
        {
            string result = string.Empty;

            try
            {
                result = await process.StandardOutput.ReadLineAsync();
            }
            catch (Exception e)
            {
                Log.Write($"[KioskAgent - GetProcessResult()] 예외 {e.ToString()}");
                KillProcess(process);
            }

            if (isKill) KillProcess(process);

            return result ?? string.Empty;
        }

        /// <summary>
        /// 폰 승인번호 전용
        /// </summary>
        /// <param name="arg">어큐먼트</param>
        /// <returns></returns>
        private static async Task<string> GetProcessResult(string arg)
        {
            string result = string.Empty;

            if (_agentProcess == null)
                return result;

            try
            {
                await _agentProcess.StandardInput.WriteAsync($"{arg}{Environment.NewLine}");
                _agentProcess.StandardInput.Close();

                result = await _agentProcess.StandardOutput.ReadLineAsync();
            }
            catch (Exception e)
            {
                Log.Write($"[KioskAgent - GetProcessResult()] 예외 {e.ToString()}");
            }

            KillProcess(_agentProcess);

            return result;
        }

        /// <summary>
        /// 에이전트 파일 검색
        /// </summary>
        /// <returns></returns>
        private static bool FindAgent()
        {
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}/KioskAgent/KioskAgent.exe";

            return File.Exists(path);
        }
    }
}
