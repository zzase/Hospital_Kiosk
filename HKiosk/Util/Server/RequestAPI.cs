using HKiosk.Manager.Data;
using Newtonsoft.Json;
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

        internal static List<T> JArrayToList<T>(JArray jArray)
        {
            try
            {
                return (List<T>)Convert.ChangeType(JsonConvert.DeserializeObject<List<T>>(jArray.ToString()), typeof(List<T>));
            }
            catch (Exception ex)
            {
                Log.Write($"JArrayToList Error {ex}");
                return null;
            }
        }

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
        /// <param name="uName">성명</param>
        /// <param name="uJumin">주민번호</param>
        /// <returns></returns>
        internal static async Task<JObject> PatNoRequest(string uName, string uJumin)
        {
            var giwanNo = DataManager.Instance.SettingInfo.GiwanNo;

            var paramData = new JObject
            {
                { "command", "patNoReq" },
                { "giwanNo", giwanNo },
                { "uName", uName },
                { "uJumin", uJumin }
            };

            var postdata = new JObject
            {
                {"reqData", await KioskAgent.UseEncAria("10001", paramData.ToString())}
            };

            return await WebServer.RequestAsync(postdata.ToString(), $"{operationURL}/ModuleM.do", "post", true);
        }

        internal static async Task<JObject> PatNoRequest_Test(string uName, string uJumin)
        {
            JObject paramData = null;

            switch (uName)
            {
                case "1":
                    paramData = new JObject
                    {
                        { "resultCode", "200" },
                        { "uPatNo", "2020123456" }
                    };
                    break;
                case "2":
                    paramData = new JObject
                    {
                        { "resultCode", "400" },
                        { "resultMessage", "등록된 환자정보가 없습니다." }
                    };
                    break;
                case "3":
                    paramData = new JObject
                    {
                        { "resultCode", "400" },
                        { "resultMessage", "발급에 필요한 필수데이터가 누락되었습니다." }
                    };
                    break;
                case "4":
                    paramData = new JObject
                    {
                        { "resultCode", "400" },
                        { "resultMessage", "서버가 다운되었거나 시스템오류가 발생하였습니다." }
                    };
                    break;
                default:
                    paramData = new JObject
                    {
                        { "resultCode", "" }
                    };
                    break;
            }

            await Task.Delay(1000);

            return paramData;
        }
        #endregion

        #region CertPage
        /// <summary>
        /// 발급가능 증명서 조회
        /// </summary>
        /// <returns></returns>
        internal static async Task<JObject> JobListRequest()
        {
            var giwanNo = DataManager.Instance.SettingInfo.GiwanNo;
            var uName = DataManager.Instance.PatientInfo.Name;
            var uBirth = DataManager.Instance.PatientInfo.Birth;
            var uPatNo = DataManager.Instance.PatientInfo.PatientNo;

            var paramData = new JObject
            {
                { "command", "jobList" },
                { "giwanNo", giwanNo },
                { "uName", uName },
                { "uBirth", uBirth },
                { "uPatNo", uPatNo }
            };

            var postdata = new JObject
            {
                {"reqData", await KioskAgent.UseEncAria("10001", paramData.ToString())}
            };

            return await WebServer.RequestAsync(postdata.ToString(), $"{operationURL}/ModuleM.do", "post", true);
        }

        /// <summary>
        /// 수진이력 조회
        /// </summary>
        /// <param name="certCd">증명서 코드</param>
        /// <param name="certNe">증명서 명칭</param>
        /// <param name="hosCertCd">병원연계 코드</param>
        /// <param name="fromDate">조회 일자(시작일)</param>
        /// <param name="toDate">조회 일자(종료일)</param>
        /// <returns></returns>
        internal static async Task<JObject> CertSujinRequest(string certCd, string certNe, string hosCertCd, string fromDate, string toDate)
        {
            var giwanNo = DataManager.Instance.SettingInfo.GiwanNo;
            var uPatNo = DataManager.Instance.PatientInfo.PatientNo;
            var uName = DataManager.Instance.PatientInfo.Name;
            var uBirth = DataManager.Instance.PatientInfo.Birth;

            var paramData = new JObject
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
                {"reqData", await KioskAgent.UseEncAria("10001", paramData.ToString())}
            };

            return await WebServer.RequestAsync(postdata.ToString(), $"{operationURL}/ModuleM.do", "post", true);
        }
        #endregion
    }
}
