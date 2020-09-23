using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKiosk.Util.Server
{
    public static class RequestAPI
    {
        internal static string operationURL = string.Empty;

        #region XML
        /// <summary>
        /// 키오스크 세팅 XML 가져오기
        /// </summary>
        /// <returns></returns>
        internal static async Task<string> GetKioskSettingInfoXml()
        {
            return await WebServer.Get($"{operationURL}/wm1.0/xml/KIOSK_SETTING_INFO.xml");
        }

        /// <summary>
        /// 증명서 정보 XML 가져오기
        /// </summary>
        /// <returns></returns>
        internal static async Task<string> GetKioskCertiInfoXml()
        {
            return await WebServer.Get($"{operationURL}/wm1.0/xml/KIOSK_CERTI_INFO.xml");
        }

        /// <summary>
        /// 지역화 XML 가져오기
        /// </summary>
        /// <returns></returns>
        internal static async Task<string> GetKioskLocalizationInfoXml()
        {
            return await WebServer.Get($"{operationURL}/wm1.0/xml/KIOSK_LOCALIZATION.xml");
        }
        #endregion

        #region PatientPage
        /// <summary>
        /// 환자정보 조회 API
        /// </summary>
        /// <param name="giwanNo">기관번호</param>
        /// <param name="uName">성명</param>
        /// <param name="uJumin">주민번호</param>
        /// <returns></returns>
        internal static async Task<string> PatNoRequest(string giwanNo, string uName, string uJumin)
        {
            var paramdata = new JObject
            {
                { "command", "patNoReq" },
                { "giwanNo", giwanNo },
                { "uName", uName },
                { "uJumin", uJumin }
            };

            var postdata = new JObject
            {
                {"reqData", await KioskAgent.UseEncAria("10001", paramdata.ToString())}
            };

            return await WebServer.RequestAsync(postdata.ToString(), $"{operationURL}/ModuleM.do", "post", true);
        }
        #endregion

        #region CertPage
        /// <summary>
        /// 발급가능 증명서 조회
        /// </summary>
        /// <param name="giwanNo">기관번호</param>
        /// <param name="uName">성명</param>
        /// <param name="uBirth">생년월일</param>
        /// <param name="uPatNo">환자번호</param>
        /// <returns></returns>
        internal static async Task<string> JobListRequest(string giwanNo, string uName, string uBirth, string uPatNo)
        {
            var paramdata = new JObject
            {
                { "command", "jobList" },
                { "giwanNo", giwanNo },
                { "uName", uName },
                { "uBirth", uBirth },
                { "uPatNo", uPatNo }
            };

            var postdata = new JObject
            {
                {"reqData", await KioskAgent.UseEncAria("10001", paramdata.ToString())}
            };

            return await WebServer.RequestAsync(postdata.ToString(), $"{operationURL}/ModuleM.do", "post", true);
        }

        /// <summary>
        /// 수진이력 조회
        /// </summary>
        /// <param name="giwanNo">기관번호</param>
        /// <param name="uPatNo">환자번호</param>
        /// <param name="uName">성명</param>
        /// <param name="uBirth">생년월일</param>
        /// <param name="certCd">증명서 코드</param>
        /// <param name="CertNe">증명서 명칭</param>
        /// <param name="hosCertCd">병원연계 코드</param>
        /// <param name="fromDate">조회 일자(시작일)</param>
        /// <param name="toDate">조회 일자(종료일)</param>
        /// <returns></returns>
        internal static async Task<string> CertSujinRequest(string giwanNo, string uPatNo, string uName, string uBirth,
            string certCd, string certNe, string hosCertCd, string fromDate, string toDate)
        {
            var paramdata = new JObject
            {
                { "command", "certSujinReq" },
                { "giwanNo", giwanNo },
                { "uPatNo", uPatNo },
                { "uName", uName },
                { "uBirth", uBirth },
                { "certCd", certCd },
                { "certNe", certNe },
                { "hosCertCd", hosCertCd },
                { "fromDate", fromDate },
                { "toDate", toDate },
            };

            var postdata = new JObject
            {
                {"reqData", await KioskAgent.UseEncAria("10001", paramdata.ToString())}
            };

            return await WebServer.RequestAsync(postdata.ToString(), $"{operationURL}/ModuleM.do", "post", true);
        }
        #endregion
    }
}
